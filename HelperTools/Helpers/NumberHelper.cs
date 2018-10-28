using System;

namespace HelperTools.Helpers
{
	public static class NumberHelper
	{

		#region NumbersAmount

		public static int NumbersAmount<T>(this T value) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return value.ToString().Length;
		}

		public static int NumbersAmount<T>(this T? value) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return !value.HasValue ? 0 : NumbersAmount(value.Value);
		}
		#endregion

		#region NotNegative
		/// <summary>
		/// Sets the value to 0 when negative.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns>0 when negative. The value when positive</returns>
		static public T NotNegative<T>(this T value) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return MathExt.Max<T>(value, (T)Convert.ChangeType(0, typeof(T)));
		}

		/// <summary>
		/// Sets the value to 0 when negative.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns>0 when negative. The value when positive</returns>
		static public T NotNegative<T>(this T? value) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return MathExt.Max<T>(value, (T)Convert.ChangeType(0, typeof(T)));
		}

		/// <summary>
		/// Sets the value to the specified minValue when negative.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="minValue">The minimum value</param>
		/// <returns>minValue when negative. The value when positive</returns>
		static public T NotNegative<T>(this T? value, T minValue) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			if (Convert.ToDouble((T)Convert.ChangeType(minValue, typeof(T))) < 0d)
				minValue = (T)Convert.ChangeType(0, typeof(T));

			return MathExt.Max<T>(value, minValue);
		}

		/// <summary>
		/// Sets the value to the specified minValue when negative.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <param name="minValue">The minimum value</param>
		/// <returns>minValue when negative. The value when positive</returns>
		static public T NotNegative<T>(this T value, T minValue) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return MathExt.Max<T>(value, NotNegative(minValue));
		}

		static public TU NotNegative<T, TU>(this T value)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return MathExt.Max((TU)Convert.ChangeType(0, typeof(TU)), value);
		}

		static public TU NotNegative<T, TU>(this T? value)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return MathExt.Max((TU)Convert.ChangeType(0, typeof(TU)), value);
		}

		static public TU NotNegative<T, TU>(this T value, TU minValue)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return MathExt.Max(NotNegative(minValue), value);
		}

		public static TU NotNegative<T, TU>(this T? value, TU minValue)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			return MathExt.Max(NotNegative(minValue), value);
		}
		#endregion

		#region NotNegativeNullable

		/// <summary>
		/// Corrects the value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns>The value when positive, when negative the null value and a null when null</returns>
		public static T? NotNegativeNullable<T>(this T? value) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			if (!value.HasValue)
				return null;

			return MathExt.Max(value, 0);
		}

		public static T? NotNegativeNullable<T>(this T? value, T minValue)
			where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			if (!value.HasValue)
				return null;

			if (Convert.ToDouble((T)Convert.ChangeType(minValue, typeof(T))) < 0d)
				minValue = (T)Convert.ChangeType(0, typeof(T));

			return MathExt.Max<T>(value, minValue);
		}

		public static TU? NotNegativeNullable<T, TU>(this T? value)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			if (!value.HasValue)
				return null;

			return MathExt.Max((TU)Convert.ChangeType(0, typeof(TU)), value);
		}

		public static TU? NotNegativeNullable<T, TU>(this T? value, TU minValue)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			if (!value.HasValue)
				return null;

			if (Convert.ToDouble((TU)Convert.ChangeType(minValue, typeof(TU))) < 0d)
				minValue = (TU)Convert.ChangeType(0, typeof(T));

			return MathExt.Max(minValue, value);
		}

		/// <summary>
		/// Corrects the value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">The value.</param>
		/// <returns>The value or an 0 when value is null</returns>
		public static T NotNullable<T>(this T? value) where T : struct
		{
			try
			{
				return value.HasValue ? value.Value : (T)Convert.ChangeType(0, typeof(T));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		#endregion

		public static bool IsInteger(string value)
		{
			return value.HasOnlyDigits();
		}
	}
}
