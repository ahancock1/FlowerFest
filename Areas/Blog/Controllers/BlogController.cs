// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeController.cs can not be copied and/or distributed without the express
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
    using FlowerFest.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Services.Interfaces;
    using ViewModels.Blog;
    using PostViewModel = ViewModels.Blog.PostViewModel;

    [Area("Blog")]
    public class BlogController : BaseController<BlogController>
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly int _postsPerPage = int.MaxValue;
        private readonly ISectionService _sectionService;
        private readonly IOptionsSnapshot<BlogSettings> _settings;

        public BlogController(
            IBlogService blogService,
            ISectionService sectionService,
            IOptionsSnapshot<BlogSettings> settings,
            IMapper mapper,
            ILogger<BlogController> logger)
            : base(logger)
        {
            _blogService = blogService;
            _settings = settings;
            _mapper = mapper;
            _sectionService = sectionService;
        }

        [Route("/Blog/{page:int?}")]
        public async Task<IActionResult> Index([FromRoute] int page = 0)
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Blog";

            try
            {
                return View("Index", new BlogViewModel
                {
                    Posts = _mapper.Map<IEnumerable<PostViewModel>>(
                        await _blogService.GetPosts(_postsPerPage)),
                    Sections = _mapper.Map<IEnumerable<SectionViewModel>>(
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

            ViewData["Title"] = $"{_settings.Value.Name} - Blog";

            try
            {
                return View("Index", new BlogViewModel
                {
                    Posts = _mapper.Map<IEnumerable<PostViewModel>>(
                        await _blogService.GetPostsByCategory(category)),
                    Sections = _mapper.Map<IEnumerable<SectionViewModel>>(
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

            var post = await _blogService.GetPostBySlug(slug);
            if (post == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"{_settings.Value.Name} - {post.Title}";

            try
            {
                var viewmodel = _mapper.Map<BlogPostViewModel>(post);
                viewmodel.Sections = _mapper.Map<IEnumerable<SectionViewModel>>(
                    await _sectionService.GetSections());

                return View("Post", viewmodel);
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
                return View("Post", _mapper.Map<PostViewModel>(post));
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