// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BaseController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class BaseController<T> : Controller where T : Controller
    {
        protected readonly ILogger Logger;

        protected BaseController(ILogger<T> logger)
        {
            Logger = logger;
        }

        protected ObjectResult ServerError(Exception e)
        {
            Logger.LogCritical(e.Message, e);
            return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
        }
    }
}