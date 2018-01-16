// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonalService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces;
    using Models;

    public class TestimonalService : ITestimonalService
    {
        public Task<IEnumerable<Testimonal>> All()
        {
            IEnumerable<Testimonal> posts = new List<Testimonal>(new[]
            {
                new Testimonal
                {
                    Author = "Tim Lloyd",
                    Place = "Captain's Club Hotel",
                    Content =
                        "The FlowerFest 18 is a very exciting prospect for Christchurch and the fact that it is being centred around the Priory Church in conjunction with the Priory Music Festival will give this new event the exposure it deserves during it’s inaugural year. As a neighbourhood business we are proud to be supporters."
                },
                new Testimonal
                {
                    Author = "Tom Le Mesurier",
                    Place = "Flora Direct",
                    Content =
                        "We look forward to supporting FF18. Growing up in Christchurch it will be a fantastic to see it in bloom."
                }
            });

            return Task.FromResult(posts);
        }
    }
}