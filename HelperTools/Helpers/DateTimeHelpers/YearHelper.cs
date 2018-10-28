using System;

namespace HelperTools.Helpers.DateTimeHelpers
{
	public static partial class DateTimeExt
	{

		#region Years

		public static int Years(this DateTime startDate, DateTime endDate)
		{
			int diff = endDate.Year - startDate.Year;

			if (diff == 1)
			{
				DateTime? leapDay = endDate.LeapDay();
				if (diff == 1 && endDate.IsLeapYear() && leapDay.HasValue && endDate >= leapDay && startDate.AddYears(1) >= endDate)
					return 1;

				leapDay = startDate.LeapDay();
				if (startDate.IsLeapYear() && leapDay.HasValue && startDate <= leapDay.Value && startDate.AddYears(1) >= endDate)
					return 1;
			}

			return diff + 1;
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
			return startDate.HasValue ? LeapYears(startDate.Value, endDate) : 0;
		}

		public static int LeapYears(this DateTime startDate, DateTime? endDate)
		{
			return endDate.HasValue ? LeapYears(startDate, endDate.Value) : 0;
		}

		public static int LeapYears(this DateTime? startDate, DateTime? endDate)
		{
			if (!startDate.HasValue || !endDate.HasValue)
				return 0;
			return LeapYears(startDate.Value, endDate.Value);
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

		#endregion

	}
}