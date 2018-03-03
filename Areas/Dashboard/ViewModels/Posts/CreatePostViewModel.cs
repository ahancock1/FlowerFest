// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   CreatePostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePostViewModel
    {
        [Required]
        public string Spotlight { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsPublished { get; set; }

        public string Categories { get; set; }
    }
}