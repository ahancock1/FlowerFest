// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.Controllers
{
    using System;
    using FlowerFest.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Area("Dashboard")]
    public class SectionsController : BaseController<SectionsController>
    {
        public SectionsController(ILogger<SectionsController> logger)
            : base(logger)
        {
        }

        public IActionResult Index()
        {
            try
            {
                return View("Index");
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }
    }
}