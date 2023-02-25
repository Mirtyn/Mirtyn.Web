using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mirtyn.Web
{    public static class StringExtensions
    {
        public static bool Contains(this string source, string value, StringComparison comparison)
        {
            return source?.IndexOf(value, comparison) >= 0;
        }
    }
}
