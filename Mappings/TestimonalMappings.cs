// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonalMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using AutoMapper;
    using DTO;
    using Models;
    using ViewModels.Home;

    public class TestimonalMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<TestimonialModel, Testimonial>();
            config.CreateMap<Testimonial, TestimonialModel>();

            config.CreateMap<Testimonial, TestimonialViewModel>();
            config.CreateMap<TestimonialViewModel, Testimonial>();
        }
    }
}