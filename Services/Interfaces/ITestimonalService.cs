// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   ITestimonalService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;

    public interface ITestimonalService
    {
        Task<IEnumerable<Testimonial>> GetTestimonals();
        Task<bool> CreateTestimonial(Testimonial testimonal);
        Task<bool> UpdateTestimonial(Testimonial testomonial);
        Task<bool> DeleteTestimonial(Guid id);
        Task<Testimonial> GetTestimonial(Guid id);
    }
}