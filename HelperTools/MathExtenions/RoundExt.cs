using System;
using static System.Convert;

namespace HelperTools
{
	public static partial class RoundExt
	{
		#region Round

		public static long? Round<T>(T? x) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return x.HasValue ? Round(x.Value) : default(long?);
		}

		public static long Round<T>(T x) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return (long)System.Math.Round(ToDouble(x));
		}

		public static TU Round<T, TU>(T x) where T : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			return (TU)ChangeType(System.Math.Round(ToDouble(x)), typeof(TU));
		}



		#endregion

	}
}