// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   ITestimonalService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITestimonalService
    {
        Task<IEnumerable<Testimonal>> All();
    }
}