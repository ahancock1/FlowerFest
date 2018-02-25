// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   DashboardController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class DashboardController : BaseController<DashboardController>
    {
        public DashboardController(ILogger<DashboardController> logger)
            : base(logger)
        {

        }

        public async Task<IActionResult> Index()
        {
            // Profile information
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