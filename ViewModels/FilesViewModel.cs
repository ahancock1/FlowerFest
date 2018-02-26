// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   FileViewModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.ViewModels
{
    using System.Collections.Generic;

    public class FilesViewModel
    {
        public IEnumerable<FileDetailsViewModel> Files { get; set; }
    }
}