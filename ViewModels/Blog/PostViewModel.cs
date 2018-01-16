// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   PostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;

    public class PostViewModel
    {
        public PostViewModel()
        {
            Categories = new List<string>();
        }

        public string Author { get; set; }
        public DateTime Published { get; set; }
        public string Title { get; set; }
        public IList<string> Categories { get; set; }
        public string Slug { get; set; }
    }
}