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
    using Models;
    using Repository.Interfaces;

    public interface IBlogService
    {
        IEnumerable<BlogPost> GetPosts(int count, int skip = 0);
        IEnumerable<BlogPost> GetPostsByCategory(string category);
        BlogPost GetPostBySlug(string slug);
        BlogPost GetPostById(Guid id);
        IEnumerable<string> GetCategories();
        IEnumerable<BlogPost> Search(string term);
    }

    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;

        public BlogService(IBlogRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BlogPost> GetPosts(int count, int skip = 0)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> GetPostsByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public BlogPost GetPostBySlug(string slug)
        {
            throw new NotImplementedException();
        }

        public BlogPost GetPostById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> Search(string term)
        {
            throw new NotImplementedException();
        }
    }
}