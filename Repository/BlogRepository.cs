// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using Interfaces;
    using Models;

    public class BlogRepository : BaseRepository<BlogPostModel>, IBlogRepository
    {
        public BlogRepository(string path)
            : base(path)
        {
        }
    }
}