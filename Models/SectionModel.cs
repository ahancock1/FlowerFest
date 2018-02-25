// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SectionModel.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Models
{
    using System;
    using Repository;

    public class SectionModel : IEntity
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