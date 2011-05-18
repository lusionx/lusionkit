using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class Enumerable_my
    {
        public static void Action<TSource>(this IEnumerable<TSource> source, Action<TSource> predicate)
        {
            foreach (var a in source)
            {
                predicate(a);
            }
        }
        public static void Action<TSource>(this IEnumerable<TSource> source, Action<TSource, int> predicate)
        {
            var i = 0;
            foreach (var a in source)
            {
                predicate(a, i);
                i++;
            }
        }

        public static bool In<T>(this T obj, IEnumerable<T> arr) where T : class
        {
            return arr.Where(a => a == obj).Any();
        }
    }
}
