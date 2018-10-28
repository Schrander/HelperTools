using System;
using System.Collections.Generic;
using System.Linq;
using HelperTools.Extensions;
using static System.Convert;

namespace HelperTools.Helpers
{
	public static class CoalesceHelper
	{
		// Based on SQL Coalesce
		// http://msdn.microsoft.com/en-us/library/ms190349.aspx

		/// <summary>
		/// Gets the first non-empty or last string from a collection of strings.
		/// </summary>
		public static string Coalesce(params string[] strings)
		{
			return strings.FirstOrDefault(s => !string.IsNullOrEmpty(s));
		}

		public static string Coalesce(IEnumerable<string> strings)
		{
			return strings.FirstOrDefault(s => !string.IsNullOrEmpty(s));
		}

		/// <summary>
		/// Gets the first non-empty or last string from a collection of strings.
		/// </summary>
		public static string Coalesce(this string value, params string[] strings)
		{
			List<string> list = strings.ToList();
			list.Insert(0, value);
			return list.FirstOrDefault(s => !string.IsNullOrEmpty(s));
		}

		public static string Coalesce(this string value, IEnumerable<string> strings)
		{
			strings.ToList().Insert(0, value);
			return strings.FirstOrDefault(s => !string.IsNullOrEmpty(s));
		}

		/// <summary>
		/// Gets the first non-null or default value.
		/// In case of datetime, the default value will be now.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		public static T Coalesce<T>(this T? value, params T?[] items) where T : struct
		{
			List<T?> list = items.ToList();
			list.Insert(0, value);
			if (typeof(T) == typeof(DateTime))
				list.Add((T)ChangeType(DateTime.MinValue, typeof(T)));
			else if (typeof(T) == typeof(DateTimeOffset))
				list.Add((T)ChangeType(DateTimeOffset.MinValue, typeof(T)));
			else
				list.Add(default(T));

			return (T)ChangeType(list.FirstOrDefault(s => s.HasValue), typeof(T));
		}

		/// <summary>
		/// Try to get the first non-null or nullable default value.
		/// If nothing has a value, null will be returned;
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		public static T? CoalesceNullable<T>(this T? value, params T?[] items) where T : struct
		{
			List<T?> list = items.ToList();
			list.Insert(0, value);

			foreach (T? i in list.Where(i => i.HasValue))
				return (T)ChangeType(i.Value, typeof(T));

			return default(T?);
		}


		public static object Coalesce(this object value, params object[] items)
		{
			List<object> objects = items.ToList();
			objects.Insert(0, value);

			return objects.FirstOrDefault(o => o.HasValue());
		}

	}
}