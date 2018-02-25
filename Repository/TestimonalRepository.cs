// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonalRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using Interfaces;
    using Models;

    public class TestimonalRepository : Repository<TestimonalModel>, ITestimonalRepository
    {
        public TestimonalRepository(string path)
            : base(path)
        {
        }
    }
}