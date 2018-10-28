using System;
using HelperTools.Collections;

namespace HelperTools.Helpers
{
	public static class NumberFormatting
	{

		#region Trailing

		public static string Trailing<T>(this T? value, char sign, int amount)
			where T : struct
		{
			if (!value.HasValue)
				return null;
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return Trailing(value.Value, sign, amount);
		}

		public static string Trailing<T>(this T value, char sign, int amount)
			where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			amount = MathExt.Max<int>(amount - value.NumbersAmount(), 0);

			string trailing = null;
			for (int i = 0; i < amount; i++)
				trailing += sign;

			return $"{trailing}{Convert.ToDecimal(value):C2}";
		}

		#endregion


		public static string ToOperator(int? value)
		{
			return value.HasValue ? $"{(value.Value > 0 ? "+" : null)}{value.Value}" : null;
		}

		public static string ToOperator(long? value)
		{
			return value.HasValue ? $"{(value.Value > 0 ? "+" : null)}{value.Value}" : null;
		}

		public static string ToOperator(short? value)
		{
			return value.HasValue ? $"{(value.Value > 0 ? "+" : null)}{value.Value}" : null;
		}

		#region TrailingZeros
		public static string TrailingZeros(this int value, int amountZeros)
		{
			return TrailingZeros(Convert.ToInt64(value), amountZeros);
		}

		public static string TrailingZeros(this short value, int amountZeros)
		{
			return TrailingZeros(Convert.ToInt64(value), amountZeros);
		}

		public static string TrailingZeros(this long value, int amountZeros)
		{
			// Formateren naar het juiste formaat
			// Opschonen en verwijderen van ongewenste tekens
			// Verwijderen van leading zeros
			// Formateren met leading zeros
			string zeros = null;
			for (int i = 0; i < amountZeros; i++)
				zeros += "0";

			string pattern = "{0:" + (zeros ?? "0") + "}";
			return string.Format(pattern, value);
		}
		#endregion


		#region ToFraction

		public static double ToFraction<T>(this T percentage, PercentageRange range = PercentageRange.RangeZeroToHunderd)
			where T : struct
		{
			try
			{
				double p = (double)Convert.ChangeType(percentage, typeof(double));

				if (range == PercentageRange.RangeZeroToHunderd)
					return p / 100d;

				return p;

			}
			catch (Exception ex)
			{
				throw new InvalidCastException(null, ex);
			}
		}

		public static double? ToFraction<T>(this T? percentage)
			where T : struct
		{
			if (!percentage.HasValue)
				return null;

			return ToFraction(percentage.Value, PercentageRange.RangeZeroToHunderd);
		}

		public static double? ToFraction<T>(this T? percentage, PercentageRange range)
			where T : struct
		{
			if (!percentage.HasValue)
				return null;

			return ToFraction(percentage.Value, range);
		}

		#endregion

		#region ToPercentage

		public static double ToPercentage<T>(this T percentage)
			where T : struct
		{
			try
			{
				double p = (double)Convert.ChangeType(percentage, typeof(double));
				return p * 100d;
			}
			catch (Exception ex)
			{
				throw new InvalidCastException(null, ex);
			}
		}

		public static Double ToPercentage<T>(this T percentage, int amount)
			where T : struct
		{
			try
			{
				if (amount < 0 || amount > 8)
					throw new ArgumentOutOfRangeException();

				string format = "{0:#0.";
				for (int i = 0; i <= amount - 1; i++)
					format += "#";
				format += "}";

				return Convert.ToDouble(string.Format(format, ToPercentage(percentage)));
			}
			catch (Exception ex)
			{
				throw new InvalidCastException(null, ex);
			}
		}
		public static double? ToPercentage<T>(this T? percentage)
			where T : struct
		{
			return !percentage.HasValue ? default(double?) : ToPercentage(percentage.Value);
		}

		/// <summary>
		/// Decimal to percentage
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="percentage"></param>
		/// <param name="amount">0 to 8</param>
		/// <returns></returns>
		public static double? ToPercentage<T>(this T? percentage, int amount)
			where T : struct
		{
			return !percentage.HasValue ? default(double?) : ToPercentage(percentage.Value, amount);
		}

		#endregion

		#region ToDecimalFraction

		public static decimal ToDecimalFraction<T>(this T percentage, PercentageRange range = PercentageRange.RangeZeroToHunderd)
			where T : struct
		{
			try
			{
				decimal p = (decimal)Convert.ChangeType(percentage, typeof(decimal));

				if (range == PercentageRange.RangeZeroToHunderd)
					return p / 100m;

				return p;

			}
			catch (Exception ex)
			{
				throw new InvalidCastException(null, ex);
			}
		}

		public static decimal? ToDecimalFraction<T>(this T? percentage)
			where T : struct
		{
			if (!percentage.HasValue)
				return null;

			return ToDecimalFraction(percentage.Value, PercentageRange.RangeZeroToHunderd);
		}

		public static decimal? ToDecimalFraction<T>(this T? percentage, PercentageRange range)
			where T : struct
		{
			if (!percentage.HasValue)
				return null;

			return ToDecimalFraction(percentage.Value, range);
		}

		#endregion

		#region ToDecimalPercentage

		public static decimal ToDecimalPercentage<T>(this T percentage)
			where T : struct
		{
			try
			{
				decimal p = (decimal)Convert.ChangeType(percentage, typeof(decimal));
				return p * 100m;
			}
			catch (Exception ex)
			{
				throw new InvalidCastException(null, ex);
			}
		}

		public static decimal ToDecimalPercentage<T>(this T percentage, int amount)
			where T : struct
		{
			try
			{
				if (amount < 0 || amount > 8)
					throw new ArgumentOutOfRangeException();

				string format = "{0:#0.";
				for (int i = 0; i <= amount - 1; i++)
					format += "#";
				format += "}";

				return Convert.ToDecimal(string.Format(format, ToPercentage(percentage)));
			}
			catch (Exception ex)
			{
				throw new InvalidCastException(null, ex);
			}
		}

		public static decimal? ToDecimalPercentage<T>(this T? percentage)
			where T : struct
		{
			return !percentage.HasValue ? default(decimal?) : ToDecimalPercentage(percentage.Value);
		}

		/// <summary>
		/// Decimal to percentage
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="percentage"></param>
		/// <param name="amount">0 to 8</param>
		/// <returns></returns>
		public static decimal? ToDecimalPercentage<T>(this T? percentage, int amount)
			where T : struct
		{
			return !percentage.HasValue ? default(decimal?) : ToDecimalPercentage(percentage.Value, amount);
		}

		#endregion

	}
}
