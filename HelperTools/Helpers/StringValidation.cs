using System.Linq;

namespace HelperTools.Helpers
{
	public static class StringValidation
	{
		/// <summary>
		/// Checks if a string only exists of digits.
		/// </summary>
		/// <param name="value"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool HasOnlyDigits(this string value)
		{
			return !string.IsNullOrEmpty(value) && value.All(char.IsDigit);
		}

		public static bool IsNumeric(this string value)
		{
			return HasOnlyDigits(value);
		}

		/// <summary>
		/// Checks if a string one or more digits contains.
		/// </summary>
		/// <param name="value"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool HasDigits(this string value)
		{
			return !string.IsNullOrEmpty(value) && value.Any(char.IsDigit);
		}

		/// <summary>
		/// Checks if a string only exits of letters.
		/// </summary>
		/// <param name="value"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool HasOnlyLetters(this string value)
		{
			return !string.IsNullOrEmpty(value) && value.All(char.IsLetter);
		}

		/// <summary>
		/// Checks if a string one or more letters contains.
		/// </summary>
		/// <param name="value"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool HasLetters(this string value)
		{
			return !string.IsNullOrEmpty(value) && value.Any(char.IsLetter);
		}

		/// <summary>
		/// Checks if a string one or more capitals contains.
		/// </summary>
		/// <param name="value"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool HasCapitals(this string value)
		{
			return value.Any(char.IsUpper);
		}

		/// <summary>
		/// Checks if a string whitespaces contains.
		/// </summary>
		/// <param name="value"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool HasWhitespaces(this string value)
		{
			return !string.IsNullOrEmpty(value) && value.Any(char.IsWhiteSpace);
		}

		/// <summary>
		/// Checks if a string only has whitespaces.
		/// </summary>
		/// <param name="value"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool HasOnlyWhitespaces(this string value)
		{
			return !string.IsNullOrEmpty(value) && value.All(char.IsWhiteSpace);
		}

	}
}
