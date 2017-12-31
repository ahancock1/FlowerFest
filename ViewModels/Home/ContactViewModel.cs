﻿// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   ContactViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ContactViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}