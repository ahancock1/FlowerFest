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

    public class TestimonalMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<TestimonalModel, Testimonal>();
            config.CreateMap<Testimonal, TestimonalModel>();

            config.CreateMap<Testimonal, TestimonalViewModel>();
            config.CreateMap<TestimonalViewModel, Testimonal>();
        }
    }
}