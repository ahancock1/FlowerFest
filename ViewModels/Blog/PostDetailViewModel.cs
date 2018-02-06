// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   PostDetailViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;

    public class PostDetailViewModel
    {
        public string Spotlight { get; set; }
        public string Id { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Title { get; set; }
        public IList<string> Categories { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
    }
}