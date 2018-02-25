// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System.Collections.Generic;
    using Home;

    public class HomeViewModel
    {
        public IEnumerable<TestimonalViewModel> Testimonals { get; set; }
        public IEnumerable<BlogPostViewModel> RecentPosts { get; set; }
        public IEnumerable<SectionViewModel> Sections { get; set; }
    }
}