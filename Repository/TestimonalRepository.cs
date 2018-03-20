// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonalRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System.IO;
    using Interfaces;
    using Models;

    public class TestimonalRepository : BaseRepository<TestimonialModel>, ITestimonalRepository
    {
        private readonly string _path;

        public TestimonalRepository(string path)
            : base(path)
        {
            _path = path;

            Seed();
        }

        private void Seed()
        {
            if (Directory.GetFiles(_path).Length > 0) return;

            Create(new TestimonialModel
            {
                Author = "Tim Lloyd",
                Content = "The FlowerFest 18 is a very exciting prospect for Christchurch and the fact that it is being centred around the Priory Church in conjunction with the Priory Music Festival will give this new event the exposure it deserves during it’s inaugural year. As a neighbourhood business we are proud to be supporters.",
                Place = "Captain's Club Hotel"
            });

            Create(new TestimonialModel
            {
                Author = "Tom Le Mesurier",
                Content = "We look forward to supporting FF18. Growing up in Christchurch it will be a fantastic to see it in bloom.",
                Place = "Flora Direct"
            });
        }
    }
}