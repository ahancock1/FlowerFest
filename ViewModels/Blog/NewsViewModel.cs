// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   NewsViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System.Collections.Generic;

    public class NewsViewModel
    {
        public NewsViewModel()
        {
            RecentPosts = new List<PostViewModel>();
        }

        public IList<PostViewModel> RecentPosts { get; set; }
    }
}