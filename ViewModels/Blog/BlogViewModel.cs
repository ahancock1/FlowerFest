// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   BlogViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels
{
    using FlowerFest.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<Post> RecentPosts { get; set; }
    }
}
