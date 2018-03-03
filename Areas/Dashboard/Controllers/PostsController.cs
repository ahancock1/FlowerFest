// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   PostsController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using DTO;
    using FlowerFest.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using ViewModels.Posts;

    [Area("Dashboard")]
    [Authorize]
    public class PostsController : BaseController<PostsController>
    {
        private readonly IMapper _mapper;
        private readonly IBlogService _blogService;

        public PostsController(
            IBlogService blogService,
            IMapper mapper,
            ILogger<PostsController> logger)
            : base(logger)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View("Index", _mapper.Map<IEnumerable<PostViewModel>>(
                    await _blogService.GetPosts(int.MaxValue)));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        public async Task<IActionResult> Details(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var post = _blogService.GetPostById(Guid.Parse(id));

                if (post == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<PostViewModel>(post));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var post = await _blogService.CreatePost(
                        _mapper.Map<BlogPost>(model));

                if (post != null)
                {
                    return Redirect("Index");
                }

                return BadRequest();

            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }

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

        public async Task<IActionResult> Edit(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }

                var post = await _blogService.GetPostById(Guid.Parse(id));
                if (post == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<EditPostViewModel>(post));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [HttpPost]
        private async Task<IActionResult> Edit(EditPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var post = await _blogService.UpdatePost(
                    _mapper.Map<BlogPost>(model));

                if (post != null)
                {
                    return Redirect("Index");
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