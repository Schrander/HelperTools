using System.Linq;
using static HelperTools.MathExt;

namespace HelperTools.Extensions
{
	public static class StringExt
	{

		#region Substring

		public static string SubstringLast(this string s)
		{
			return !string.IsNullOrEmpty(s) ? SubstringLast(s, 1) : null;
		}

		public static string SubstringLastChar(this string s)
		{
			return !string.IsNullOrEmpty(s) ? SubstringLast(s, 1) : null;
		}

		public static string EllipsisTrim(this string value, int charCount, string ellipsis = "…")
		{
			if (string.IsNullOrEmpty(value)) return null;
			if (charCount <= 3) return value;

			return value.Substring(0, Min<int>(charCount, value.Length)) + (value.Length > charCount ? ellipsis : null);
		}

		/// <summary>
		/// Gets the last specified amount of characters for a given string.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="i">The number of chars.</param>
		/// <returns></returns>
		public static string SubstringLast(this string s, int i)
		{
			if (string.IsNullOrEmpty(s)) return null;
			if (i < 1) i = 1;
			if (i >= s.Length) i = s.Length;
			return s.Substring(s.Length - i, i);
		}

		#endregion

		#region TrueFalse Value
		/// <summary>
		/// Returns trueValue if true
		/// </summary>
		/// <param name="value"></param>
		/// <param name="trueValue"></param>
		/// <returns><c>trueValue</c> otherwise <c>null</c></returns>
		public static string TrueOrNullValue(this string value, string trueValue)
		{
			return TrueOrNullValue(value, trueValue, null);
		}

		/// <summary>
		/// Returns trueValue or falseValue
		/// </summary>
		/// <param name="value"></param>
		/// <param name="trueValue">the true value</param>
		/// <param name="nullValue">the false value</param>
		/// <returns><c>trueValue</c> otherwise <c>falseValue</c></returns>
		public static string TrueOrNullValue(this string value, string trueValue, string nullValue)
		{
			return !string.IsNullOrEmpty(value) ? trueValue : nullValue;
		}

		#endregion

		public static string Replace(this string s, string[] oldValues, string newValue)
		{
			return !string.IsNullOrEmpty(s) ? oldValues.Aggregate(s, (current, item) => current.Replace(item, newValue)) : null;
		}
		


	}

}
