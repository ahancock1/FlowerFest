// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   BlogViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels
{
    using FlowerFest.ViewModels.Blog;
    using System.Collections.Generic;

    public class BlogViewModel
    {
        public BlogViewModel()
        {
            Posts = new List<PostViewModel>();
        }

        public IList<PostViewModel> Posts { get; set; }
    }
}
