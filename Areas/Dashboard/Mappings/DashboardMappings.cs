// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   DashboardMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            config.CreateMap<BlogPost, CreatePostViewModel>()
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom(
                        src => string.Join(", ", src.Categories)));
            config.CreateMap<CreatePostViewModel, BlogPost>()
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom(
                        src => SplitCategories(src.Categories)))
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(
                        src => src.Title.Trim()))
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(
                        src => src.Content.Trim()))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(
                        src => src.Description.Trim()));

            config.CreateMap<BlogPost, EditPostViewModel>()
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom(
                        src => string.Join(", ", src.Categories)));
            config.CreateMap<EditPostViewModel, BlogPost>()
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom(
                        src => SplitCategories(src.Categories)))
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(
                        src => src.Title.Trim()))
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(
                        src => src.Content.Trim()))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(
                        src => src.Description.Trim()));

            config.CreateMap<BlogPost, PostViewModel>();
            config.CreateMap<PostViewModel, BlogPost>();
        }

        private IEnumerable<string> SplitCategories(string text)
        {
            return text.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim().ToLowerInvariant()).ToList();
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