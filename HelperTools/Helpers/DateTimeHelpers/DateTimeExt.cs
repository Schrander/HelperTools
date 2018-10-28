using System;

namespace HelperTools.Helpers.DateTimeHelpers
{
	public static partial class DateTimeExt
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

		public static DateTime GetDateByDayOfYear(int year, int day)
		{
			return new DateTime(year, 1, 1).AddDays(day - 1);
		}


		public static DateTime SetTime(this DateTime date, TimeSpan time)
		{
			return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
		}

		public static DateTime AddWeeks(this DateTime date, int weeks)
		{
			return date.AddDays(weeks * 7);
		}


		public static DateTime? GetDateTime(string date)
		{
			if (string.IsNullOrWhiteSpace(date))
				return default(DateTime?);

			if (NumberHelper.IsInteger(date) && date.Length == 8)
				date = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);

			DateTime oDate = DateTime.MinValue;
			bool isSuccess = DateTime.TryParse(date, out oDate);
			return isSuccess ? oDate : default(DateTime?);
		}

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

	}
}