// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   CommentViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    // TODO - Use this for the comment section
    public class CommentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
