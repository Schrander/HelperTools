using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HelperTools.Extensions
{
	public static class EnumerableExtensions
	{
		// source: http://geekswithblogs.net/michelotti/archive/2009/02/06/custom-c-3.0-linq-max-extension-method.aspx
		// To determine an item with the highest property (arbitrary) in a collection
		[Obsolete]
		public static T MaxObject<T, TCompare>(this IEnumerable<T> collection, Func<T, TCompare> func) where TCompare : IComparable<TCompare>
		{
			T maxItem = default(T);
			TCompare maxValue = default(TCompare);
			foreach (var item in collection)
			{
				TCompare temp = func(item);
				if (maxItem == null || temp.CompareTo(maxValue) > 0)
				{
					maxValue = temp;
					maxItem = item;
				}
			}
			return maxItem;
		}

		public static T MaxFunc<T, TCompare>(this IEnumerable<T> collection, Func<T, TCompare> func) where TCompare : IComparable<TCompare>
		{
			T maxItem = default(T);
			TCompare maxValue = default(TCompare);
			foreach (var item in collection)
			{
				TCompare temp = func(item);
				if (maxItem == null || temp.CompareTo(maxValue) > 0)
				{
					maxValue = temp;
					maxItem = item;
				}
			}
			return maxItem;
		}

		public static T MinFunc<T, TCompare>(this IEnumerable<T> collection, Func<T, TCompare> func) where TCompare : IComparable<TCompare>
		{
			T minItem = default(T);
			TCompare minValue = default(TCompare);
			foreach (var item in collection)
			{
				TCompare temp = func(item);
				if (minItem == null || temp.CompareTo(minValue) <= 0)
				{
					minValue = temp;
					minItem = item;
				}
			}
			return minItem;
		}


		// http://stackoverflow.com/questions/2117404/searching-hierarchical-list
		public static IEnumerable<T> Flatten<T>(this IEnumerable<T> sequence, Func<T, IEnumerable<T>> childFetcher)
		{
			var itemsToYield = new Queue<T>(sequence);
			while (itemsToYield.Count > 0)
			{
				var item = itemsToYield.Dequeue();
				yield return item;

				IEnumerable<T> children = childFetcher(item);
				if (children == null) 
					continue;

				foreach (var child in children) itemsToYield.Enqueue(child);
			}
		}

		public static int Count(this IEnumerable sequence)
		{
			if (sequence == null)
				return 0;

			int count = 0;

			try
			{
				IEnumerator enumerator = sequence.GetEnumerator();

				while (enumerator.MoveNext())
					count++;
			}
			catch
			{
				return 0;
			}

			return count;
		}

		/// <summary>
		/// Determines whether the collection is null or contains no elements.
		/// </summary>
		/// <typeparam name="T">The IEnumerable type.</typeparam>
		/// <param name="enumerable">The enumerable, which may be null or empty.</param>
		/// <returns>
		///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
				return true;

			/* If this is a list, use the Count property. 
			 * The Count property is O(1) while IEnumerable.Count() is O(N). */
			var collection = enumerable as ICollection<T>;
			if (collection != null)
				return collection.Count < 1;

			return !enumerable.Any();
		}

		/// <summary>
		/// Determines whether the collection is null or contains no elements.
		/// </summary>
		/// <typeparam name="T">The IEnumerable type.</typeparam>
		/// <param name="collection">The collection, which may be null or empty.</param>
		/// <returns>
		///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
		{
			if (collection == null)
				return true;

			return collection.Count < 1;
		}

		public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
		{
			T[] bucket = null;
			var count = 0;

			foreach (var item in source)
			{
				if (bucket == null)
					bucket = new T[size];

				bucket[count++] = item;

				if (count != size)
					continue;

				yield return bucket;

				bucket = null;
				count = 0;
			}

			if (bucket != null && count > 0)
				yield return bucket.Take(count);
		}

		public static IEnumerable<T> BatchPaged<T>(this IEnumerable<T> source, int size, int page)
		{
			var batch = Batch(source, size).ToList();
			page = MathExt.Max<int>(1, MathExt.Min<int>(page, batch.ToList().Count)) - 1;
			return batch.ToList()[page];
		}

		public static IEnumerable<DateTime> Range(this DateTime startDate, DateTime endDate)
		{
			return Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1).Select(i => startDate.AddDays(i));
		}

	}
}