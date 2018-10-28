using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HelperTools.Normalizations;

namespace HelperTools.Text
{
	public static class TextFormatting
	{
		public static string ToLowerNullable(this string value)
		{
			return !string.IsNullOrEmpty(value) ? value.ToLower() : null;
		}

		public static string ToUpperNullable(this string value)
		{
			return !string.IsNullOrEmpty(value) ? value.ToUpper() : null;
		}

		#region RemoveDiacritics

		/// <summary>
		/// vervangt "ë" etc naar "e"
		/// </summary>
		/// <param name="s">de aan te passen tekst</param>
		/// <returns>de aangepaste tekst</returns>
		public static string RemoveDiacritics(string s)
		{
			string normalizedString = s.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();

			for (int i = 0; i < normalizedString.Length; i++)
			{
				char c = normalizedString[i];
				if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
					stringBuilder.Append(c);
			}

			return stringBuilder.ToString();
		}
		#endregion

		#region First letter Upper or Lower Case
		/// <summary>
		/// Converts the first letter to upper case.
		/// Example: "van der Vliet" becomes "Van der Vliet"
		/// </summary>
		/// <param name="s">The string</param>
		/// <returns></returns>
		public static string FirstLetterToUpper(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return null;

			if (s.Length == 1)
				return s.ToUpper();

			s = s.Substring(0, 1).ToUpper() + s.Substring(1);

			// IJ heeft een aparte behandeling
			if (s.StartsWith("ij", StringComparison.CurrentCultureIgnoreCase))
				s = s.Substring(0, 2).ToUpper() + s.Substring(2);

			return s;
		}

		/// <summary>
		/// Converts the first letter to upper case.
		/// Example: "van der Vliet" becomes "Van der Vliet"
		/// </summary>
		/// <param name="s">The string</param>
		/// <returns></returns>
		public static string FirstLetterToUpperForced(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return null;

			if (s.Length == 1)
				return s.ToUpper();

			s = s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();

			// IJ heeft een aparte behandeling
			if (s.StartsWith("ij", StringComparison.CurrentCultureIgnoreCase))
				s = s.Substring(0, 2).ToUpper() + s.Substring(2).ToLower();

			return s;
		}

		/// <summary>
		/// Converts the first letter to lower case.
		/// Example: "Van der Vliet" becomes "van der Vliet"
		/// </summary>
		/// <param name="s">The string</param>
		/// <returns></returns>
		public static string FirstLetterToLower(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return s;

			if (s.Length == 1)
				return s.ToLower();

			s = s.Substring(0, 1).ToLower() + s.Substring(1);

			// IJ heeft een aparte behandeling
			if (s.StartsWith("ij", StringComparison.CurrentCultureIgnoreCase))
				s = s.Substring(0, 2).ToUpper() + s.Substring(2);

			return s;
		}

		public static string FirstLetterToLowerForced(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return s;

			if (s.Length == 1)
				return s.ToLower();

			s = s.Substring(0, 1).ToLower() + s.Substring(1).ToLower();

			// IJ heeft een aparte behandeling
			if (s.StartsWith("ij", StringComparison.CurrentCultureIgnoreCase))
				s = s.Substring(0, 2).ToLower() + s.Substring(2).ToLower();

			return s;
		}

		#endregion

		#region Pascal Casing

		/// <summary>
		/// Converts the string to Pascal casing.
		/// Example: "the quick brown fox" becomes "The Quick Brown Fox"
		/// </summary>
		/// <param name="value">The string</param>
		/// <returns></returns>
		static public string ToPascalCase(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;
			value = value.Sanitize(true);

			return Regex.Replace(value, @"\w+", delegate (Match match) { string v = match.ToString(); return v.FirstLetterToUpperForced(); });
		}

		/// <summary>
		/// Converts the string to Pascal casing.
		/// Example: "the quick brown fox" becomes "The Quick Brown Fox"
		/// </summary>
		/// <param name="s">The string</param>
		/// <returns></returns>
		static public string ToPascalCaseLossLess(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return null;
			s = s.Sanitize();

			return Regex.Replace(s, @"\w+", delegate (Match match) {
				string v = match.ToString();
				return v.FirstLetterToUpper();
			});
		}

		/// <summary>
		/// Converts the string to Pascal casing with white spaces and non-alfabetical en non-numeric chars removed.
		/// Example: "the quick brown fox" becomes "TheQuickBrownFox"
		/// </summary>
		/// <param name="s">The string</param>
		/// <returns></returns>
		static public string ToPascalCaseWordTrimmed(this string s)
		{
			return !string.IsNullOrEmpty(s) ? s.ToPascalCase().Sanitize(new[]
				{ " ", "[", "]", "(", ")", "<", ">", "{", "}", ",", ".", ";", ":", @"""", "'", "+", "-" , "!",
					"@", "#" , "$","%", "^" , "&", "*" , "/", @"\", "|", "?"
				}) : null;
		}

		#endregion

		#region Camel Casing
		/// <summary>
		/// Converts the string to camel casing.
		/// Example: "the quick brown fox" becomes "the Quick Brown Fox"
		/// </summary>
		/// <param name="value">The string</param>
		/// <returns></returns>
		public static string ToCamelCase(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;
			value = value.Sanitize(true);

			return Regex.Replace(value, @"\w+", delegate (Match match) {
				string v = match.ToString();
				return (match.Index == 0) ? v.FirstLetterToLowerForced() : v.FirstLetterToUpperForced();

			});
		}

		/// <summary>
		/// Converts the string to camel casing.
		/// Example: "the quick brown fox" becomes "the Quick Brown Fox"
		/// </summary>
		/// <param name="s">The string</param>
		/// <returns></returns>
		public static string ToCamelCaseLossLess(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return null;
			s = s.Sanitize();

			return Regex.Replace(s, @"\w+", delegate (Match match) {
				string v = match.ToString();
				return (match.Index == 0) ? v.FirstLetterToLower() : v.FirstLetterToUpper();

			});
		}

		/// <summary>
		/// Converts the string to camel casing and with white spaces removed.
		/// Example: "the quick brown fox" becomes "theQuickBrownFox"
		/// </summary>
		/// <param name="s">The string</param>
		/// <returns></returns>
		static public string ToCamelCaseWordTrimmed(this string s)
		{
			return !string.IsNullOrEmpty(s) ? s.ToCamelCase().Sanitize(new[]
				{ " ", "[", "]", "(", ")", "<", ">", "{", "}", ",", ".", ";", ":", @"""", "'", "+", "-" , "!",
					"@", "#" , "$","%", "^" , "&", "*" , "/", @"\", "|", "?"
				}) : null;
		}

		#endregion

		#region Title Casing

		/// <summary>
		/// Converts the string to title casing.
		/// Example: "the quick brown fox jumps over the lazy dog" becomes "The Quick Brown Fox Jumps over the Lazy Dog"
		/// </summary>
		/// <param name="value">The string</param>
		/// <returns></returns>
		public static string ToTitleCase(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			value = value.Sanitize(true);

			if (string.IsNullOrEmpty(value))
				return null;

			value = Regex.Replace(value, @"\w+", delegate (Match match) {
				string v = match.ToString();
				bool isBlacklisted = Blacklist.Any(word => word.Equals(v, StringComparison.CurrentCultureIgnoreCase));
				return (match.Index != 0 && isBlacklisted) ? v.ToLower().FirstLetterToLower() : v.ToLower().FirstLetterToUpper();

			});

			// Uitzonderingen
			// http://taaladvies.net/taal/advies/vraag/543
			// d'Ancona, d'Hellewyn
			MatchCollection matches = Regex.Matches(value, @"[d|D] ?'( *)?[aehiouyAEHIOUY]");
			foreach (Match m in matches)
				value = value.Replace(m.Value, m.Value.Replace(" ", "").ToUpper().FirstLetterToLower());

			return value;
		}

		/// <summary>
		/// Converts the string to title casing.
		/// Example: "the quick brown fox jumps over the lazy dog" becomes "The Quick Brown Fox Jumps over the Lazy Dog"
		/// </summary>
		/// <param name="s">The string</param>
		/// <returns></returns>
		public static string ToTitleCaseLossLess(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			value = value.Sanitize();

			if (string.IsNullOrEmpty(value))
				return null;

			return Regex.Replace(value, @"\w+", delegate (Match match) {
				string m = match.ToString();
				bool isBlacklisted = Blacklist.Any(word => word.Equals(m, StringComparison.CurrentCultureIgnoreCase));
				return (match.Index != 0 && isBlacklisted) ? m.FirstLetterToLower() : m.FirstLetterToUpper();

			});
		}

		private static IEnumerable<string> Blacklist
		{
			get
			{
				return new List<string> { "van", "de", "het", "een", "op", "met", "der", "den", "des", "the", "over", "a", "on", "'s", "'t" };
			}
		}

		#endregion


		#region Trimming

		/// <summary>
		/// Returns a substring for a specified amount of characters and adds three dots to the end.
		/// </summary>
		/// <param name="value">The source string.</param>
		/// <param name="charCount">The number of characters to save.</param>
		/// <returns>An ellipsed string.</returns>
		public static string EllipsisTrim(this string value, int charCount)
		{
			return EllipsisTrim(value, charCount, "…");
		}

		public static string EllipsisTrim(this string value, int charCount, string ellipsis)
		{
			if (string.IsNullOrEmpty(value)) return null;
			if (charCount <= 3) return value;

			return value.Substring(0, MathExt.Min<int>(charCount, value.Length)) + (value.Length > charCount ? ellipsis : null);
		}

		/// <summary>
		/// Trims a string.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="doCollapseSpaces"></param>
		/// <returns>a sanitized string</returns>
		public static string Sanitize(this string value, bool doCollapseSpaces = false)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			if (doCollapseSpaces)
				value = value.CollapseSpaces();

			value = value.Trim();

			return !string.IsNullOrEmpty(value) ? value : null;
		}


		/// <summary>
		/// Removes the quotes from a specified string.
		/// </summary>
		/// <param name="aValue">the string.</param>
		/// <returns>A specified string without quotes.</returns>
		public static string TrimQuotes(this string value)
		{
			if (string.IsNullOrEmpty(value)) return value;
			value = value.Trim();

			if (string.IsNullOrEmpty(value) || value.Length == 1)
				return value;

			value = value.StartsWith("\"") ? value.Substring(1, value.Length - 1) : value;
			value = value.EndsWith("\"") ? value.Substring(0, value.Length - 1) : value;

			return value;
		}


		/// <summary>
		/// Removes the single quotes from a specified string.
		/// </summary>
		/// <param name="value">the string.</param>
		/// <returns>A specified string without single quotes.</returns>
		public static string TrimSingleQuotes(this string value)
		{
			if (string.IsNullOrEmpty(value)) return value;
			value = value.Trim();

			if (string.IsNullOrEmpty(value) || value.Length == 1)
				return value;

			value = value.StartsWith("'") ? value.Substring(1, value.Length - 1) : value;
			value = value.EndsWith("'") ? value.Substring(0, value.Length - 1) : value;

			return value;
		}

		/// <summary>
		/// Removes the parentheses ('(' | ')')from a specified string.
		/// </summary>
		/// <param name="value">the string.</param>
		/// <returns>A specified string without parentheses.</returns>
		public static string TrimParentheses(this string value)
		{
			if (string.IsNullOrEmpty(value)) return value;
			value = value.Trim();

			if (string.IsNullOrEmpty(value) || value.Length == 1)
				return value;

			value = value.StartsWith("(") ? value.Substring(1, value.Length - 1) : value;
			value = value.EndsWith(")") ? value.Substring(0, value.Length - 1) : value;

			return value;
		}

		/// <summary>
		/// Removes the brackets ([]) from a specified string.
		/// </summary>
		/// <param name="value">the string.</param>
		/// <returns>A specified string without brackets.</returns>
		private static string TrimBrackets(string value)
		{
			if (string.IsNullOrEmpty(value)) return value;
			value = value.Trim();

			if (string.IsNullOrEmpty(value) || value.Length == 1)
				return value;

			value = value.StartsWith("[") ? value.Substring(1, value.Length - 1) : value;
			value = value.EndsWith("]") ? value.Substring(0, value.Length - 1) : value;

			return value;
		}

		/// <summary>
		/// Removes the curly brackets ({}) from a specified string.
		/// </summary>
		/// <param name="value">the string.</param>
		/// <returns>A specified string without curly brackets.</returns>
		private static string TrimCurlyBrackets(string value)
		{
			if (string.IsNullOrEmpty(value)) return value;
			value = value.Trim();

			if (string.IsNullOrEmpty(value) || value.Length == 1)
				return value;

			value = value.StartsWith("{") ? value.Substring(1, value.Length - 1) : value;
			value = value.EndsWith("}") ? value.Substring(0, value.Length - 1) : value;

			return value;
		}

		/// <summary>
		/// Removes the chevrons (<>) from a specified string.
		/// </summary>
		/// <param name="aValue">the string.</param>
		/// <returns>A specified string without chevrons.</returns>
		private static string TrimChevrons(string value)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			value = value.Trim();

			if (string.IsNullOrEmpty(value) || value.Length == 1)
				return value;

			value = value.StartsWith("<") ? value.Substring(1, value.Length - 1) : value;
			value = value.EndsWith(">") ? value.Substring(0, value.Length - 1) : value;

			return value;
		}


		/// <summary>
		/// Trims a string.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="doCollapseSpaces"></param>
		/// <returns>a sanitized string</returns>
		public static string Sanitize(this string value, string[] replaceChars, bool doCollapseSpaces = false)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			value = Sanitize(value, doCollapseSpaces);
			if (replaceChars.Any())
				value = value.Replace(replaceChars, "");

			return !string.IsNullOrEmpty(value) ? value : null;
		}

		public static string Normalize(this string value, BaseNormalization normalisation)
		{
			return normalisation.Normalize(value);
		}

		/// <summary>
		/// All whitespaces will be replaced by one single whitespace.
		/// </summary>
		public static string CollapseSpaces(this string value)
		{
			return Regex.Replace(value, @"\s+", " ");
		}

		public static string Formatting(this string value, EnumFormatType? formatType)
		{
			if (string.IsNullOrEmpty(value) || !formatType.HasValue)
				return null;

			switch (formatType.Value)
			{
				case EnumFormatType.UpperCase:
					return value.ToUpper();

				case EnumFormatType.LowerCase:
					return value.ToLower();

				case EnumFormatType.PascalCase:
					return value.ToPascalCase();

				case EnumFormatType.CamelCase:
					return value.ToCamelCase();

				case EnumFormatType.TitleCase:
					return value.ToTitleCase();

				case EnumFormatType.FirstLetterLowerCase:
					return value.FirstLetterToLower();

				case EnumFormatType.FirstLetterUpperCase:
					return value.FirstLetterToUpper();

				default:
					return value;
			}
		}

		#endregion

	}
}
