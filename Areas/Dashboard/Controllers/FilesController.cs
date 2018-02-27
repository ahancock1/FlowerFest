// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   FilesController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using FlowerFest.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using ViewModels;
    
    public class FilesController : BaseController<FilesController>
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FilesController(
            IFileService fileService,
            IMapper mapper,
            ILogger<FilesController> logger)
            : base(logger)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                await _fileService.Save(file);

                return RedirectToAction("Index");
            }
            catch (NotSupportedException e)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Route("Dashboard/Files/{id}")]
        [Authorize]
        public async Task<IActionResult> Index(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var file = await _fileService.GetFile(Guid.Parse(id));

                    if (file != null)
                    {
                        var data = await _fileService.Read(file);
                        var type = await _fileService.GetContentType(file);

                        return File(data, type, id);
                    }

                    return NotFound();
                }

                return View("Index", _mapper.Map<IEnumerable<FileViewModel>>(
                    await _fileService.GetFiles()));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }
        
        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (await _fileService.Delete(Guid.Parse(id)))
                {
                    return Ok();
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