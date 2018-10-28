using System;

namespace HelperTools.Collections
{
    public static class RangeHelper
    {
        public static bool IsInRange<T>(this T value, T minValue, T maxValue, bool useNull = true) where T : struct
        {
            if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTimeOffset))
                throw new InvalidCastException();

            return (double)Convert.ChangeType(value, typeof(double)) <= (double)Convert.ChangeType(maxValue, typeof(double))
                && (double)Convert.ChangeType(value, typeof(double)) >= (double)Convert.ChangeType(minValue, typeof(double));
        }

        //public static T ForceToRange<T>(this T value, T maxValue) where T : struct
        //{
        //	return MathExt.Max<T>((T)Convert.ChangeType(0, typeof(T)), MathExt.Min<T>(value, maxValue));
        //}

        //public static T ForceToRange<T>(this T value, T minValue, T maxValue) where T : struct
        //{
        //	return MathExt.Max<T>(minValue, MathExt.Min<T>(value, maxValue));
        //}

    }
}
