// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonialViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Home
{
    using System;

    public class TestimonialViewModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
        public string Author { get; set; }
        public string Place { get; set; }
    }
}