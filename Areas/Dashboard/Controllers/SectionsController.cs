// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeController.cs can not be copied and/or distributed without the express
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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using ViewModels.Sections;
    using ViewModels.Testimonials;

    [Area("Dashboard")]
    public class SectionsController : BaseController<SectionsController>
    {
        private readonly ISectionService _service;
        private readonly IMapper _mapper;

        public SectionsController(
            ISectionService service,
            IMapper mapper,
            ILogger<SectionsController> logger)
            : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View("Index", _mapper.Map<IEnumerable<Testimonial>>(
                    await _service.GetSections()));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        public async Task<IActionResult> Create()
        {
            return View(new SectionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await _service.CreateSection(_mapper.Map<Section>(model)))
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
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                if (await _service.DeleteSection(Guid.Parse(id)))
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

        public async Task<IActionResult> Edit(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var partner = _service.GetSection(Guid.Parse(id));
                if (partner == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<EditSectionViewModel>(partner));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await _service.UpdateSection(
                    _mapper.Map<Section>(model)))
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