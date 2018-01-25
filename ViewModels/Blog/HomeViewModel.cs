﻿// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IList<TestimonalViewModel> Testimonals { get; set; }

        public IList<PostViewModel> RecentPosts { get; set; }
    }
}