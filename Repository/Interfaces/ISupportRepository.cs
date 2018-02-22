// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   ISupportRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository.Interfaces
{
    using System;
    using Models;

    public interface ISupportRepository
    {
        bool CreateSupport(Support support);
        bool UpdateSupport(Support support);
        bool DeleteSupport(Support support);
        Support GetSupport(Guid id);
    }
}