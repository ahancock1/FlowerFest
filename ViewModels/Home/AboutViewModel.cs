// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   AboutViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels
{
    using System.Collections.Generic;

    public class AboutViewModel
    {
        public SectionContentViewModel[] Contents { get; set; }
    }

    public class SectionContentViewModel
    {
        public int Index { get; set; }
        public string Content { get; set; }
    }
}
