// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogPost.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.DTO
{
    using System;
    using System.Collections.Generic;

    public class BlogPost
    {
        public string Slug { get; set; }
        public string Spotlight { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<string> Categories { get; set; }
        public Guid Id { get; set; }
        public bool IsPublished { get; set; }
        public string Author { get; set; }
    }
}