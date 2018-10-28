using System;
using System.Globalization;
using HelperTools.Helpers;

namespace HelperTools.Extensions
{
	public static class DateTimeExt
	{

		public static bool TryParse(string sDate, out DateTime? outDate)
		{
			outDate = default(DateTime?);

			if (string.IsNullOrWhiteSpace(sDate))
				return false;

			DateTime oDate;
			bool isSuccess = DateTime.TryParse(sDate, out oDate);
			if (isSuccess)
				outDate = oDate;

			return isSuccess;
		}

		public static DateTime? GetDateTime(string date)
		{
			if (string.IsNullOrWhiteSpace(date))
				return default(DateTime?);

			DateTime oDate = DateTime.MinValue;
			bool isSuccess = DateTime.TryParse(date, out oDate);
			return isSuccess ? oDate : default(DateTime?);
		}

		#region Age

		public static int? Age(this DateTime? date)
		{
			return date.HasValue ? Age(date.Value) : default(int?);
		}

		public static int Age(this DateTime date)
		{
			return Age(date, DateTime.Now);
		}

		public static int Age(this DateTime date, DateTime refDate)
		{
			try
			{
				int age = (refDate <= date) ? date.Year - refDate.Year : refDate.Year - date.Year;

				if (age != 0 && date > refDate.AddYears(-age))
					age--;

				return age;
			}
			catch (Exception ex)
			{
				throw new Exception("Ongeldige datum", ex.InnerException);
			}

		}

		public static int? Age(this DateTime date, DateTime? refDate)
		{
			return refDate.HasValue ? Age(date, refDate.Value) : default(int?);
		}

		public static int? Age(this DateTime? date, DateTime refDate)
		{
			return date.HasValue ? Age(date.Value, refDate) : default(int?);
		}

		public static int? Age(this DateTime? date, DateTime? refDate)
		{
			return (date.HasValue && refDate.HasValue) ? Age(date.Value, refDate.Value) : default(int?);
		}

		public static int Age(this DateTime date, TimeUnit unit)
		{
			return Age(date, DateTime.Now, unit);
		}

		public static int? Age(this DateTime? date, TimeUnit unit)
		{
			return date.HasValue ? Age(date.Value, DateTime.Now, unit) : default(int?);
		}

		public static int? Age(this DateTime date, DateTime? refDate, TimeUnit unit)
		{
			return refDate.HasValue ? Age(date, refDate.Value, unit) : default(int?);
		}

		public static int? Age(this DateTime? date, DateTime refDate, TimeUnit unit)
		{
			return date.HasValue ? Age(date.Value, refDate, unit) : default(int?);
		}

		public static int? Age(this DateTime? date, DateTime? refDate, TimeUnit unit)
		{
			return (date.HasValue && refDate.HasValue) ? Age(date.Value, refDate.Value, unit) : default(int?);
		}

		public static int Age(this DateTime date, DateTime refDate, TimeUnit unit)
		{
			if (date >= refDate)
				return 0;

			TimeSpan period = refDate - date;

			switch (unit)
			{
				case TimeUnit.Days:
					return period.Days;

				case TimeUnit.Years:
					return Age(date, refDate);

				case TimeUnit.Months:
					return MonthAmount(date, refDate);

				case TimeUnit.Hours:
					return (int)period.TotalHours;

				default:
					return Age(date, refDate);
			}
		}

		private static int MonthAmount(DateTime lowerBound, DateTime upperBound)
		{
			int amount = 0;
			while (lowerBound.AddMonths(1) <= upperBound)
			{
				lowerBound = lowerBound.AddMonths(1);
				amount++;
			}
			return amount;
		}


		#region Age In Months

		public static int? AgeInMonths(this DateTime? date)
		{
			return date.HasValue ? AgeInMonths(date.Value) : default(int?);
		}

		public static int AgeInMonths(this DateTime date)
		{
			return Age(date, TimeUnit.Months);
		}

		public static int? AgeInMonths(this DateTime? date, DateTime refDate)
		{
			return date.HasValue ? AgeInMonths(date.Value, refDate) : default(int?);
		}

		public static int? AgeInMonths(this DateTime? date, DateTime? refDate)
		{
			return (date.HasValue && refDate.HasValue) ? AgeInMonths(date.Value, refDate.Value) : default(int?);
		}

		public static int? AgeInMonths(this DateTime date, DateTime? refDate)
		{
			return refDate.HasValue ? AgeInMonths(date, refDate.Value) : default(int?);
		}

		public static int AgeInMonths(this DateTime date, DateTime refDate)
		{
			return Age(date, refDate, TimeUnit.Months);
		}


		#endregion

		#region Age in Years


		public static int? AgeInYears(this DateTime? date)
		{
			return date.HasValue ? AgeInYears(date.Value) : default(int?);
		}

		public static int? AgeInYears(this DateTime? date, DateTime refDate)
		{
			return date.HasValue ? AgeInYears(date.Value, refDate) : default(int?);
		}

		public static int? AgeInYears(this DateTime date, DateTime? refDate)
		{
			return refDate.HasValue ? AgeInYears(date, refDate.Value) : default(int?);
		}

		public static int? AgeInYears(this DateTime? date, DateTime? refDate)
		{
			return (date.HasValue && refDate.HasValue) ? AgeInYears(date.Value, refDate.Value) : default(int?);
		}

		public static int AgeInYears(this DateTime date)
		{
			return Age(date, TimeUnit.Years);
		}

		public static int AgeInYears(this DateTime date, DateTime refDate)
		{
			return Age(date, refDate, TimeUnit.Years);
		}

		#endregion

		#region Age in Days

		public static int? AgeInDays(this DateTime? date)
		{
			return date.HasValue ? AgeInDays(date.Value) : default(int?);
		}

		public static int? AgeInDays(this DateTime? date, DateTime refDate)
		{
			return date.HasValue ? AgeInDays(date.Value, refDate) : default(int?);
		}

		public static int? AgeInDays(this DateTime date, DateTime? refDate)
		{
			return refDate.HasValue ? AgeInDays(date, refDate.Value) : default(int?);
		}

		public static int? AgeInDays(this DateTime? date, DateTime? refDate)
		{
			return (date.HasValue && refDate.HasValue) ? AgeInDays(date.Value, refDate.Value) : default(int?);
		}

		public static int AgeInDays(this DateTime date)
		{
			return Age(date, TimeUnit.Days);
		}

		public static int AgeInDays(this DateTime date, DateTime refDate)
		{
			return Age(date, refDate, TimeUnit.Days);
		}

		#endregion

		#region Age in Hours

		public static int? AgeInHours(this DateTime? date)
		{
			return date.HasValue ? AgeInHours(date.Value) : default(int?);
		}

		public static int? AgeInHours(this DateTime? date, DateTime refDate)
		{
			return date.HasValue ? AgeInHours(date.Value, refDate) : default(int?);
		}

		public static int? AgeInHours(this DateTime date, DateTime? refDate)
		{
			return refDate.HasValue ? AgeInHours(date, refDate.Value) : default(int?);
		}

		public static int? AgeInHours(this DateTime? date, DateTime? refDate)
		{
			return (date.HasValue && refDate.HasValue) ? AgeInHours(date.Value, refDate.Value) : default(int?);
		}

		public static int AgeInHours(this DateTime date)
		{
			return Age(date, TimeUnit.Hours);
		}

		public static int AgeInHours(this DateTime date, DateTime refDate)
		{
			return Age(date, refDate, TimeUnit.Hours);
		}

		#endregion

		#endregion

		#region Years

		public static int Years(this DateTime startDate, DateTime endDate)
		{
			int yearDif = endDate.Year - startDate.Year;

			if (yearDif == 1)
			{
				DateTime? leapDay = endDate.LeapDay();
				// Einddatum schrikkeljaar. Eindatum kleiner en gelijk aan schrikkeldag. 
				// Looptijd minder dan 1 jaar;
				if (yearDif == 1 && endDate.IsLeapYear() && leapDay.HasValue && endDate >= leapDay && startDate.AddYears(1) >= endDate)
					return 1;

				// Startdatum is een schrikkeljaar
				// Looptijd minder dan 1 jaar
				leapDay = startDate.LeapDay();
				if (startDate.IsLeapYear() && leapDay.HasValue && startDate <= leapDay.Value && startDate.AddYears(1) >= endDate)
					return 1;
			}

			return yearDif + 1;
		}

		public static int Years(this DateTime? startDate, DateTime? endDate)
		{
			return !startDate.HasValue || !endDate.HasValue ? 0 : Years(startDate.Value, endDate.Value);
		}

		public static int Years(this DateTime? startDate, DateTime endDate)
		{
			return !startDate.HasValue ? 0 : Years(startDate.Value, endDate);
		}
		
		public static int Years(this DateTime startDate, DateTime? endDate)
		{
			return !endDate.HasValue ? 0 : Years(startDate, endDate.Value);
		}

		#endregion

		#region Universal Time

		public static DateTime? ToUniversalTime(this DateTime? date)
		{
			return !date.HasValue ? default(DateTime?) : date.Value.ToUniversalTime();
		}

		public static DateTime? ToUniversalTime(string date)
		{
			if (!string.IsNullOrEmpty(date))
				return default(DateTime?);

			DateTime d;
			return DateTime.TryParse(date, out d) ? d.ToUniversalTime() : default(DateTime?);
		}

		public static DateTime? ToLocalTime(this DateTime? date)
		{
			return date.HasValue ? date.Value.ToLocalTime() : default(DateTime?);
		}


		#endregion

		#region HasTimeStamp

		public static bool HasTimeStamp(this DateTime date)
		{
			return !(date.Hour == 0 && date.Minute == 0 && date.Second == 0);
		}

		public static bool HasTimeStamp(this DateTime? date)
		{
			return date.HasValue && HasTimeStamp(date.Value);
		}

		//public static bool? HasTimeStamp(this DateTime? date) {
		//  return date.HasValue ? HasTimeStamp(date.Value) : default(bool?);
		//}
		#endregion

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

		#region Is Age

		public static bool IsAge(this DateTime date, int age)
		{
			return IsAge(date, DateTime.Now, age);
		}

		public static bool IsAge(this DateTime? date, int age)
		{
			return date.HasValue && IsAge(date.Value, DateTime.Now, age);
		}

		public static bool IsAge(this DateTime? date, DateTime refDate, int age)
		{
			return date.HasValue && IsAge(date.Value, refDate, age);
		}

		public static bool IsAge(this DateTime date, DateTime refDate, int age)
		{
			return age > 0 && date.Age(refDate) >= age;
		}

		#endregion

		#region Week

		//public static string WeekDay(this DateTime date) {
		//   return WeekDay(date.DayOfWeek);
		//}

		//public static string WeekDayShort(this DateTime date) {
		//   return WeekDay(date.DayOfWeek).Substring(0, 2);
		//}

		//public static string WeekDayShort(this DayOfWeek dayOfWeek) {
		//   return WeekDay(dayOfWeek).Substring(0, 2);
		//}

		//public static string WeekDay(this DayOfWeek dayOfWeek) {
		//   switch (dayOfWeek) {
		//      case DayOfWeek.Sunday:
		//         return Weekday.Zondag.GetDescription();

		//      case DayOfWeek.Monday:
		//         return Weekday.Maandag.GetDescription();

		//      case DayOfWeek.Tuesday:
		//         return Weekday.Dinsdag.GetDescription();

		//      case DayOfWeek.Wednesday:
		//         return Weekday.Woensdag.GetDescription();

		//      case DayOfWeek.Thursday:
		//         return Weekday.Donderdag.GetDescription();

		//      case DayOfWeek.Friday:
		//         return Weekday.Vrijdag.GetDescription();

		//      case DayOfWeek.Saturday:
		//         return Weekday.Zaterdag.GetDescription();

		//      default:
		//         throw new ArgumentOutOfRangeException();
		//   }
		//}

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
			return date.HasValue ? IsWeekendDay(date.Value) : false;
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
			return DateTime.IsLeapYear(date.Year) ? 366 : 365;
		}

		public static int DaysInYear(int year)
		{
			return DateTime.IsLeapYear(year) ? 366 : 365;
		}

		public static int? DaysInYear(this DateTime? date)
		{
			return date.HasValue ? (DateTime.IsLeapYear(date.Value.Year) ? 366 : 365) : default(int?);
		}

		#endregion

		#region LeapYears

		/// <summary>
		/// Geeft het aantal schrikkeljaren terug voor een opgegeven periode.
		/// </summary>
		public static int LeapYears(this DateTime startDate, DateTime endDate)
		{
			if (endDate.Year - startDate.Year == 1)
			{
				// Einddatum is een schrikkeljaar. Eindatum kleiner dan schrikkeldag.
				// Looptijd minder dan 1 jaar.
				DateTime? leapDay = endDate.LeapDay();
				if (endDate.IsLeapYear() && leapDay.HasValue && (endDate < leapDay) && startDate.AddYears(1) >= endDate)
					return 0;

				// Startdaum is een schrikkeljaar.
				// Looptijd minder dan 1 jaar;
				if (startDate.IsLeapYear() && startDate.AddYears(1) >= endDate && startDate <= leapDay)
					return 0;

				leapDay = startDate.LeapDay();
				if (startDate.IsLeapYear() && leapDay.HasValue && startDate > leapDay)
					return 0;
			}

			int result = 0;

			for (int indexer = startDate.Year; indexer <= endDate.Year; indexer++)
				if (DateTime.IsLeapYear(indexer))
					result++;

			return result;
		}

		public static int LeapYears(this DateTime? startDate, DateTime endDate)
		{
			if (!startDate.HasValue)
				return 0;
			return LeapYears(startDate.Value, endDate);
		}

		public static int LeapYears(this DateTime startDate, DateTime? endDate)
		{
			if (!endDate.HasValue)
				return 0;
			return LeapYears(startDate, endDate.Value);
		}

		public static int LeapYears(this DateTime? startDate, DateTime? endDate)
		{
			if (!startDate.HasValue || !endDate.HasValue)
				return 0;
			return LeapYears(startDate.Value, endDate.Value);
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
		/// Determines if the date is a leap year.
		/// </summary>
		public static bool IsLeapYear(this DateTime date)
		{
			return DateTime.IsLeapYear(date.Year);
		}

		public static bool IsLeapYear(this DateTime? date)
		{
			return date.HasValue && DateTime.IsLeapYear(date.Value.Year);
		}

		public static bool IsLeapYear(int year)
		{
			return DateTime.IsLeapYear(year);
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
			return date.HasValue ? IsLeapDay(date.Value) : false;
		}
		#endregion

		#region Is In Range

		/// <summary>
		/// Opgegeven datum valt in de periode startdatum en einddatum
		/// 
		/// </summary>
		/// <param name="date">de datum</param>
		/// <param name="lowerBound">startdatum</param>
		/// <param name="upperBound">einddatum</param>
		/// <returns></returns>
		public static bool IsInDateRange(this DateTime? date, DateTime? lowerBound, DateTime? upperBound)
		{
			//if (NullableHelper.AnyIsNull(date, lowerBound, upperBound))
			//    return false;

			//// Als de range binnen dezelfde dag ligt op 1 stellen.
			//if (lowerBound.HasValue && upperBound.HasValue && lowerBound.Equals(upperBound))
			//    upperBound = upperBound.Value.AddDays(1);

			//return date.HasValue && date >= lowerBound && date < upperBound;
			return IsInDateRange(date, lowerBound, RangeBoundaryType.Inclusive, upperBound, RangeBoundaryType.Exclusive);
		}

		/// <summary>
		/// Opgegeven datum valt in de periode startdatum en einddatum
		/// 
		/// </summary>
		/// <param name="date">de datum</param>
		/// <param name="lowerBound">startdatum</param>
		/// <param name="lowerBoundType"></param>
		/// <param name="upperBound">einddatum</param>
		/// <param name="upperBoundType"></param>
		/// <returns></returns>
		public static bool IsInDateRange(this DateTime? date, DateTime? lowerBound, RangeBoundaryType lowerBoundType,
			DateTime? upperBound, RangeBoundaryType upperBoundType)
		{
			if (NullableHelper.AnyIsNull(date, lowerBound, upperBound))
				return false;

			// Als de range binnen dezelfde dag ligt op 1 stellen.
			if (lowerBound.HasValue && upperBound.HasValue && lowerBound.Equals(upperBound))
				upperBound = upperBound.Value.AddDays(1);

			return ((lowerBoundType == RangeBoundaryType.Exclusive && date > lowerBound) ||
						(lowerBoundType == RangeBoundaryType.Inclusive && date >= lowerBound)) &&
						((upperBoundType == RangeBoundaryType.Exclusive && date < upperBound) ||
						(upperBoundType == RangeBoundaryType.Inclusive && date <= upperBound));
		}

		public static bool IsInDateRange(DateTime date, DateTime secondDate, int? startDays, int? endDays = null)
		{
			DateTime beginOfWeek = GetBeginOfWeek();

			bool result = false;
			if (startDays.HasValue)
			{
				result = date.Date >= beginOfWeek.AddDays(startDays.Value).Date &&
				secondDate.Date >= beginOfWeek.AddDays(startDays.Value).Date;
			}

			if (endDays.HasValue && result)
			{
				result = date.Date < beginOfWeek.AddDays(endDays.Value).Date && secondDate.Date < beginOfWeek.AddDays(endDays.Value).Date;
			}

			return result;
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
		/// Determines whether the specified datetime is a workhour.
		/// A workhour is mo-fr 8:00 to 17:00 Localtime
		/// </summary>
		/// <param name="date">The localtime to calculate with.</param>
		/// <returns></returns>
		public static bool IsWorkHour(this DateTime date)
		{
			return !date.IsWeekendDay() && date.Hour >= 8 && date.Hour < 17;
		}

		public static bool IsWorkHour(this DateTime? date)
		{
			return IsWorkHour(date ?? DateTime.Now);
		}

		public static DateTime FloorToHour(this DateTime date)
		{
			return date.AddTicks(-1 * (date.Ticks % TimeSpan.TicksPerHour));
		}

		public static DateTime FloorToMinute(this DateTime date)
		{
			return date.AddTicks(-1 * (date.Ticks % TimeSpan.TicksPerMinute));
		}

		public static DateTime TrimTime(this DateTime date)
		{
			return date.AddTicks(-1 * (date.Ticks % TimeSpan.TicksPerDay));
		}

		public static DateTime FloorToSecond(this DateTime date)
		{
			return date.AddTicks(-1 * (date.Ticks % TimeSpan.TicksPerSecond));
		}

		public static DateTime RoundUpToMinutes(this DateTime date, int minutes)
		{
			TimeSpan d = TimeSpan.FromMinutes(minutes);
			long modTicks = date.Ticks % d.Ticks;
			long delta = modTicks != 0 ? d.Ticks - modTicks : 0;
			return new DateTime(date.Ticks + delta, date.Kind);
		}

		public static DateTime RoundDownToMinutes(this DateTime date, int minutes)
		{
			TimeSpan d = TimeSpan.FromMinutes(minutes);
			long delta = date.Ticks % d.Ticks;
			return new DateTime(date.Ticks - delta, date.Kind);
		}

		public static DateTime RoundToMinutes(this DateTime date, int minutes)
		{
			TimeSpan d = TimeSpan.FromMinutes(minutes);
			long delta = date.Ticks % d.Ticks;
			bool roundUp = delta > d.Ticks / 2;
			long offset = roundUp ? d.Ticks : 0;

			return new DateTime(date.Ticks + offset - delta, date.Kind);
		}

		public static DateTime SetTime(this DateTime date, int hour, int minutes = 0, int seconds = 0, int milliseconds = 0)
		{
			if (!hour.IsInRange(0, 24) || !minutes.IsInRange(0, 59) || !seconds.IsInRange(0, 59) || !milliseconds.IsInRange(0, 999))
				throw new ArgumentOutOfRangeException("Invalid time definition");

			return new DateTime(date.Year, date.Month, date.Day, hour, minutes, seconds, milliseconds, date.Kind);
		}



		/// <summary>
		/// Gives the last week number of the specified date.
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
		/// Gives the first date of the month for the specified date.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static DateTime FirstDayOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		/// <summary>
		/// Gives the last date of the month for the specified date.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static DateTime LastDayOfMonth(this DateTime date)
		{
			return date.AddMonths(1).FirstDayOfMonth().AddDays(-1);
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
		/// <param name="lowerbound">the from date</param>
		/// <param name="upperbound">the to date</param>
		/// <returns></returns>
		public static int NightsFromTo(this DateTime lowerbound, DateTime upperbound)
		{
			return (int)upperbound.Date.Subtract(lowerbound.Date).TotalDays - 1;
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
		/// <param name="lowerbound">The from date.</param>
		/// <param name="upperbound">The till date.</param>
		/// <returns></returns>
		public static int DaysFromTill(this DateTime lowerbound, DateTime upperbound)
		{
			return (int)upperbound.AddDays(1).Date.Subtract(lowerbound.Date).TotalDays;
		}

		/// <summary>
		/// Gives the number of days from fromdate to ToDate.
		/// </summary>
		/// <param name="lowerbound">the from date</param>
		/// <param name="upperbound">the to date</param>
		/// <returns></returns>
		public static int DaysFromTo(this DateTime lowerbound, DateTime upperbound)
		{
			return (int)upperbound.Date.Subtract(lowerbound.Date).TotalDays;
		}

	}
}