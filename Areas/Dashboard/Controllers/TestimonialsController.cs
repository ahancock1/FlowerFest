// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonialsController.cs can not be copied and/or distributed without the express
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
    using ViewModels.Testimonials;

    [Area("Dashboard")]
    [Authorize]
    public class TestimonialsController : BaseController<TestimonialsController>
    {
        private readonly IMapper _mapper;
        private readonly ITestimonalService _service;

        public TestimonialsController(
            ITestimonalService service,
            IMapper mapper,
            ILogger<TestimonialsController> logger)
            : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(_mapper.Map<IEnumerable<TestimonialViewModel>>(
                    await _service.GetTestimonials()));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        public async Task<IActionResult> Create()
        {
            return View(new CreateTestimonialViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await _service.CreateTestimonial(_mapper.Map<Testimonial>(model)))
                {
                    return RedirectToAction("Index");
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }
        
        public async Task<IActionResult> Delete(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                if (await _service.DeleteTestimonial(Guid.Parse(id)))
                {
                    return RedirectToAction("Index");
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
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var partner = await _service.GetTestimonialById(Guid.Parse(id));
                if (partner == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<EditTestimonialViewModel>(partner));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTestimonialViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await _service.UpdateTestimonial(
                    _mapper.Map<Testimonial>(model)))
                {
                    return RedirectToAction("Index");
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