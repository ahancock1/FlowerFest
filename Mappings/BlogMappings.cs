// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DTO;
    using Models;
    using ViewModels.Blog;

    public class BlogMappings : Profile
    {
        public BlogMappings()
        {
            CreateMap<BlogPost, BlogPostModel>();
            CreateMap<BlogPostModel, BlogPost>();

            CreateMap<BlogPost, BlogPostViewModel>();
            CreateMap<BlogPostViewModel, BlogPost>();

            CreateMap<BlogPost, PostDetailViewModel>();
            CreateMap<PostDetailViewModel, BlogPost>();

            CreateMap<BlogPost, EditPostViewModel>()
                .ForMember(
                    dest => dest.Spotlight,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom(
                            src => string.Join(", ", src.Categories)));
            CreateMap<EditPostViewModel, BlogPost>()
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
        }

        private IEnumerable<string> SplitCategories(string text)
        {
            return text.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim().ToLowerInvariant()).ToList();
        }
    }
}