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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using ViewModels.Partners;

    [Area("Dashboard")]
    [Authorize]
    public class PartnersController : BaseController<PartnersController>
    {
        private readonly IPartnerService _service;
        private readonly IMapper _mapper;

        public PartnersController(
            IPartnerService service,
            IMapper mapper,
            ILogger<PartnersController> logger)
            : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View("Index", _mapper.Map<IEnumerable<PartnerViewModel>>(
                    await _service.GetPartners()));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        public async Task<IActionResult> Create()
        {
            return View(new CreatePartnerViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePartnerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (await _service.CreatePartner(_mapper.Map<Partner>(model)))
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
                if (await _service.DeletePartner(Guid.Parse(id)))
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
                var partner = await _service.GetPartner(Guid.Parse(id));
                if (partner == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<EditPartnerViewModel>(partner));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPartnerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if(await _service.UpdatePartner(
                    _mapper.Map<Partner>(model)))
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