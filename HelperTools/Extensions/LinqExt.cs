using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperTools.Extensions
{
	public static class LinqExt
	{
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
			 Func<TSource, TKey> keySelector)
		{
			return source.DistinctBy(keySelector, null);
		}

		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
			 Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));
			if (keySelector == null)
				throw new ArgumentNullException(nameof(keySelector));

			return DistinctByImpl(source, keySelector, comparer);
		}

		private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>(IEnumerable<TSource> source,
			 Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			var knownKeys = new HashSet<TKey>(comparer);
			foreach (var element in source)
			{
				if (knownKeys.Add(keySelector(element)))
					yield return element;
			}
		}


		public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
		{
			var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

			var l = source.ToLookup(firstKeySelector);
			foreach (var item in l)
			{
				var dict = new Dictionary<TSecondKey, TValue>();
				retVal.Add(item.Key, dict);
				var subdict = item.ToLookup(secondKeySelector);
				foreach (var subitem in subdict)
					dict.Add(subitem.Key, aggregate(subitem));

			}

			return retVal;
		}



	}
}