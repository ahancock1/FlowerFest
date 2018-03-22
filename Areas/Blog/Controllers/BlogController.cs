// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Blog.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using DTO;
    using FlowerFest.Controllers;
    using FlowerFest.ViewModels.Shared;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using ViewModels;
    using ViewModels.Blog;

    [Area("Blog")]
    public class BlogController : BaseController<BlogController>
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly int _postsPerPage = int.MaxValue;
        private readonly ISectionService _sectionService;

        public BlogController(
            IBlogService blogService,
            ISectionService sectionService,
            IMapper mapper,
            ILogger<BlogController> logger)
            : base(logger)
        {
            _blogService = blogService;
            _sectionService = sectionService;
            _mapper = mapper;
        }

        [Route("/Blog/{page:int?}")]
        public async Task<IActionResult> Index([FromRoute] int page = 0)
        {
            try
            {
                return View(new BlogViewModel
                {
                    Posts = _mapper.Map<IEnumerable<PostViewModel>>(
                        await _blogService.GetPosts(_postsPerPage)),
                    HeaderSections = _mapper.Map<IEnumerable<HeaderSectionViewModel>>(
                        await _sectionService.GetSections())
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

            try
            {
                return View("Index", new BlogViewModel
                {
                    Posts = _mapper.Map<IEnumerable<PostViewModel>>(
                        await _blogService.GetPostsByCategory(category)),
                    HeaderSections = _mapper.Map<IEnumerable<HeaderSectionViewModel>>(
                        await _sectionService.GetSections())
                });
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Route("/Blog/{slug?}")]
        public async Task<IActionResult> Post(string slug = "")
        {
            if (string.IsNullOrEmpty(slug))
            {
                return NotFound();
            }

            try
            {
                var post = await _blogService.GetPostBySlug(slug);
                if (post == null)
                {
                    return NotFound();
                }
                
                return View(new BlogPostViewModel
                {
                    Post = _mapper.Map<PostViewModel>(post),
                    HeaderSections = _mapper.Map<IEnumerable<HeaderSectionViewModel>>(
                        await _sectionService.GetSections())
                });
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
            try
            {
                var post = await _blogService.GetPostById(Guid.Parse(postId));
                if (post == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return View("Post", _mapper.Map<BlogPostViewModel>(post));
                }

                if (!Request.Form.ContainsKey("website"))
                {
                    await _blogService.AddComment(Guid.Parse(postId), _mapper.Map<Comment>(comment));
                }

                return Redirect($"Blog/{post.Slug}#{comment.Id}");
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
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