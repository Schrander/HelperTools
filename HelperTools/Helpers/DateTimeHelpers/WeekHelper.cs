using System;
using System.Globalization;

namespace HelperTools.Helpers.DateTimeHelpers
{
    public static partial class DateTimeExt
    {

        public static int WeekNumber(this DateTime date)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            int weekNum = culture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }


        public static int MaxWeekNumber(this DateTime date)
        {
            return MaxWeekNumber(date.Year);
        }

        public static int MaxWeekNumber(int year)
        {
            DateTimeFormatInfo dateInfo = DateTimeFormatInfo.CurrentInfo;
            DateTime lastDay = new DateTime(year, 12, 31);
            if (dateInfo == null)
                return 0;

            Calendar cal = dateInfo.Calendar;
            return cal.GetWeekOfYear(lastDay, dateInfo.CalendarWeekRule, dateInfo.FirstDayOfWeek);
        }

        /// <summary>
        /// Gives the last week number of the specified year.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int LastWeekNumberOfYear(this DateTime date)
        {
            return LastWeekNumberOfYear(date.Year);
        }

        /// <summary>
        /// Gives the last week number of the specified year.
        /// </summary>
        /// <param name="year">The given year.</param>
        /// <returns></returns>
        public static int LastWeekNumberOfYear(int year)
        {
            DateTimeFormatInfo dateInfo = DateTimeFormatInfo.CurrentInfo;
            return dateInfo?.Calendar.GetWeekOfYear(new DateTime(year, 12, 31), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) ?? 52;
        }

        /// <summary>
        /// Gives the first week of the year for the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int FirstWeekNumberOfYear(this DateTime date)
        {
            return FirstWeekNumberOfYear(date.Year);
        }

        /// <summary>
        /// Gives the first week of the year for the specified year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static int FirstWeekNumberOfYear(int year)
        {
            DateTimeFormatInfo dateInfo = DateTimeFormatInfo.CurrentInfo;
            if (dateInfo == null)
                return 1;

            Calendar cal = dateInfo.Calendar;
            return cal.GetWeekOfYear(new DateTime(year, 1, 1), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Gives the number of weeks for the specified year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static int NumberOfWeeksOfYear(int year)
        {
            return LastWeekNumberOfYear(year) + (FirstWeekNumberOfYear(year) != 1 ? 1 : 0);
        }

        /// <summary>
        /// Gives the number of weeks for the current year of the specified date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int NumberOfWeeksOfYear(this DateTime date)
        {
            return NumberOfWeeksOfYear(date.Year);
        }

        /// <summary>
        /// Gives the number of weeks for the specified date range.
        /// </summary>
        /// <param name="dateFrom">the from date</param>
        /// <param name="dateTo">the to date</param>
        /// <returns></returns>
        public static int NumberOfWeeks(this DateTime dateFrom, DateTime dateTo)
        {
            TimeSpan span = dateTo.Subtract(dateFrom);

            if (span.Days <= 7)
                return dateFrom.DayOfWeek > dateTo.DayOfWeek ? 2 : 0;

            int days = span.Days - 7 + (int)dateFrom.DayOfWeek;
            int weekCount;
            int dayCount = 0;

            for (weekCount = 1; dayCount < days; weekCount++)
                dayCount += 7;

            return weekCount;
        }


        #region Week


        public static DateTime GetBeginOfWeek()
        {
            DateTime beginOfWeek = DateTime.Now.DayOfWeek == DayOfWeek.Sunday
                ? DateTime.Now.AddDays(-6)
                : DateTime.Now.AddDays((double)(DateTime.Now.DayOfWeek - 1) * -1);
            return beginOfWeek;
        }

        public static bool IsWeekendDay(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsWeekendDay(this DateTime? date)
        {
            return date.HasValue && IsWeekendDay(date.Value);
        }

        public static int WeeksInMonth(this DateTime time)
        {
            int days = DateTime.DaysInMonth(time.Year, time.Month);
            var firstDay = new DateTime(time.Year, time.Month, 1);
            var firstDayDayOfWeek = (int)firstDay.DayOfWeek;
            return (int)Math.Ceiling((firstDayDayOfWeek + days) / 7.0);
        }


        #endregion
    }
}