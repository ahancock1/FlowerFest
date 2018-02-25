// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   EditPostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class EditPostViewModel
    {
        [DataType(DataType.Upload)]
        public IFormFile Spotlight { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsPublished { get; set; } = true;

        public string Categories { get; set; }
    }
}