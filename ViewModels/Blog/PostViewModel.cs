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
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public IList<string> Categories { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }
}