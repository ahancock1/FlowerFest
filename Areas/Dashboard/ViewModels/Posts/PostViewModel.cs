// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   PostViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.ViewModels.Posts
{
    using System;
    using System.ComponentModel;

    public class PostViewModel
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayName("Date Published")]
        public DateTime PublishedDate { get; set; }
    }
}