// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   EditPartnerViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.ViewModels.Partners
{
    using System.ComponentModel.DataAnnotations;

    public class EditPartnerViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string Name { get; set; }
    }
}