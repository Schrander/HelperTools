namespace HelperTools.Financial
{
	public static class ElfproefHelper
	{
		/// <summary>
		/// Determines whether the specified value is elfproef.
		/// </summary>
		/// <param name="value">The Value</param>
		/// <returns>
		///   <c>true</c> if [is elfproef] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsElfproef(long value)
		{
			int digits = 1;
			int sum = 0;

			while (digits < 10)
			{
				sum += (int)(value % 10) * digits;
				value /= 10;
				digits++;
			}

			return (sum % 11) == 0;
		}

		/// <summary>
		/// Determines whether the specified value is elfproef.
		/// </summary>
		/// <param name="value">The Value</param>
		/// <returns>
		///   <c>true</c> if [is elfproef] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsElfproef(int value)
		{
			int digits = 1;
			int sum = 0;

			while (digits < 10)
			{
				sum += (int)(value % 10) * digits;
				value /= 10;
				digits++;
			}

			return (sum % 11) == 0;
		}


		/// <summary>
		/// Determines whether the specified value is a valid BSN nummer.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///   <c>true</c> if [is elfproef BSN] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsElfproefBSN(long value)
		{
			int digits = 1;
			int sum = 0;

			while (digits < 10)
			{
				sum += (int)(value % 10) * (digits == 1 ? -1 * digits : digits);
				value /= 10;
				digits++;
			}

			return (sum % 11) == 0;
		}



		/// <summary>
		/// Determines whether the specified value is a valid BSN nummer.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///   <c>true</c> if [is elfproef BSN] [the specified value]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsElfproefBSN(int value)
		{
			int digits = 1;
			int sum = 0;

			while (digits < 10)
			{
				sum += (int)(value % 10) * (digits == 1 ? -1 * digits : digits);
				value /= 10;
				digits++;
			}

			return (sum % 11) == 0;
		}

		public static bool IsElfproefBSN(string value)
		{
			int bsnnummer;
			return int.TryParse(value, out bsnnummer) && IsElfproefBSN(bsnnummer);
		}

	}
}
