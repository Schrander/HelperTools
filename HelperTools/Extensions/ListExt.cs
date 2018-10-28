using HelperTools.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HelperTools.Extensions
{
    public static class ListExt
    {
        public static int LastIndex(this IEnumerable item)
        {
            return item?.Count() - 1 ?? -1;
        }

        public static IEnumerable<T?> SelectWithValue<T>(this IEnumerable<T?> list) where T : struct
        {
            return from item in list where item.HasValue() select item;
        }

        public static IEnumerable<string> SelectWithValue(this IEnumerable<string> list)
        {
            return from item in list where !string.IsNullOrEmpty(item) select item;
        }

        public static List<T> DistinctList<T>(this List<T> list) where T : struct
        {
            return list.Distinct<T>().ToList<T>();
        }

        public static List<List<TSource>> GroupByNestedList<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> keySelector)
        {
            var output = list.GroupBy(keySelector).Select(s => s.ToList()).ToList();
            return output;
        }

        public static List<List<TSource>> GroupByNestedList<TSource, TKey>(this IEnumerable<TSource> list, Func<TSource, TKey> keySelector)
        {
            var output = list.GroupBy(keySelector).Select(s => s.ToList()).ToList();
            return output;
        }

        public static List<List<TSource>> ToLookupList<TSource, TKey>(this List<TSource> list, Func<TSource, TKey> keySelector)
        {
            var output = list.ToLookup(keySelector).Select(s => s.ToList()).ToList();
            return output;
        }


        public static IEnumerable<TSource> GetDuplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IEqualityComparer<TKey> comparer)
        {
            var hash = new HashSet<TKey>(comparer);
            return source.Where(item => !hash.Add(selector(item)));
        }

        public static IEnumerable<TSource> GetDuplicates<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            return source.GetDuplicates(x => x, comparer);
        }

        public static IEnumerable<TSource> GetDuplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.GetDuplicates(selector, null);
        }

        public static IEnumerable<TSource> GetDuplicates<TSource>(this IEnumerable<TSource> source)
        {
            return source.GetDuplicates(x => x, null);
        }

        // http://stackoverflow.com/questions/2117404/searching-hierarchical-list
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> sequence, Func<T, IEnumerable<T>> childFetcher)
        {
            var itemsToYield = new Queue<T>(sequence);
            while (itemsToYield.Count > 0)
            {
                var item = itemsToYield.Dequeue();
                yield return item;

                var children = childFetcher(item);
                if (children != null)
                    foreach (var child in children) itemsToYield.Enqueue(child);
            }
        }


        public static List<T> RemoveDefault<T>(this List<T> list) //where T : struct
        {
            return list.Where(w => (double)Convert.ChangeType(w, typeof(double)) > 0).ToList();
        }

        public static List<T> Sanitize<T>(this List<T> list) //where T : struct
        {
            return list.RemoveDefault().Distinct().ToList();
        }

        public static List<List<T>> Columnize<T>(this List<T> list, int maxRow)
        {
            int max = maxRow.ForceToRange(1, list.Count);
            int columnMax = (int)Math.Ceiling(list.Count / (decimal)max);

            List<List<T>> newList = new List<List<T>>();
            for (int j = 0; j < max; j++)
            {
                int init = 0 + (j * columnMax);
                int m = Math.Min((j + 1) * columnMax, list.Count);
                List<T> l = new List<T>();
                for (int i = init; i < m; i++)
                {
                    l.Add(list[i]);
                }
                newList.Add(l);
            }

            return newList;
        }


    }
}