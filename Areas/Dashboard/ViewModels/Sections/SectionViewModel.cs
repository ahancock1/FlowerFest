// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SectionViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Areas.Dashboard.ViewModels.Sections
{
    public class SectionViewModel
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}