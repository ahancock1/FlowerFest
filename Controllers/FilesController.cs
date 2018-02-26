// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   FilesController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;

    [Route("Api/[controller]")]
    public class FilesController : BaseController<FilesController>
    {
        private readonly IFileService _fileService;

        public FilesController(
            IFileService fileService,
            ILogger<FilesController> logger)
            : base(logger)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            throw new NotImplementedException();
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