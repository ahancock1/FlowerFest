﻿// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   CommentViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Blog.ViewModels.Blog
{
    using System.ComponentModel.DataAnnotations;

    public class CommentViewModel
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Content { get; set; }

        public string Id { get; set; }

        public string Gravatar { get; set; }
    }
}