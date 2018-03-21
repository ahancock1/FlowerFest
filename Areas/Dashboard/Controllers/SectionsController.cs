// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SectionsController.cs can not be copied and/or distributed without the express
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
    using ViewModels.Sections;

    [Area("Dashboard")]
    [Authorize]
    public class SectionsController : BaseController<SectionsController>
    {
        private readonly IMapper _mapper;
        private readonly ISectionService _service;

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
                return View("Index", _mapper.Map<IEnumerable<SectionViewModel>>(
                    await _service.GetSections()));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        public async Task<IActionResult> Create()
        {
            return View(new CreateSectionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await _service.CreateSection(_mapper.Map<Section>(model)))
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
                if (await _service.DeleteSection(Guid.Parse(id)))
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
                var partner = await _service.GetSection(Guid.Parse(id));
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