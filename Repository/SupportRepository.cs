// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SupportRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System;
    using Interfaces;
    using Models;

    public class SupportRepository : Repository<Support>, ISupportRepository
    {
        public SupportRepository(string path)
            : base(path)
        {
        }

        public bool CreateSupport(Support support)
        {
            return Create(support);
        }

        public bool UpdateSupport(Support support)
        {
            return Update(support);
        }

        public bool DeleteSupport(Support support)
        {
            return Delete(support);
        }

        public Support GetSupport(Guid id)
        {
            return Retrieve(id);
        }
    }
}