using System;
using static System.Convert;

namespace HelperTools
{
	public static partial class PowerExt
	{
		#region Power
		public static double? Pow<T>(T? x, T? y) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			if (!x.HasValue || !y.HasValue)
				return null;
			try
			{
				return System.Math.Pow(ToDouble(x.Value), ToDouble(y.Value));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		public static double? Pow<T>(T x, T? y) where T : struct
		{

			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			if (!y.HasValue)
				return null;

			try
			{
				return System.Math.Pow(ToDouble(x), ToDouble(y.Value));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}


		public static double? Pow<T>(T? x, T y) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			if (!x.HasValue)
				return null;

			try
			{
				return System.Math.Pow(ToDouble(x.Value), ToDouble(y));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		public static double? Pow<T, TU>(T? x, TU? y)
			where T : struct
			where TU : struct
		{

			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			if (!x.HasValue || !y.HasValue)
				return null;
			try
			{
				return System.Math.Pow(ToDouble(x.Value), ToDouble(y.Value));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		public static double? Pow<T, TU>(T x, TU? y)
			where T : struct
			where TU : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			if (!y.HasValue)
				return null;

			try
			{
				return System.Math.Pow(ToDouble(x), ToDouble(y.Value));
			}
			catch
			{
				throw new InvalidCastException();
			}

		}

		public static double? Pow<T, TU>(T? x, TU y)
			where T : struct
			where TU : struct
		{

			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			if (!x.HasValue)
				return null;

			try
			{
				return System.Math.Pow(ToDouble(x.Value), ToDouble(y));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}


		public static double Pow<T, TU>(T x, TU y)
			where T : struct
			where TU : struct
		{
			try
			{
				if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
					throw new InvalidCastException();

				return System.Math.Pow(ToDouble(x), ToDouble(y));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		public static double Pow<T>(T x, T y) where T : struct
		{
			try
			{
				if (typeof(T) == typeof(DateTime))
					throw new InvalidCastException();

				return System.Math.Pow(ToDouble(x), ToDouble(y));
			}
			catch
			{
				throw new InvalidCastException();
			}
		}

		#endregion

	}
}