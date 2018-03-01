// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   DetailsPostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class DetailsViewModel
    {
        [Required]
        public string Spotlight { get; set; }
    }
}