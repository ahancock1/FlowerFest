// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonalsViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Blog
{
    using System.Collections.Generic;

    public class TestimonalsViewModel
    {
        public TestimonalsViewModel()
        {
            Posts = new List<TestimonalPostViewModel>();
        }

        public IList<TestimonalPostViewModel> Posts { get; set; }
    }
}