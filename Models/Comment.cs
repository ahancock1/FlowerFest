// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   Comment.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Cryptography;
    using System.Text;

    public class Comment
    {
        [Required]
        public string ID { get; set; } = Guid.NewGuid().ToString();

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