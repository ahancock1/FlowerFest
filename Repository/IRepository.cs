// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> All(
            Func<T, bool> predicate = null,
            Func<T, object> orderby = null,
            int? skip = null,
            int? take = null);

        bool Create(T item);
        bool Delete(T item);
        T Get(Func<T, bool> predicate);
        bool Update(T item);
    }
}