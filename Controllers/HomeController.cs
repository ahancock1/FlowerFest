﻿// -----------------------------------------------------------------------
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
    using FlowerFest.Models;
    using System.Threading.Tasks;
    using System.IO;
    using System.Collections.Generic;
    using System.Net.Mail;
    using Microsoft.Extensions.Configuration;
    using System.Net;
    using FlowerFest.Services.Interfaces;

    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<BlogSettings> _settings;

        private readonly IMailService _mailService;

        public HomeController(IMailService mailService, IOptionsSnapshot<BlogSettings> settings)
        {
            _mailService = mailService;
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

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", model);
            }

            var from = model.Email;
            var to = "a.hancock@hotmail.co.uk";
            var name = new
            {
                First = model.FirstName,
                Last = model.LastName
            };
            var message = model.Message;

            _mailService.Send(from, to, message, $"FLOWERFEST - {name.First} {name.Last} sent you a message");

            return Index();
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats" },
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}