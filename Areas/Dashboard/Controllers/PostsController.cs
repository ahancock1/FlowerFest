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
    using FlowerFest.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using ViewModels.Posts;

    [Area("Dashboard")]
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
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
            }

            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }

                var post = _mapper.Map<EditViewModel>(
                    await _blogService.GetPostById(Guid.Parse(id)));

                return View(post);
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [HttpPost]
        private async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }
    }
}