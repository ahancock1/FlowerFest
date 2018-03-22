// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   HomeViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels.Home
{
    using System.Collections.Generic;
    using Shared;

    public class HomeViewModel
    {
        public IEnumerable<TestimonialViewModel> Testimonials { get; set; }
        public IEnumerable<PostViewModel> RecentPosts { get; set; }
        public IEnumerable<SectionViewModel> Sections { get; set; }
        public IEnumerable<PartnerViewModel> Partners { get; set; }
        public IEnumerable<HeaderSectionViewModel> HeaderSections { get; set; }
    }
}