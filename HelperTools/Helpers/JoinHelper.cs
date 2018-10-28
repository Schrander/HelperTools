using System.Collections.Generic;
using System.Linq;
using HelperTools.Extensions;

namespace HelperTools.Helpers
{
	public static class JoinHelper
	{
		public static string JoinDistinct<T>(string separator, IEnumerable<T> list) where T : struct
		{
			string output = string.Join(separator, list.Distinct().ToList());
			return output.EndsWith(",") ? output.Substring(0, output.Length - 1) : output;
		}

		public static string JoinCoalesce(string seperator, params string[] values)
		{
			return Join(seperator, true, values);
		}

		public static string Join(string seperator, bool skipNull, params string[] values)
		{
			string[] output = values.ToList().Where(s => !string.IsNullOrEmpty(s)).ToArray();
			return string.Join(seperator, (skipNull ? output : values));
		}

		public static string Join<T>(this IEnumerable<T> list, string separator) where T : struct
		{
			return string.Join(separator, list);
		}

		public static string Join<T>(this IEnumerable<T?> list, string separator) where T : struct
		{
			return string.Join(separator, list.SelectWithValue());
		}

		public static string Join(this IEnumerable<string> list, string separator)
		{
			return string.Join(separator, list.SelectWithValue());
		}

		public static string JoinDistinct<T>(this IEnumerable<T> list, string separator) where T : struct
		{
			return string.Join(separator, list.Distinct());
		}

		public static string JoinDistinct<T>(this IEnumerable<T?> list, string separator) where T : struct
		{
			return string.Join(separator, list.Distinct().SelectWithValue());
		}

		public static string JoinDistinct(this IEnumerable<string> list, string separator)
		{
			return string.Join(separator, list.Distinct().SelectWithValue());
		}
		public static string Join(string seperator, params string[] values)
		{
			return string.Join(seperator, values.ToList());
		}

	}
}
