// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Services.Interfaces;
    using ViewModels.Home;

    public class HomeController : BaseController<HomeController>
    {
        private readonly IBlogService _blogService;
        private readonly ITestimonalService _testimonalService;
        private readonly ISectionService _sectionService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<BlogSettings> _settings;

        private readonly int postsPerPage = 6;

        public HomeController(
            IBlogService blogService,
            ITestimonalService testimonalService,
            ISectionService sectionService,
            IMailService mailService,
            IMapper mapper,
            IOptionsSnapshot<BlogSettings> settings,
            ILogger<HomeController> logger)
            : base(logger)
        {
            _blogService = blogService;
            _testimonalService = testimonalService;
            _sectionService = sectionService;
            _mailService = mailService;
            _mapper = mapper;
            _settings = settings;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _settings.Value.Name;

            try
            {
                return View("Index", new HomeViewModel
                {
                    Testimonials = _mapper.Map<IEnumerable<TestimonialViewModel>>(
                        await _testimonalService.GetTestimonials()),
                    RecentPosts = _mapper.Map<IEnumerable<PostViewModel>>(
                        await _blogService.GetPosts(postsPerPage)),
                    Sections = _mapper.Map<IEnumerable<SectionViewModel>>(
                        await _sectionService.GetSections())
                });
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        public IActionResult Error()
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Error";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", model);
            }

            try
            {
                var from = model.Email;
                var to = "a.hancock@hotmail.co.uk";
                var name = model.Name;
                var message = model.Message;
                var subject = model.Subject;

                if (await _mailService.Send(from, to, message, subject))
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }
    }
}