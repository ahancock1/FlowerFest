// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   HomeController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<BlogSettings> _settings;

        public HomeController(IOptionsSnapshot<BlogSettings> settings)
        {
            _settings = settings;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = _settings.Value.Name;
            ViewData["Description"] = _settings.Value.Description;

            ViewData["Event"] = "13-17 JUNE 2018";
            ViewData["Location"] = "CHRISTCHURCH, DORSET";

            return View("Views/Home/Index.cshtml");
        }

        public IActionResult About()
        {
            ViewData["Title"] = $"{_settings.Value.Name} - About";
            ViewData["Description"] = _settings.Value.Description;

            var about = new AboutViewModel
            {
                Contents = new[]
                {
                    new SectionContentViewModel
                    {
                        Content = "My inspiration came after a visit i made to the annual flower festival in Girona, Spain."
                    },
                    new SectionContentViewModel
                    {
                        Content = @"'Temps de Flors' has grown year on year (now 61 years), and is the biggest revenue provider for Girona. The streets are lined with retailers and businesses showcasing their own creativity and ingenuity. From a small plant pot to the more elaborate window display of floral art, it is a feast for the eyes and senses. With local participation being key to its success it has become known as a commuinity with floaral focus."
                    },
                    new SectionContentViewModel
                    {
                        Content = @"Walking the streets of Girona, it dawned on me that Christchurch would be the perfect venue for our own flower festival in Dorset, and give an opportunity to focus on an area of health that is proven to improve by working with nature, the area of, Mental Health and Well-being."
                    }
                }
            };

            return View(about);
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Contact";
            ViewData["Description"] = _settings.Value.Description;

            return View();
        }

        public IActionResult Error()
        {
            ViewData["Title"] = $"{_settings.Value.Name} - Error";
            ViewData["Description"] = _settings.Value.Description;

            return View();
        }
    }
}