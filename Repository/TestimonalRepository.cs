// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonalRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System;
    using Interfaces;
    using Models;

    public class TestimonalRepository : Repository<Testimonal>, ITestimonalRepository
    {
        public TestimonalRepository(string path)
            : base(path)
        {
        }

        public bool CreateTestimonal(Testimonal testimonal)
        {
            return Create(testimonal);
        }

        public bool UpdateTestimonal(Testimonal testimonal)
        {
            return Update(testimonal);
        }

        public bool DeleteTestimonal(Testimonal testimonal)
        {
            return Delete(testimonal);
        }

        public Testimonal GetTestimonal(Guid id)
        {
            return Retrieve(id);
        }
    }
}