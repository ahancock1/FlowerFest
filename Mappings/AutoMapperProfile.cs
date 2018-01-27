// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   AutoMapperProfile.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using AutoMapper;
    using Models;
    using ViewModels.Blog;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PostViewModel, Post>();
            CreateMap<Post, PostViewModel>();

            CreateMap<PostDetailViewModel, Post>();
            CreateMap<Post, PostDetailViewModel>();

            CreateMap<CommentViewModel, Comment>();
            CreateMap<Comment, CommentViewModel>();

            CreateMap<Testimonal, TestimonalViewModel>();
            CreateMap<TestimonalViewModel, Testimonal>();
        }
    }
}