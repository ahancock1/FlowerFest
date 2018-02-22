// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IEntity.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System;

    public interface IEntity
    {
        Guid Id { get; set; }
    }
}