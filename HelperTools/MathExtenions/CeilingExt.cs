using System;
using static System.Convert;

namespace HelperTools
{
	public static partial class MathExt
	{


		#region Ceiling
		public static long? Ceiling<T>(T? x) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return x.HasValue ? Ceiling(x.Value) : default(long?);
		}

		public static long Ceiling<T>(T x) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();
			return (long)System.Math.Ceiling(ToDouble(x));
		}

		public static TU Ceiling<T, TU>(T x) where T : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			return (TU)ChangeType(System.Math.Ceiling(ToDouble(x)), typeof(TU));
		}

		#endregion

	}
}