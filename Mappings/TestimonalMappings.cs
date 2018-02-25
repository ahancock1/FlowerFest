// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   AutoMapperProfile.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using AutoMapper;
    using DTO;
    using Models;
    using ViewModels.Blog;

    public class TestimonalMappings : Profile
    {
        public TestimonalMappings()
        {
            CreateMap<TestimonalModel, Testimonal>();
            CreateMap<Testimonal, TestimonalModel>();

            CreateMap<Testimonal, TestimonalViewModel>();
            CreateMap<TestimonalViewModel, Testimonal>();
        }
    }
}