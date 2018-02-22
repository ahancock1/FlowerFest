// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IBlogRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Models;

    public interface IBlogRepository
    {
        bool CreatePost(BlogPost post);
        bool UpdatePost(BlogPost post);
        bool DeletePost(BlogPost post);
        BlogPost GetPost(Guid id);
        IEnumerable<BlogPost> Search(string term);
    }
}