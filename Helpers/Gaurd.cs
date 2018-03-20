// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   Gaurd.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Helpers
{
    using System;

    public static class Gaurd
    {
        public static void ThrowIfNull<T>(T item)
        {
            if (item == null)
            {
                throw new ArgumentException($"{typeof(T)} can not be null.");
            }
        }
    }
}