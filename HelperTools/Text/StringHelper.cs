using System.ComponentModel;
using System.Linq;

namespace HelperTools.Helpers
{
	public static class StringHelper
	{

		public static T? ToNullable<T>(this string s)
			 where T : struct
		{
			T? result = new T?();
			try
			{
				if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
				{
					TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
					object convertFrom = conv.ConvertFrom(s);
					if (convertFrom != null) result = (T)convertFrom;
				}
			}
			catch { }
			return result;
		}

		/// <summary>
		/// Makes from a collection of strings one large string.
		/// </summary>
		/// <param name="strings">words</param>
		/// <returns>A concatted string</returns>
		public static string Concat(params string[] strings)
		{
			if (strings == null)
				return null;

			string result = strings.Aggregate<string, string>(null, (current, item) => current + (!string.IsNullOrEmpty(item) ? item.Trim() + " " : null));
			return result.Trim();
		}

		/// <summary>
		/// Checks if a string end mathces with one of the given items.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="strings"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool EndsWith(this string value, params string[] strings)
		{
			return !string.IsNullOrEmpty(value) && strings.Any(item => !string.IsNullOrEmpty(item) && value.EndsWith(item));
		}

		/// <summary>
		/// Returns een new string in which all occurences of a specified multiple strings in the current instant
		/// are replaced with another specified string.
		/// </summary>
		/// <param name="s">The value.</param>
		/// <param name="oldValues">Array of strings.</param>
		/// <param name="newValue">The new string.</param>
		/// <returns></returns>
		public static string Replace(this string s, string[] oldValues, string newValue)
		{
			return !string.IsNullOrEmpty(s) ? oldValues.Aggregate(s, (current, item) => current.Replace(item, newValue)) : null;
		}

	}
}
