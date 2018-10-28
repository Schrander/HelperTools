using System;
using static HelperTools.MathExt;
using static System.Convert;

namespace HelperTools.Helpers
{

    public static class RangeHelper
    {
        /// <summary>
        /// Indicates if a value is within a range.
        /// 
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="minValue">min value</param>
        /// <param name="maxValue">max value</param>
        /// <param name="useNull"></param>
        /// <returns></returns>
        public static bool IsInRange<T>(this T value, T minValue, T maxValue, bool useNull = true) where T : struct
        {
            if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTimeOffset))
            {
                var date = (DateTime)ChangeType(value, typeof(DateTime));
                var maxDate = (DateTime)ChangeType(maxValue, typeof(DateTime));
                var minDate = (DateTime)ChangeType(minValue, typeof(DateTime));

                return date.IsInDateRange(minDate, maxDate);
            }

            double doubleValue = (double)ChangeType(value, typeof(double));
            double maxDoubleValue = (double)ChangeType(maxValue, typeof(double));
            double minDoubleValue = (double)ChangeType(minValue, typeof(double));

            return doubleValue <= maxDoubleValue && doubleValue >= minDoubleValue;
        }

        /// <summary>
        /// Force range to 0 to specified max value;
        /// 
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="maxValue">max value</param>
        /// <returns></returns>
        public static T ForceToRange<T>(this T value, T maxValue) where T : struct
        {
            return Max<T>((T)ChangeType(0, typeof(T)), Min<T>(value, maxValue));
        }

        /// <summary>
        /// Force range to specified min and max value;
        /// 
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="minValue"></param>
        /// <param name="maxValue">max value</param>
        /// <returns></returns>
        public static T ForceToRange<T>(this T value, T minValue, T maxValue) where T : struct
        {
            return Max<T>(minValue, Min<T>(value, maxValue));
        }

        public static T? ForceToRange<T>(this T? value, T minValue, T maxValue) where T : struct
        {
            return Max<T>(minValue, Min<T>(value, maxValue));
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound till upper bound.
        /// This is inclusive the upper bound date.
        /// </summary>
        /// <param name="date">The given date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="upperBound">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInDateRange(this DateTime? date, DateTime? start, DateTime? upperBound)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, upperBound, RangeBoundaryType.Inclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound till upper bound.
        /// This is inclusive the upper bound date.
        /// </summary>
        /// <param name="date">The given date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="upperBound">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInDateRange(this DateTime date, DateTime? start, DateTime? upperBound)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, upperBound, RangeBoundaryType.Inclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound till upper bound.
        /// The range is inclusive the upper bound date.
        /// </summary>
        /// <param name="date">The given date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="upperBound">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInDateRange(this DateTime date, DateTime start, DateTime upperBound)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, upperBound, RangeBoundaryType.Inclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound till upper bound.
        /// This is inclusive the upper bound date.
        /// </summary>
        /// <param name="date">The given date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="startType">The lower bound type</param>
        /// <param name="end">The upper bound date.</param>
        /// <param name="endType">The upper bound type</param>
        /// <returns></returns>
        public static bool IsInDateRange(this DateTime date, DateTime start, RangeBoundaryType startType, DateTime end, RangeBoundaryType endType)
        {

            bool hasTimeSettings = start.TimeOfDay.Ticks != 0 || end.TimeOfDay.Ticks != 0;

            // If the lower bound date and upper bound date are the same day, but the time of day is not set.
            // Then de upperbound date is set to the next day minus one millisec.
            if (start.Equals(end) && !hasTimeSettings)
                end = end.AddDays(1).AddMilliseconds(-1);

            if (endType == RangeBoundaryType.Inclusive && end.TimeOfDay.Ticks == 0)
                end = end.AddDays(1).AddMilliseconds(-1);

            // Flushes time of day for the given date, if the lower and upper bound also no time of day are set.
            if (!hasTimeSettings)
                date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            return (startType == RangeBoundaryType.Exclusive && date > start || (startType == RangeBoundaryType.Inclusive && date >= start))
                    && ((endType == RangeBoundaryType.Exclusive && date < end) || (endType == RangeBoundaryType.Inclusive && date <= end));
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound till upper bound.
        /// The range is inclusive the upper bound date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="end">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInFromTillRange(this DateTime date, DateTime start, DateTime end)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, end, RangeBoundaryType.Inclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lowerbound till upperbound.
        /// The range is inclusive the upper bound date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="end">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInFromTillRange(this DateTime date, DateTime? start, DateTime? end)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, end, RangeBoundaryType.Inclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lowerbound till upperbound.
        /// The range is inclusive the upper bound date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="end">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInFromTillRange(this DateTime? date, DateTime? start, DateTime? end)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, end, RangeBoundaryType.Inclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound to upper bound.
        /// The range is exclusive the upper bound date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="end">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInFromToRange(this DateTime date, DateTime start, DateTime end)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, end, RangeBoundaryType.Exclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound to upper bound.
        /// The range is exclusive the upper bound date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="end">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInFromToRange(this DateTime date, DateTime? start, DateTime? end)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, end, RangeBoundaryType.Exclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound to upper bound.
        /// The range is exclusive the upper bound date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="start">The lower bound date.</param>
        /// <param name="end">The upper bound date.</param>
        /// <returns></returns>
        public static bool IsInFromToRange(this DateTime? date, DateTime? start, DateTime? end)
        {
            return IsInDateRange(date, start, RangeBoundaryType.Inclusive, end, RangeBoundaryType.Exclusive);
        }

        /// <summary>
        /// Determines whether the specified date is within the range from lower bound to upper bound.
        /// </summary>
        /// <param name="date">the date</param>
        /// <param name="start">start date</param>
        /// <param name="startType">start date boundary type</param>
        /// <param name="end">end date</param>
        /// <param name="endType">end date boundary type</param>
        /// <returns></returns>
        public static bool IsInDateRange(this DateTime? date, DateTime? start, RangeBoundaryType startType, DateTime? end, RangeBoundaryType endType)
        {
            if (!date.HasValue || !start.HasValue || !end.HasValue)
                return false;

            return IsInDateRange(date.Value, start.Value, startType, end.Value, endType);
        }



    }

}

