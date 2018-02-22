// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Services;
    using Services.Interfaces;
    using ViewModels.Blog;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IOldBlogService _blog;

        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<BlogSettings> _settings;
        private readonly ITestimonalService _testimonals;

        public HomeController(
            IMailService mailService,
            ITestimonalService testimonals,
            IOldBlogService blog,
            IOptionsSnapshot<BlogSettings> settings,
            IMapper mapper)
        {
            _mailService = mailService;
            _settings = settings;
            _testimonals = testimonals;
            _blog = blog;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _settings.Value.Name;

            var viewmodel = new HomeViewModel
            {
                RecentPosts = new List<PostViewModel>(),
                Testimonals = new List<TestimonalViewModel>()
            };

            // Testimonals
            foreach (var testimonal in await _testimonals.All())
            {
                viewmodel.Testimonals.Add(
                    _mapper.Map<TestimonalViewModel>(testimonal));
            }

            var items = _settings.Value.PostsPerPage;

            // Posts
            foreach (var post in await _blog.GetPosts(items))
            {
                var p = _mapper.Map<PostViewModel>(post);
                p.Author = _settings.Value.Owner;

                viewmodel.RecentPosts.Add(p);
            }

            return View("Index", viewmodel);
        }

        public IActionResult Error()
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Error";

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", model);
            }

            var from = model.Email;
            var to = "a.hancock@hotmail.co.uk";
            var name = model.Name;
            var message = model.Message;
            var subject = model.Subject;

            _mailService.Send(from, to, message, subject);

            return View();
        }
    }
}