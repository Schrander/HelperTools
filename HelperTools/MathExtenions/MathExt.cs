namespace HelperTools
{
	public static partial class MathExt
	{

		#region Even or Odd

		//http://bytes.com/topic/c-sharp/answers/520852-how-determinate-integer-odd-even

		/// <summary>
		/// Checks if the value is even.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is even otherwise <c>false</c></returns>
		public static bool IsEven(this int value)
		{
			return value % 2 == 0;
		}

		/// <summary>
		/// Checks if the value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is odd otherwise <c>false</c></returns>
		public static bool IsOdd(this int value)
		{
			return value % 2 != 0;
		}

		/// <summary>
		/// Checks if the value is even.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is even otherwise <c>false</c></returns>
		public static bool IsEven(this int? value)
		{
			return value.HasValue && IsEven(value.Value);
		}

		/// <summary>
		/// Checks if the value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is odd otherwise <c>false</c></returns>
		public static bool IsOdd(this int? value)
		{
			return value.HasValue && IsOdd(value.Value);
		}

		/// <summary>
		/// Checks if the value is even.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is even otherwise <c>false</c></returns>
		public static bool IsEven(this long value)
		{
			return value % 2 == 0;
		}

		/// <summary>
		/// Checks if the value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is odd otherwise <c>false</c></returns>
		public static bool IsOdd(this long value)
		{
			return value % 2 != 0;
		}

		/// <summary>
		/// Checks if the value is even.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is even otherwise <c>false</c></returns>
		public static bool IsEven(this long? value)
		{
			return value.HasValue && IsEven(value.Value);
		}

		/// <summary>
		/// Checks if the value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is odd otherwise <c>false</c></returns>
		public static bool IsOdd(this long? value)
		{
			return value.HasValue && IsOdd(value.Value);
		}

		/// <summary>
		/// Checks if the value is even.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is even otherwise <c>false</c></returns>
		public static bool IsEven(this short value)
		{
			return value % 2 == 0;
		}

		/// <summary>
		/// Checks if the value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is odd otherwise <c>false</c></returns>
		public static bool IsOdd(this short value)
		{
			return value % 2 != 0;
		}

		/// <summary>
		/// Checks if the value is even.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is even otherwise <c>false</c></returns>
		public static bool IsEven(this short? value)
		{
			return value.HasValue && IsEven(value.Value);
		}

		/// <summary>
		/// Checks if the value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the value is odd otherwise <c>false</c></returns>
		public static bool IsOdd(this short? value)
		{
			return value.HasValue && IsOdd(value.Value);
		}

		#endregion



	}

}