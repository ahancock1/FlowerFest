// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   BlogService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Models;

    public class BlogService : IBlogService
    {
        private readonly List<Post> _cache = new List<Post>();
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _folder;

        public BlogService(IHostingEnvironment env, IHttpContextAccessor contextAccessor)
        {
            _folder = Path.Combine(env.WebRootPath, "posts");
            _contextAccessor = contextAccessor;

            Initialize();
        }

        public virtual Task<IEnumerable<Post>> GetPosts(int count, int skip = 0)
        {
            var isAdmin = IsAdmin();

            var posts = _cache
                .Where(p => p.PublishedDate <= DateTime.UtcNow && (p.IsPublished || isAdmin))
                .Skip(skip)
                .Take(count);

            return Task.FromResult(posts);
        }

        public virtual Task<IEnumerable<Post>> GetPostsByCategory(string category)
        {
            var isAdmin = IsAdmin();

            var posts = from p in _cache
                        where p.PublishedDate <= DateTime.UtcNow && (p.IsPublished || isAdmin)
                        where p.Categories.Contains(category, StringComparer.OrdinalIgnoreCase)
                        select p;

            return Task.FromResult(posts);
        }

        public virtual Task<Post> GetPostBySlug(string slug)
        {
            var post = _cache.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
            var isAdmin = IsAdmin();

            if (post != null && post.PublishedDate <= DateTime.UtcNow && (post.IsPublished || isAdmin))
            {
                return Task.FromResult(post);
            }

            return Task.FromResult<Post>(null);
        }

        public virtual Task<Post> GetPostById(string id)
        {
            var post = _cache.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            var isAdmin = IsAdmin();

            if (post != null && post.PublishedDate <= DateTime.UtcNow && (post.IsPublished || isAdmin))
            {
                return Task.FromResult(post);
            }

            return Task.FromResult<Post>(null);
        }

        public virtual Task<IEnumerable<string>> GetCategories()
        {
            var isAdmin = IsAdmin();

            var categories = _cache
                .Where(p => p.IsPublished || isAdmin)
                .SelectMany(post => post.Categories)
                .Select(cat => cat.ToLowerInvariant())
                .Distinct();

            return Task.FromResult(categories);
        }

        public async Task SavePost(Post post)
        {
            var filePath = GetFilePath(post);
            post.ModifiedDate = DateTime.UtcNow;

            var doc = new XDocument(
                new XElement("post",
                    new XElement("title", post.Title),
                    new XElement("slug", post.Slug),
                    new XElement("pubDate", post.PublishedDate.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("lastModified", post.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("excerpt", post.Description),
                    new XElement("content", post.Content),
                    new XElement("ispublished", post.IsPublished),
                    new XElement("categories", string.Empty),
                    new XElement("comments", string.Empty)
                ));

            var categories = doc.XPathSelectElement("post/categories");
            foreach (var category in post.Categories)
            {
                categories.Add(new XElement("category", category));
            }

            var comments = doc.XPathSelectElement("post/comments");
            foreach (var comment in post.Comments)
            {
                comments.Add(
                    new XElement("comment",
                        new XElement("author", comment.Author),
                        new XElement("email", comment.Email),
                        new XElement("date", comment.PublishedDate.ToString("yyyy-MM-dd HH:m:ss")),
                        new XElement("content", comment.Content),
                        new XAttribute("isAdmin", comment.IsAdmin),
                        new XAttribute("id", comment.Id)
                    ));
            }

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await doc.SaveAsync(fs, SaveOptions.None, CancellationToken.None).ConfigureAwait(false);
            }

            if (!_cache.Contains(post))
            {
                _cache.Add(post);
                SortCache();
            }
        }

        public Task DeletePost(Post post)
        {
            var filePath = GetFilePath(post);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            if (_cache.Contains(post))
            {
                _cache.Remove(post);
            }

            return Task.CompletedTask;
        }

        public async Task<string> SaveFile(byte[] bytes, string fileName, string suffix = null)
        {
            suffix = suffix ?? DateTime.UtcNow.Ticks.ToString();

            var ext = Path.GetExtension(fileName);
            var name = Path.GetFileNameWithoutExtension(fileName);

            var relative = $"files/{name}_{suffix}{ext}";
            var absolute = Path.Combine(_folder, relative);
            var dir = Path.GetDirectoryName(absolute);

            Directory.CreateDirectory(dir);
            using (var writer = new FileStream(absolute, FileMode.CreateNew))
            {
                await writer.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            }

            return "/posts/" + relative;
        }

        private string GetFilePath(Post post)
        {
            return Path.Combine(_folder, post.Id + ".xml");
        }

        private void Initialize()
        {
            LoadPosts();
            SortCache();
        }

        private void LoadPosts()
        {
            if (!Directory.Exists(_folder))
                Directory.CreateDirectory(_folder);

            // Can this be done in parallel to speed it up?
            foreach (var file in Directory.EnumerateFiles(_folder, "*.xml", SearchOption.TopDirectoryOnly))
            {
                var doc = XElement.Load(file);

                var post = new Post
                {
                    Id = Path.GetFileNameWithoutExtension(file),
                    Title = ReadValue(doc, "title"),
                    Description = ReadValue(doc, "excerpt"),
                    Content = ReadValue(doc, "content"),
                    Slug = ReadValue(doc, "slug").ToLowerInvariant(),
                    PublishedDate = DateTime.Parse(ReadValue(doc, "pubDate")),
                    ModifiedDate = DateTime.Parse(ReadValue(doc, "lastModified", DateTime.Now.ToString())),
                    IsPublished = bool.Parse(ReadValue(doc, "ispublished", "true"))
                };

                LoadCategories(post, doc);
                LoadComments(post, doc);
                _cache.Add(post);
            }
        }

        private static void LoadCategories(Post post, XElement doc)
        {
            var categories = doc.Element("categories");
            if (categories == null)
                return;

            var list = new List<string>();

            foreach (var node in categories.Elements("category"))
            {
                list.Add(node.Value);
            }

            post.Categories = list.ToArray();
        }

        private static void LoadComments(Post post, XElement doc)
        {
            var comments = doc.Element("comments");

            if (comments == null)
                return;

            foreach (var node in comments.Elements("comment"))
            {
                var comment = new Comment
                {
                    Id = ReadAttribute(node, "id"),
                    Author = ReadValue(node, "author"),
                    Email = ReadValue(node, "email"),
                    IsAdmin = bool.Parse(ReadAttribute(node, "isAdmin", "false")),
                    Content = ReadValue(node, "content"),
                    PublishedDate = DateTime.Parse(ReadValue(node, "date", "2000-01-01"))
                };

                post.Comments.Add(comment);
            }
        }

        private static string ReadValue(XElement doc, XName name, string defaultValue = "")
        {
            if (doc.Element(name) != null)
                return doc.Element(name).Value;

            return defaultValue;
        }

        private static string ReadAttribute(XElement element, XName name, string defaultValue = "")
        {
            if (element.Attribute(name) != null)
                return element.Attribute(name).Value;

            return defaultValue;
        }

        protected void SortCache()
        {
            _cache.Sort((p1, p2) => p2.PublishedDate.CompareTo(p1.PublishedDate));
        }

        protected bool IsAdmin()
        {
            return _contextAccessor.HttpContext?.User?.Identity.IsAuthenticated == true;
        }

        public Task<IEnumerable<Post>> Search(string term)
        {
            var isAdmin = IsAdmin();

            var posts = _cache?
                .Where(p =>
                    {
                        return
                        p.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        p.Content.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        p.Description.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        p.PublishedDate <= DateTime.UtcNow && (p.IsPublished || isAdmin);
                    }
                )
                .OrderByDescending(p => p.PublishedDate)
                .ToList();

            if (posts != null)
            {
                return Task.FromResult<IEnumerable<Post>>(posts);
            }

            return Task.FromResult<IEnumerable<Post>>(null);
        }
    }
}