// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Comment.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.DTO
{
    using System;

    public class Comment
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}