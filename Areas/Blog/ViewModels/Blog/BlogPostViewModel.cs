// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   BlogPostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Blog.ViewModels.Blog
{
    using System.Collections.Generic;
    using FlowerFest.ViewModels.Shared;

    public class BlogPostViewModel
    {
        public PostViewModel Post { get; set; }
        public IEnumerable<HeaderSectionViewModel> HeaderSections { get; set; }
    }
}