// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FlowerFest.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Area("Dashboard")]
    public class HomeController : BaseController<HomeController>
    {
        public HomeController(ILogger<HomeController> logger)
            : base(logger)
        {
        }

        public async Task<IActionResult> Index()
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