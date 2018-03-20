// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Support.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Models
{
    using System;
    using Repository;

    public class PartnerModel : IEntity
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
    }
}