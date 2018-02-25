// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   CommentModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Models
{
    using System;
    using Repository;

    public class CommentModel : IEntity
    {
        public string Author { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public Guid Id { get; set; }
    }
}