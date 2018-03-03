// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   CommentMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using System.Security.Cryptography;
    using System.Text;
    using Areas.Blog.ViewModels.Blog;
    using AutoMapper;
    using DTO;
    using Models;

    public class CommentMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<CommentModel, Comment>();
            config.CreateMap<Comment, CommentModel>();

            config.CreateMap<Comment, CommentViewModel>();
            config.CreateMap<CommentViewModel, Comment>();

            config.CreateMap<CommentViewModel, Comment>()
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(
                        src => src.Email.Trim().ToLowerInvariant()))
                .ForMember(
                    dest => dest.Gravatar,
                    opt => opt.MapFrom(
                        src => GetGravatar(src.Email.Trim().ToLowerInvariant())));
            config.CreateMap<Comment, CommentViewModel>();
        }

        private string GetGravatar(string email)
        {
            using (var md5 = MD5.Create())
            {
                var input = Encoding.UTF8.GetBytes(email);
                var bytes = md5.ComputeHash(input);

                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                var gavatar = builder.ToString().ToLowerInvariant();

                return $"https://www.gravatar.com/avatar/{gavatar}?s=60&d=blank";
            }
        }
    }
}