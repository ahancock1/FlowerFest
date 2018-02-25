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
    using DTO;

    public interface ITestimonalService
    {
        Task<IEnumerable<Testimonal>> GetTestimonals();
        Task<bool> Create(Testimonal testimonal);
    }
}