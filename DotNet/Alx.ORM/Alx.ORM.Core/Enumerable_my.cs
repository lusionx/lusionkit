using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Alx.ORM.Core
{
    internal static class Enumerable_my
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

        public static ArrayList ToList(IEnumerable source)
        {
            var ls = new ArrayList();
            foreach (var a in source)
            {
                ls.Add(a);
            }
            return ls;
        }


        public static bool In<T>(this T obj, IEnumerable<T> arr) where T : class
        {
            return arr.Where(a => a == obj).Any();
        }
    }
}
