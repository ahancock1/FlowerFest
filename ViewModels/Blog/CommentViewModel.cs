// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   CommentViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels
{
    using FlowerFest.Helpers;
    using FlowerFest.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentViewModel
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Content { get; set; }

        public Post Post { get; set; }

        public bool AreCommentsOpen(int days)
        {
            return PostHelper.AreCommentsOpen(Post, days);
        }
    }
}
