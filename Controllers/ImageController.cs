using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FlowerFest.Controllers
{
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class ImageController : BaseController<ImageController>
    {
        private readonly IFileService _fileService;

        protected ImageController(IFileService fileService, ILogger<ImageController> logger) 
            : base(logger)
        {
            _fileService = fileService;
        }
        
        [Route("api/images/upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            throw new NotImplementedException();
            //var supported = new[]
            //{
            //    ".jpg", ".jpeg", ".png"
            //};

            //if (!_fileService.Validate(file, supported))
            //{
            //    return BadRequest("Unsupported media type");
            //}

            //await _fileService.Save(file);

            //// Check if the request contains multipart/form-data.
            //if (!Request.Content.IsMimeMultipartContent("form-data"))
            //{
            //    return BadRequest("Unsupported media type");
            //}
            //try
            //{
            //    var provider = new CustomMultipartFormDataStreamProvider(workingFolder);
            //    await Request.Content.ReadAsMultipartAsync(provider);

            //    var file = provider.FileData.First();
            //    var fileInfo = new FileInfo(file.LocalFileName);

            //    return Ok(new { location = $"/images/{DateTime.Today.ToString("ddMMyyyy")}/{fileInfo.Name}" });
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.GetBaseException().Message);
            //}
        }
    }
}
