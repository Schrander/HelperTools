using System;
using static System.Convert;
using System.Collections.Generic;

namespace HelperTools
{
	public static partial class MathExt
	{

		#region Max
		public static T Max<T>(T x, T y) where T : struct
		{
			return (Comparer<T>.Default.Compare(x, y) > 0) ? x : y;
		}

		public static T Max<T, TU>(T x, TU y)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			return (T)ChangeType(Max<double>(ToDouble(x), ToDouble(y)), typeof(T));
		}

		public static T? Max<T>(T? x, T? y) where T : struct
		{
			if (!x.HasValue && !y.HasValue)
				return null;

			return x.HasValue ? Max<T>(x.Value, y) : Max(null, y.Value);
		}

		public static T Max<T>(T x, T? y) where T : struct
		{
			return y.HasValue ? Max<T>(x, y.Value) : x;
		}

		public static T Max<T>(T? x, T y) where T : struct
		{
			return x.HasValue ? Max<T>(x.Value, y) : y;
		}

		public static T? Max<T, U>(T? x, U? y)
			where T : struct
			where U : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(U) == typeof(DateTime))
				throw new InvalidCastException();

			if (!x.HasValue && !y.HasValue)
				return null;
			try
			{
				double? xValue = x.HasValue ? ToDouble(x.Value) : (double?)null;
				double? yValue = y.HasValue ? ToDouble(y.Value) : (double?)null;

				return (T)ChangeType(Max<double>(xValue, yValue), typeof(T));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		public static T Max<T, U>(T x, U? y)
			where T : struct
			where U : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(U) == typeof(DateTime))
				throw new InvalidCastException();
			try
			{
				double? yValue = y.HasValue ? ToDouble(y) : (double?)null;
				return (T)ChangeType(Max<double>(ToDouble(x), yValue), typeof(T));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		public static T Max<T, U>(T? x, U y)
			where T : struct
			where U : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(U) == typeof(DateTime))
				throw new InvalidCastException();
			try
			{
				double? xValue = x.HasValue ? ToDouble(x) : (double?)null;
				return (T)ChangeType(Max<double>(xValue, ToDouble(y)), typeof(T));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}
		#endregion

	}
}