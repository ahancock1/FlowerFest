// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   StringExtensions.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static bool Contains(this string source, string value, StringComparison comparison)
        {
            return source?.IndexOf(value, comparison) >= 0;
        }
    }
}