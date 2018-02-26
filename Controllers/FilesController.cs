// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   FilesController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using ViewModels;

    [Route("Api/[controller]")]
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
            var supported = new[]
            {
                ".jpg", ".jpeg", ".png"
            };

            if (!_fileService.Validate(file, supported))
            {
                return BadRequest();
            }

            try
            {
                await _fileService.Save(file);

                return RedirectToAction("Files");
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Authorize]
        public async Task<IActionResult> Files()
        {
            try
            {
                return View("Files", _mapper.Map<IEnumerable<FileDetailsViewModel>>(
                    await _fileService.GetFiles()));
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Download(string id)
        {
            throw new NotImplementedException();
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