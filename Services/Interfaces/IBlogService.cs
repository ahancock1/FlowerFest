// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IBlogService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;
    using Microsoft.AspNetCore.Http;

    public interface IBlogService
    {
        Task<IEnumerable<BlogPost>> GetPosts(int count, int skip = 0);
        Task<IEnumerable<BlogPost>> GetPostsByCategory(string category);
        Task<BlogPost> GetPostBySlug(string slug);
        Task<BlogPost> GetPostById(Guid id);
        Task<IEnumerable<string>> GetCategories();
        Task<IEnumerable<BlogPost>> Search(string term);
        Task<BlogPost> AddComment(Guid postId, Comment comment);
        Task<BlogPost> DeleteComment(Guid postId, Guid commentId);
        Task<BlogPost> UpdatePost(BlogPost post);
        Task<BlogPost> CreatePost(BlogPost post);//, IFormFile spotlight);
        Task<bool> DeletePost(Guid id);
    }
}