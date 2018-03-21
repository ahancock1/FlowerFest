// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   EditPostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.ViewModels.Sections
{
    using System.ComponentModel.DataAnnotations;

    public class EditSectionViewModel
    {
        public int Index { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsPublished { get; set; }
        
        public string Id { get; set; }
        
        public string Background { get; set; }

        public string Tag { get; set; }
    }
}