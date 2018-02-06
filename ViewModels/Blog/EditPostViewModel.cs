// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   EditPostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class EditPostViewModel
    {
        //[Required(ErrorMessage = "Please Upload a Valid Image File")]
        [DataType(DataType.Upload)]
        public IFormFile Spotlight { get; set; }

        [Required]
        public string Id { get; set; } = DateTime.UtcNow.Ticks.ToString();

        [Required]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public bool IsPublished { get; set; } = true;

        public IList<string> Categories { get; set; } = new List<string>();
        
        public string Link => $"/blog/{Slug}/";
    }
}