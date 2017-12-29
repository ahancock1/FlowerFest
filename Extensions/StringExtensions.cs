// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   StringExtensions.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class StringExtensions
    {
        public static bool Contains(this string source, string check, StringComparison comp)
        {
            return source?.IndexOf(check, comp) >= 0;
        }
    }
}
