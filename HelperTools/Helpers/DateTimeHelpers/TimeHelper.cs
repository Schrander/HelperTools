using System;

namespace HelperTools.Helpers.DateTimeHelpers
{
	public static partial class DateTimeExt
	{

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

		/// <summary>
		/// Determines whether the specified datetime is a workhour.
		/// A workhour is mo~fr 8:00 to 17:00 Localtime
		/// </summary>
		/// <param name="datetimeLocal">The localtime to calculae with.</param>
		/// <returns></returns>
		public static bool IsWorkhour(this DateTime? datetimeLocal)
		{
			var check = datetimeLocal ?? DateTime.Now;

			switch (check.DayOfWeek)
			{
				case DayOfWeek.Friday:
				case DayOfWeek.Monday:
				case DayOfWeek.Thursday:
				case DayOfWeek.Tuesday:
				case DayOfWeek.Wednesday:
					return check.Hour >= 8 && check.Hour < 17;
				case DayOfWeek.Saturday:
				case DayOfWeek.Sunday:
				default:
					return false;
			}
		}



	}
}