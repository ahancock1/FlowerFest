// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   BlogController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Helpers;
    using ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Services;
    using FlowerFest.ViewModels.Blog;

    public class BlogController : Controller
    {
        private readonly IBlogService _blog;
        private readonly IOptionsSnapshot<BlogSettings> _settings;

        public BlogController(IBlogService blog, IOptionsSnapshot<BlogSettings> settings)
        {
            _blog = blog;
            _settings = settings;
        }

        [Route("/Blog/{page:int?}")]
        public async Task<IActionResult> Index([FromRoute] int page = 0)
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Blog";

            var viewmodel = new BlogViewModel();

            var items = _settings.Value.PostsPerPage;
            var author = _settings.Value.Owner;

            foreach ( var post in await _blog.GetPosts(items))
            {
                // TODO - use automapper
                viewmodel.Posts.Add(new PostViewModel
                {
                    Author = author,
                    Categories = post.Categories,
                    Title = post.Title,
                    Published = post.PublishedDate,
                    Slug = post.Slug,
                    Description = post.Excerpt
                });
            }

            return View("Views/Blog/Index.cshtml", viewmodel);
        }
        
        [Route("/Blog/Category/{category}/{page:int?}")]
        public async Task<IActionResult> Category(string category, int page = 0)
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Blog";

            var viewmodel = new BlogViewModel();

            var author = _settings.Value.Owner;

            foreach (var post in await _blog.GetPostsByCategory(category))
            {
                viewmodel.Posts.Add(new PostViewModel
                {
                    Author = author,
                    Categories = post.Categories,
                    Title = post.Title,
                    Published = post.PublishedDate,
                    Slug = post.Slug,
                    Description = post.Excerpt
                });
            }

            return View("Views/Blog/Index.cshtml", viewmodel);
        }

        [Route("/Blog/{slug?}")]
        public async Task<IActionResult> Post(string slug)
        {
            var post = await _blog.GetPostBySlug(slug);

            if (post != null)
            {
                return View("PostDetail", post);
            }

            return NotFound();
        }

        [Route("/Blog/Edit/{id?}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new Post());
            }

            var post = await _blog.GetPostById(id);

            if (post != null)
            {
                return View(post);
            }

            return NotFound();
        }

        [Route("/Blog/{slug?}")]
        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", post);
            }

            var existing = await _blog.GetPostById(post.ID) ?? post;
            string categories = Request.Form["categories"];

            existing.Categories = categories.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim().ToLowerInvariant()).ToList();
            existing.Title = post.Title.Trim();
            existing.Slug = post.Slug.Trim();
            existing.Slug = !string.IsNullOrWhiteSpace(post.Slug)
                ? post.Slug.Trim()
                : PostHelper.GenerateSlug(post.Title);
            existing.IsPublished = post.IsPublished;
            existing.Content = post.Content.Trim();
            existing.Excerpt = post.Excerpt.Trim();

            await _blog.SavePost(existing);

            await SaveFilesToDisk(existing);

            return Redirect(post.Link);
        }

        private async Task SaveFilesToDisk(Post post)
        {
            var imgRegex = new Regex("<img[^>].+ />", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var base64Regex = new Regex("data:[^/]+/(?<ext>[a-z]+);base64,(?<base64>.+)", RegexOptions.IgnoreCase);

            foreach (Match match in imgRegex.Matches(post.Content))
            {
                var doc = new XmlDocument();
                doc.LoadXml("<root>" + match.Value + "</root>");

                var img = doc.FirstChild.FirstChild;
                var srcNode = img.Attributes["src"];
                var fileNameNode = img.Attributes["data-filename"];

                // The HTML editor creates base64 DataURIs which we'll have to convert to image files on disk
                if (srcNode != null && fileNameNode != null)
                {
                    var base64Match = base64Regex.Match(srcNode.Value);
                    if (base64Match.Success)
                    {
                        var bytes = Convert.FromBase64String(base64Match.Groups["base64"].Value);
                        srcNode.Value = await _blog.SaveFile(bytes, fileNameNode.Value).ConfigureAwait(false);

                        img.Attributes.Remove(fileNameNode);
                        post.Content = post.Content.Replace(match.Value, img.OuterXml);
                    }
                }
            }
        }

        [Route("/Blog/Delete/{id}")]
        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _blog.GetPostById(id);

            if (existing != null)
            {
                await _blog.DeletePost(existing);
                return Redirect("/");
            }

            return NotFound();
        }

        [Route("/Blog/Comment/{postId}")]
        [HttpPost]
        public async Task<IActionResult> AddComment(string postId, Comment comment)
        {
            var post = await _blog.GetPostById(postId);

            if (!ModelState.IsValid)
            {
                return View("Post", post);
            }

            var c = new Comment
            {
                //ID = new Guid().ToString(),
                //IsAdmin = User.Identity.IsAuthenticated,
                //Content = comment.Content.Trim(),
                //Author = comment.Name,
                //Email = comment.Email
            };

            if (post == null || !PostHelper.AreCommentsOpen(post, _settings.Value.CommentsCloseAfterDays))
            {
                return NotFound();
            }
            
            // the website form key should have been removed by javascript
            // unless the comment was posted by a spam robot
            if (!Request.Form.ContainsKey("website"))
            {
                post.Comments.Add(c);
                await _blog.SavePost(post);
            }

            return Redirect(post.Link + "#" + comment.ID);
        }

        [Route("/Blog/Comment/{postId}/{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(string postId, string commentId)
        {
            var post = await _blog.GetPostById(postId);

            if (post == null)
            {
                return NotFound();
            }

            var comment = post.Comments.FirstOrDefault(c => c.ID.Equals(commentId, StringComparison.OrdinalIgnoreCase));

            if (comment == null)
            {
                return NotFound();
            }

            post.Comments.Remove(comment);
            await _blog.SavePost(post);

            return Redirect(post.Link + "#comments");
        }
    }
}