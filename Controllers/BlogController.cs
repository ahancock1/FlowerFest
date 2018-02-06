// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using AutoMapper;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;
    using Microsoft.Net.Http.Headers;
    using Models;
    using Services;
    using ViewModels;
    using ViewModels.Blog;

    public class BlogController : Controller
    {
        private readonly IBlogService _blog;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<BlogSettings> _settings;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BlogController(IBlogService blog, IOptionsSnapshot<BlogSettings> settings,
            IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _blog = blog;
            _settings = settings;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("/Blog/{page:int?}")]
        public async Task<IActionResult> Index([FromRoute] int page = 0)
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Blog";

            var viewmodel = new BlogViewModel
            {
                Posts = new List<PostViewModel>()
            };

            var items = _settings.Value.PostsPerPage;

            foreach (var post in await _blog.GetPosts(items))
            {
                var p = _mapper.Map<PostViewModel>(post);
                p.Author = _settings.Value.Owner;

                viewmodel.Posts.Add(p);
            }

            return View("Index", viewmodel);
        }

        [Route("/Blog/Category/{category}/{page:int?}")]
        public async Task<IActionResult> Category(string category, int page = 0)
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Blog";

            var viewmodel = new BlogViewModel();
            
            foreach (var post in await _blog.GetPostsByCategory(category))
            {
                var p = _mapper.Map<PostViewModel>(post);
                p.Author = _settings.Value.Owner;

                viewmodel.Posts.Add(p);
            }

            return View("Index", viewmodel);
        }

        [Route("/Blog/{slug?}")]
        public async Task<IActionResult> Post(string slug)
        {
            var post = await _blog.GetPostBySlug(slug);

            if (post != null)
            {
                ViewData["Title"] = $"{_settings.Value.Name} - {post.Title}";

                var viewmodel = _mapper.Map<PostDetailViewModel>(post);
                viewmodel.Author = _settings.Value.Owner;
                
                return View("PostDetail", viewmodel);
            }
            
            return NotFound();
        }


        // Update below

        [Route("/Blog/Edit/{id?}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new EditPostViewModel());
            }

            var post = await _blog.GetPostById(id);

            if (post != null)
            {
                var edit = _mapper.Map<EditPostViewModel>(post);

                //edit.Spotlight = await OpenSpotlight(post.Spotlight);

                return View(edit);
            }

            return NotFound();
        }

        private Task<IFormFile> OpenSpotlight(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                if (Directory.Exists(uploads))
                {
                    var filepath = Path.Combine(uploads, filename);

                    // Check file exists

                    using (var stream = new FileStream(filepath, FileMode.Open))
                    {
                        var file = new FormFile(stream, 0, stream.Length, "spotlight", filename);

                        return Task.FromResult((IFormFile) file);
                    }
                }
            }

            return Task.FromResult(default(IFormFile));
        }

        private async Task<string> SaveSpotlight(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                
                var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                var filepath = Path.Combine(uploads, filename);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return filename;
            }

            return null;
        }

        private bool ValidateSpotlight(IFormFile file)
        {
            if (file == null || file.Length == 0) return false;

            var accepted = new[]
            {
                "jpg",
                "jpeg",
                "png"
            };

            var extension = Path.GetExtension(file.FileName);

            foreach (var type in accepted)
            {
                if (extension.ToLower().Contains(type))
                {
                    return true;
                }
            }

            return false;
        }
        
        [Route("/Blog/Update")]
        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(EditPostViewModel edit)
        {
            if (!ModelState.IsValid || !ValidateSpotlight(edit.Spotlight))
            {
                return View("Edit", edit);
            }
            
            var filename = await SaveSpotlight(edit.Spotlight);
            
            var post = _mapper.Map<Post>(edit);

            if (!string.IsNullOrEmpty(filename))
            {
                post.Spotlight = filename;
            }
            
            var existing = await _blog.GetPostById(post.Id) ?? post;
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
            existing.Description = post.Description.Trim();

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

                var viewmodel = new BlogViewModel
                {
                    Posts = new List<PostViewModel>()
                };

                var items = _settings.Value.PostsPerPage;

                foreach (var post in await _blog.GetPosts(items))
                {
                    var p = _mapper.Map<PostViewModel>(post);
                    p.Author = _settings.Value.Owner;

                    viewmodel.Posts.Add(p);
                }

                return View("Index", viewmodel);
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

            var c = new Comment();

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

            return Redirect(post.Link + "#" + comment.Id);
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

            var comment = post.Comments.FirstOrDefault(c => c.Id.Equals(commentId, StringComparison.OrdinalIgnoreCase));

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