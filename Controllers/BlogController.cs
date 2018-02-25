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
    using System.Threading.Tasks;
    using AutoMapper;
    using DTO;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Services.Interfaces;
    using ViewModels.Blog;

    // TODO - Add area
    public class BlogController : BaseController<BlogController>
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly int _postsPerPage = int.MaxValue;
        private readonly IOptionsSnapshot<BlogSettings> _settings;

        public BlogController(IBlogService blogService, IOptionsSnapshot<BlogSettings> settings,
            IMapper mapper, ILogger<BlogController> logger)
            : base(logger)
        {
            _blogService = blogService;
            _settings = settings;
            _mapper = mapper;
        }

        [Route("/Blog/{page:int?}")]
        public async Task<IActionResult> Index([FromRoute] int page = 0)
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Blog";

            try
            {
                return View("Index", new BlogViewModel
                {
                    Posts = _mapper.Map<IEnumerable<BlogPostViewModel>>(
                        await _blogService.GetPosts(_postsPerPage))
                });
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Route("/Blog/Category/{category}/{page:int?}")]
        public async Task<IActionResult> Category(string category, int page = 0)
        {
            if (string.IsNullOrEmpty(category))
            {
                return NotFound();
            }

            ViewData["Title"] = $"{_settings.Value.Name} - Blog";

            try
            {
                return View("Index", new BlogViewModel
                {
                    Posts = _mapper.Map<IEnumerable<BlogPostViewModel>>(
                        await _blogService.GetPostsByCategory(category))
                });
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Route("/Blog/{slug?}")]
        public async Task<IActionResult> Post(string slug)
        {
            var post = await _blogService.GetPostBySlug(slug);
            if (post == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"{_settings.Value.Name} - {post.Title}";

            try
            {
                return View("PostDetail", _mapper.Map<PostDetailViewModel>(post));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Route("/Blog/Edit/{id?}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Edit", new EditPostViewModel());
            }

            var post = await _blogService.GetPostById(Guid.Parse(id));
            if (post == null)
            {
                return NotFound();
            }

            try
            {
                return View("Edit", _mapper.Map<EditPostViewModel>(post));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Route("/Blog/Update")]
        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(EditPostViewModel edit)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", edit);
            }

            try
            {
                BlogPost post;
                if (string.IsNullOrEmpty(edit.Id))
                {
                    post = await _blogService.CreatePost(
                        _mapper.Map<BlogPost>(edit), edit.Spotlight);
                }
                else
                {
                    post = await _blogService.UpdatePost(
                        _mapper.Map<BlogPost>(edit));
                }

                if (post != null)
                {
                    return Redirect($"Blog/{post.Slug}");
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Route("/Blog/Delete/{id}")]
        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (await _blogService.DeletePost(Guid.Parse(id)))
                {
                    return Redirect("Blog");
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Route("/Blog/Comment/{postId}")]
        [HttpPost]
        public async Task<IActionResult> AddComment(string postId, CommentViewModel comment)
        {
            var id = Guid.Parse(postId);

            var post = await _blogService.GetPostById(id);

            if (!ModelState.IsValid)
            {
                return View("PostDetail", _mapper.Map<PostDetailViewModel>(post));
            }

            if (post == null)
            {
                return NotFound();
            }

            if (!Request.Form.ContainsKey("website"))
            {
                await _blogService.AddComment(id, _mapper.Map<Comment>(comment));
            }

            return Redirect($"Blog/{post.Slug}#{comment.Id}");
        }

        [Route("/Blog/Comment/{postId}/{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(string postId, string commentId)
        {
            try
            {
                var post = await _blogService.DeleteComment(Guid.Parse(postId), Guid.Parse(commentId));

                if (post != null)
                {
                    return Redirect($"Blog/{post.Slug}#comments");
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }
    }
}