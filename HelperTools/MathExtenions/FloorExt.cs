using System;
using static System.Convert;

namespace HelperTools
{
	public static partial class MathExt
	{

		#region Floor
		public static long? Floor<T>(T? x) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return x.HasValue ? Floor(x.Value) : default(long?);
		}

		public static long Floor<T>(T x) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new InvalidCastException();

			return (long)System.Math.Floor(Convert.ToDouble(x));
		}

		public static TU Floor<T, TU>(T x) where T : struct
		{
			if (typeof(T) == typeof(DateTime) || typeof(TU) == typeof(DateTime))
				throw new InvalidCastException();

			return (TU)ChangeType(System.Math.Floor(ToDouble(x)), typeof(TU));
		}

		#endregion

	}
}