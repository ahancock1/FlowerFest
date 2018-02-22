// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Models;
    using Extensions;

    public class BlogRepository : Repository<BlogPost>, IBlogRepository
    {
        public BlogRepository(string path)
            : base(path)
        {
        }

        public bool CreatePost(BlogPost post)
        {
            return Create(post);
        }

        public bool DeletePost(BlogPost post)
        {
            return Delete(post);
        }

        public BlogPost GetPost(Guid id)
        {
            return Retrieve(id);
        }

        public bool UpdatePost(BlogPost post)
        {
            return Update(post);
        }

        public IEnumerable<BlogPost> Search(string term)
        {
            var comparison = StringComparison.OrdinalIgnoreCase;

            return All(
                post =>
                    (post.Title.Contains(term, comparison) ||
                    post.Content.Contains(term, comparison) ||
                    post.Description.Contains(term, comparison))
                        && post.IsPublished,
                post =>
                    post.PublishedDate);
        }
    }
}