// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   DashboardController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Interfaces;
    using ViewModels;

    public class DashboardController : BaseController<DashboardController>
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public DashboardController(
            IFileService fileService,
            IMapper mapper,
            ILogger<DashboardController> logger)
            : base(logger)
        {
            _fileService = fileService;
            _mapper = mapper;
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