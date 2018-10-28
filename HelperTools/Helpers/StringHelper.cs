using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HelperTools.Helpers
{
	public static class StringHelper {

		public static T? ToNullable<T>(this string s)
			 where T : struct {
			T? result = new T?();
			try {
				if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0) {
					TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
					result = (T)conv.ConvertFrom(s);
				}
			}
			catch { }
			return result;
		}

		public static bool StringToBool(string item) {
			if (!string.IsNullOrEmpty(item)) {
				switch (item) {
					case "1":
					case "y":
					case "yes":
					case "ja":
					case "j":
						return true;
					case "0":
					case "n":
					case "no":
					case "nee":
						return false;
					default:
						return false;
				}

			}
			return false;
		}

		/// <summary>
		/// Makes from a collection of strings one large string.
		/// </summary>
		/// <param name="strings">words</param>
		/// <returns>A concatted string</returns>
		public static string Concat(params string[] strings) {
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
		public static bool EndsWith(this string value, params string[] strings) {
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
		public static string Replace(this string s, string[] oldValues, string newValue) {
			return !string.IsNullOrEmpty(s) ? oldValues.Aggregate(s, (current, item) => current.Replace(item, newValue)) : null;
		}



		public static string ConsecutiveList(List<int> items) {

			string y = string.Empty;
			for (int i = 0; i <= items.Count - 1; i++) {
				if (i == 0)
					y = items[0].ToString();

				if (i > 0) {
					if (items[i] - items[i - 1] == 1) {
						if (!y.EndsWith("-"))
							y += "-";

						if (items[i] < items.Count - 1)
							continue;
					}
					if ((items[i] - items[i - 1]) > 1) {
						y += (items[i] > items[i - 1] ? (y.EndsWith("-") ? items[i - 1].ToString() : null) + ", " : null) + items[i];
					}
					else if ((i == items.Count - 1) && items[i] > items[i - 1]) {
						y += items[i].ToString();
					}
				}
			}

			return y;
		}


	}
}
