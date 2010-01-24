using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMSBlog.Model.Helpers
{
    public static class LinqExtensions
    {
        public static bool None<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

    }
}
