// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogPost.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Models
{
    using System;
    using System.Collections.Generic;
    using Repository;

    public class BlogPost : IEntity
    {
        public string Slug { get; set; }
        public string Spotlight { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public IList<Comment> Comments { get; set; } = new List<Comment>();
        public IList<string> Categories { get; set; } = new List<string>();
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsPublished { get; set; }
    }
}