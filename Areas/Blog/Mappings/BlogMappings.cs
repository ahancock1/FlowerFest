// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Blog.Mappings
{
    using AutoMapper;
    using DTO;
    using FlowerFest.Mappings;
    using ViewModels;
    using ViewModels.Blog;

    public class BlogMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<BlogPost, PostViewModel>();
            config.CreateMap<PostViewModel, BlogPost>();

            config.CreateMap<Comment, CommentViewModel>();
            config.CreateMap<CommentViewModel, Comment>();

            config.CreateMap<BlogPost, BlogPostViewModel>();
            config.CreateMap<BlogPostViewModel, BlogPost>();

        }
    }
}