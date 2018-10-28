using System;
using System.Collections.Generic;
using static System.Convert;

namespace HelperTools
{
	public static partial class MathExt
	{
		#region Min

		public static T Min<T>(T x, T y) where T : struct
		{
			return (Comparer<T>.Default.Compare(x, y) > 0) ? y : x;
		}

		public static T Min<T, TU>(T x, TU y)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();
			try
			{
				double xDouble = ToDouble(x);
				double yDouble = ToDouble(y);
				double d = (Comparer<double>.Default.Compare(xDouble, yDouble) > 0) ? yDouble : xDouble;

				return (T)ChangeType(d, typeof(T));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		public static T? Min<T>(T? x, T? y) where T : struct
		{
			if (!x.HasValue && !y.HasValue)
				return null;

			return x.HasValue ? Min<T>(x.Value, y) : Min(null, y.Value);
		}

		public static T Min<T>(T x, T? y) where T : struct
		{
			return y.HasValue ? Min<T>(x, y.Value) : x;
		}

		public static T Min<T>(T? x, T y) where T : struct
		{
			return x.HasValue ? Min<T>(x.Value, y) : y;
		}

		public static T? Min<T, TU>(T? x, TU? y) where T : struct where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			if (!x.HasValue && !y.HasValue)
				return null;

			try
			{
				double? xValue = x.HasValue ? ToDouble(x.Value) : (double?)null;
				double? yValue = y.HasValue ? ToDouble(y.Value) : (double?)null;

				return (T)ChangeType(Min<double>(xValue, yValue), typeof(T));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		public static T Min<T, TU>(T x, TU? y) where T : struct where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();
			try
			{
				double xValue = ToDouble(x);
				double? yValue = y.HasValue ? ToDouble(y.Value) : (double?)null;

				return (T)ChangeType(Min<double>(xValue, yValue), typeof(T));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}


		public static T Min<T, TU>(T? x, TU y) where T : struct where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			double? xValue = x.HasValue ? ToDouble(x.Value) : (double?)null;

			return (T)ChangeType(Min<double>(xValue, ToDouble(y)), typeof(T));
		}

		#endregion
	}
}