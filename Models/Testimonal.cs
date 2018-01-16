// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Testimonal.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Models
{
    using System;

    public class Testimonal
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
        public string Author { get; set; }
        public string Place { get; set; }
    }
}