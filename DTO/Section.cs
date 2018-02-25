// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Section.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.DTO
{
    using System;

    public class Section
    {
        public int Index { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public Guid Id { get; set; }
        public string Background { get; set; }
        public string Tag { get; set; }
    }
}