// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SectionController.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Controllers
{
    using Microsoft.Extensions.Logging;

    public class SectionController : BaseController<SectionController>
    {
        public SectionController(ILogger<SectionController> logger) 
            : base(logger)
        {

        }
    }
}