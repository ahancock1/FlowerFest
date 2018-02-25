// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   CommentMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using AutoMapper;
    using DTO;
    using Models;
    using ViewModels.Blog;

    public class CommentMappings : Profile
    {
        public CommentMappings()
        {
            CreateMap<CommentModel, Comment>();
            CreateMap<Comment, CommentModel>();

            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentViewModel, Comment>();
        }
    }
}