// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System.Collections.Generic;
    using Home;

    public class BlogViewModel
    {
        public IEnumerable<BlogPostViewModel> Posts { get; set; }
        public IEnumerable<SectionViewModel> Sections { get; set; }
    }
}