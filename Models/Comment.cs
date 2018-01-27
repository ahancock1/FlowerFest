// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Comment.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Author { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        public bool IsAdmin { get; set; }
    }
}