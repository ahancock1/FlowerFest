﻿// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   CreatePostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.ViewModels.Testimonials
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTestimonialViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Place { get; set; }
    }
}