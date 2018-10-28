using System;

namespace HelperTools.Helpers.DateTimeHelpers
{
    public static partial class DateTimeExt
    {

        /// <summary>
        ///  Bepaalt of verjaardag al is geweest
        /// </summary>
        public static bool HadBirthday(this DateTime date)
        {
            return date.HasPassed();
        }

        public static bool HadBirthday(this DateTime? date)
        {
            return date.HasValue && date.HasPassed();
        }

        /// <summary>
        ///  Bepaalt of een bepaalde dag al geweest in het jaar
        /// </summary>
        public static bool DayHasPassed(this DateTime date)
        {
            DateTime newDate = new DateTime(DateTime.Today.Year, date.Month, date.Day);
            return new DateTime(DateTime.Now.Subtract(newDate).Ticks).Day > 0;
        }
        public static bool DayHasPassed(this DateTime? date)
        {
            return date.HasValue && DayHasPassed(date.Value);
        }

        #region DaysToDate

        public static int DaysToDate(this DateTime date, DateTime refDate)
        {
            return (date - refDate).Days;
        }

        public static int DaysToDate(this DateTime date)
        {
            return DaysToDate(date, DateTime.Now);
        }

        public static int DaysToDate(this DateTime? date)
        {
            return date.HasValue ? DaysToDate(date.Value, DateTime.Now) : default(int);
        }

        public static int DaysToDate(this DateTime? date, DateTime refDate)
        {
            return date.HasValue ? DaysToDate(date.Value, refDate) : default(int);
        }

        public static int DaysToDate(this DateTime? date, DateTime? refDate)
        {
            return date.HasValue && refDate.HasValue ? DaysToDate(date.Value, refDate.Value) : default(int);
        }

        #endregion

        #region DaysFromDate

        public static int DaysFromDate(this DateTime date, DateTime refDate)
        {
            return (refDate - date).Days;
        }

        public static int DaysFromDate(this DateTime date)
        {
            return DaysToDate(date, DateTime.Now);
        }

        public static int DaysFromDate(this DateTime? date)
        {
            return date.HasValue ? DaysFromDate(date.Value, DateTime.Now) : default(int);
        }

        public static int DaysFromDate(this DateTime? date, DateTime refDate)
        {
            return date.HasValue ? DaysFromDate(date.Value, refDate) : default(int);
        }

        public static int DaysFromDate(this DateTime? date, DateTime? refDate)
        {
            return date.HasValue && refDate.HasValue ? DaysFromDate(date.Value, refDate.Value) : default(int);
        }

        #endregion

        #region HasPassed

        public static bool HasPassed(this DateTime date)
        {
            return HasPassed(date, DateTime.Now);
        }

        public static bool HasPassed(this DateTime? date)
        {
            return date.HasValue && HasPassed(date.Value, DateTime.Now);
        }

        public static bool HasPassed(this DateTime? date, DateTime refDate)
        {
            return date.HasValue && HasPassed(date.Value, refDate);
        }

        public static bool HasPassed(this DateTime? date, DateTime? refDate)
        {
            return date.HasValue && refDate.HasValue && HasPassed(date.Value, refDate.Value);
        }

        public static bool HasPassed(this DateTime date, DateTime refDate)
        {
            return date > refDate;
        }

        #endregion



        /// <summary>
        /// Gives the New Year's Eve for a specified year.
        /// </summary>
        public static DateTime OldYearsDay(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        public static DateTime OldYearsDay(int year)
        {
            return new DateTime(year, 12, 31);
        }

        public static DateTime? OldYearsDay(this DateTime? date)
        {
            return date.HasValue ? OldYearsDay(date.Value) : default(DateTime?);
        }


        /// <summary>
        /// Gives the newyearsday for a specified year.
        /// </summary>

        public static DateTime NewYearsDay(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime NewYearsDay(int year)
        {
            return new DateTime(year, 1, 1);
        }

        public static DateTime? NewYearsDay(this DateTime? date)
        {
            return date.HasValue ? NewYearsDay(date.Value) : default(DateTime?);
        }

        #region DaysInYear

        /// <summary>
        /// Number of days in specified year.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int DaysInYear(this DateTime date)
        {
            return DaysInYear(date.Year);
        }

        public static int DaysInYear(int year)
        {
            return DateTime.IsLeapYear(year) ? 366 : 365;
        }

        public static int? DaysInYear(this DateTime? date)
        {
            return date.HasValue ? DaysInYear(date.Value.Year) : default(int?);
        }

        #endregion

        #region Expires
        /// <summary>
        /// Will be expired after the calculated days.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int ExpiresInDays(this DateTime date)
        {
            return ExpiresInDays(date, DateTime.Now);
        }

        public static int ExpiresInDays(this DateTime? date)
        {
            return date.HasValue ? ExpiresInDays(date.Value, DateTime.Now) : 0;
        }

        public static int ExpiresInDays(this DateTime date, DateTime? refDate)
        {
            return refDate.HasValue && !date.IsExpired(refDate.Value) ? ExpiresInDays(date, refDate.Value) : 0;
        }

        public static int ExpiresInDays(this DateTime date, DateTime refDate)
        {
            return Convert.ToInt32((date - refDate).TotalDays);
        }

        #endregion

        #region IsExpired

        public static bool IsExpired(this DateTime date)
        {
            return IsExpired(date, DateTime.Now);
        }

        public static bool IsExpired(this DateTime? date)
        {
            return IsExpired(date, DateTime.Now);
        }

        public static bool IsExpired(this DateTime date, DateTime? refDate)
        {
            return refDate.HasValue && IsExpired(date, refDate.Value);
        }

        public static bool IsExpired(this DateTime? date, DateTime? refDate)
        {
            return date.HasValue && refDate.HasValue && IsExpired(date, refDate.Value);
        }

        public static bool IsExpired(this DateTime date, DateTime refDate)
        {
            return refDate >= date;
        }

        #endregion


        /// <summary>
        /// Gives the number of nights between arrival and departure.
        /// </summary>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <returns></returns>
        public static int NightsArrivalDeparture(this DateTime arrivalDate, DateTime departureDate)
        {
            return (int)departureDate.Date.Subtract(arrivalDate.Date).TotalDays;
        }

        /// <summary>
        /// Gives the number of nights between FromDate and TillDate.
        /// </summary>
        /// <param name="lowerbound">the from date.</param>
        /// <param name="upperbound">the till date.</param>
        /// <returns></returns>
        public static int NightsFromTill(this DateTime lowerbound, DateTime upperbound)
        {
            return (int)upperbound.Date.Subtract(lowerbound.Date).TotalDays;
        }

        /// <summary>
        /// Gives the number of nights from fromdate to ToDate.
        /// </summary>
        /// <param name="arrivalDate">the from date</param>
        /// <param name="departureDate">the to date</param>
        /// <returns></returns>
        public static int NightsFromTo(this DateTime arrivalDate, DateTime departureDate)
        {
            return (int)departureDate.Date.Subtract(arrivalDate.Date).TotalDays - 1;
        }

        /// <summary>
        /// Gives the number of days between arrival and departure.
        /// </summary>
        /// <param name="arrivalDate">The arrival date.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <returns></returns>
        public static int DaysArrivalDeparture(this DateTime arrivalDate, DateTime departureDate)
        {
            return (int)departureDate.AddDays(1).Date.Subtract(arrivalDate.Date).TotalDays;
        }

        /// <summary>
        /// Gives the number of days between fromDate and TillDate.
        /// </summary>
        /// <param name="arrivalDate">The from date.</param>
        /// <param name="depatureDate">The till date.</param>
        /// <returns></returns>
        public static int DaysFromTill(this DateTime arrivalDate, DateTime depatureDate)
        {
            return (int)depatureDate.AddDays(1).Date.Subtract(arrivalDate.Date).TotalDays;
        }

        /// <summary>
        /// Gives the number of days from fromdate to ToDate.
        /// </summary>
        /// <param name="arrivalDate">the from date</param>
        /// <param name="departureDate">the to date</param>
        /// <returns></returns>
        public static int DaysFromTo(this DateTime arrivalDate, DateTime departureDate)
        {
            return (int)departureDate.Date.Subtract(arrivalDate.Date).TotalDays;
        }

        /// <summary>
        /// Geeft de schrikkeldag van een schrikkeljaar. Zo niet dan niets.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? LeapDay(this DateTime date)
        {
            return LeapDay(date.Year);
        }

        public static DateTime? LeapDay(this DateTime? date)
        {
            return date.HasValue ? LeapDay(date.Value.Year) : default(DateTime?);
        }

        public static DateTime? LeapDay(int year)
        {
            return DateTime.IsLeapYear(year) ? new DateTime(year, 2, 29) : default(DateTime?);
        }

        /// <summary>
        /// Determines whether the date is a leap day
        /// </summary>
        public static bool IsLeapDay(this DateTime date)
        {
            return DateTime.IsLeapYear(date.Year) && date.Equals(new DateTime(date.Year, 2, 29));
        }

        public static bool IsLeapDay(this DateTime? date)
        {
            return date.HasValue && IsLeapDay(date.Value);
        }


    }
}