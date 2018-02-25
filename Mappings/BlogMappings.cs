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
    using System.Text.RegularExpressions;
    using AutoMapper;
    using DTO;
    using Models;
    using ViewModels.Blog;

    public class BlogMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<BlogPost, BlogPostModel>();
            config.CreateMap<BlogPostModel, BlogPost>();

            config.CreateMap<BlogPost, BlogPostViewModel>();
            config.CreateMap<BlogPostViewModel, BlogPost>();

            config.CreateMap<BlogPost, PostDetailViewModel>()
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(
                        src => CompileContent(src.Content)));
            config.CreateMap<PostDetailViewModel, BlogPost>();

            config.CreateMap<BlogPost, EditPostViewModel>()
                .ForMember(
                    dest => dest.Spotlight,
                    opt => opt.Ignore())
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
        }

        private IEnumerable<string> SplitCategories(string text)
        {
            return text.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim().ToLowerInvariant()).ToList();
        }

        private string CompileContent(string content)
        {
            // Set up lazy loading of images/iframes
            content = content.Replace(" src=\"",
                " src=\"data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==\" data-src=\"");

            // Youtube content embedded using this syntax: [youtube:xyzAbc123]
            var video =
                "<div class=\"video\">" +
                    "<iframe width=\"560\" height=\"315\" " +
                        "title=\"YouTube embed\" src=\"about:blank\" " +
                        "data-src=\"https://www.youtube-nocookie.com/embed/{0}?modestbranding=1&amp;hd=1&amp;rel=0&amp;theme=light\" " +
                        "allowfullscreen>" +
                    "</iframe>" +
                "</div>";
            content = Regex.Replace(content, @"\[youtube:(.*?)\]", m => string.Format(video, m.Groups[1].Value));

            return content;
        }
    }
}