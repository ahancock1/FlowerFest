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
        [DisplayName("ID")]
        public Guid Id { get; set; }

        public string Title { get; set; }

        [DisplayName("Published")]
        public bool IsPublished { get; set; }

        [DisplayName("Date Published")]
        public DateTime PublishedDate { get; set; }
    }
}