// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Blog.ViewModels.Blog
{
    using System.Collections.Generic;
    using FlowerFest.ViewModels.Home;

    public class BlogViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public IEnumerable<SectionViewModel> Sections { get; set; }
    }
}