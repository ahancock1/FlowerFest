// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   ITestimonalRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository.Interfaces
{
    using System;
    using Models;

    public interface ITestimonalRepository
    {
        bool CreateTestimonal(Testimonal testimonal);
        bool UpdateTestimonal(Testimonal testimonal);
        bool DeleteTestimonal(Testimonal testimonal);
        Testimonal GetTestimonal(Guid id);
    }
}