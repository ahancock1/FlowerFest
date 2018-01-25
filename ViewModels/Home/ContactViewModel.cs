// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   ContactViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}