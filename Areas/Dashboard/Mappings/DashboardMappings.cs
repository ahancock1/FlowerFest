// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   DashboardMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.Mappings
{
    using AutoMapper;
    using DTO;
    using FlowerFest.Mappings;
    using ViewModels.Partners;
    using ViewModels.Posts;
    using ViewModels.Sections;
    using ViewModels.Testimonials;

    public class DashboardMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            MapPartners(config);
            MapPosts(config);
            MapSections(config);
            MapTestimonials(config);
        }

        private void MapPartners(IMapperConfigurationExpression config)
        {
            config.CreateMap<Partner, CreatePartnerViewModel>();
            config.CreateMap<CreatePartnerViewModel, Partner>();

            config.CreateMap<Partner, EditPartnerViewModel>();
            config.CreateMap<EditPartnerViewModel, Partner>();
            
            config.CreateMap<Partner, PartnerViewModel>();
            config.CreateMap<PartnerViewModel, Partner>();
        }

        private void MapPosts(IMapperConfigurationExpression config)
        {
            config.CreateMap<BlogPost, CreatePostViewModel>();
            config.CreateMap<CreatePostViewModel, BlogPost>();

            config.CreateMap<BlogPost, EditPostViewModel>();
            config.CreateMap<EditPostViewModel, BlogPost>();
            
            config.CreateMap<BlogPost, PostViewModel>();
            config.CreateMap<PostViewModel, BlogPost>();
        }

        private void MapSections(IMapperConfigurationExpression config)
        {
            config.CreateMap<Section, CreateSectionViewModel>();
            config.CreateMap<CreateSectionViewModel, Section>();

            config.CreateMap<Section, EditSectionViewModel>();
            config.CreateMap<EditSectionViewModel, Section>();
            
            config.CreateMap<Section, SectionViewModel>();
            config.CreateMap<SectionViewModel, Section>();
        }

        private void MapTestimonials(IMapperConfigurationExpression config)
        {
            config.CreateMap<Testimonial, CreateTestimonialViewModel>();
            config.CreateMap<CreateTestimonialViewModel, Testimonial>();

            config.CreateMap<Testimonial, EditTestimonialViewModel>();
            config.CreateMap<EditTestimonialViewModel, Testimonial>();
            
            config.CreateMap<Testimonial, TestimonialViewModel>();
            config.CreateMap<TestimonialViewModel, Testimonial>();
        }
    }
}