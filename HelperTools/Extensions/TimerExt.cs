
namespace HelperTools
{
	public static class TimerExt
	{
		public static long Duration(int value, TimeUnit unit = TimeUnit.Milliseconds)
		{
			return value * UnitDuration(unit);
		}

		////http://stackoverflow.com/questions/385966/strange-c-sharp-overflow-error
		//private static long UnitDuration(TimeUnit unit)
		//{
		//	switch (unit)
		//	{
		//		case TimeUnit.Days: return 24 * 60 * 60 * 1000L;
		//		case TimeUnit.Years: return 365 * 24 * 60 * 60 * 1000L;
		//		case TimeUnit.Months: return (365 * 24 * 60 * 60 * 1000L) / 12;
		//		case TimeUnit.Hours: return 60 * 60 * 1000L;
		//		case TimeUnit.Milliseconds: return 1;
		//		case TimeUnit.Seconds: return 1000L;
		//		case TimeUnit.Minutes: return 60 * 1000L;
		//		case TimeUnit.Weeks: return 7 * 24 * 60 * 60 * 1000L;

		//		default: return 1000L;

		//	}
		//}


		//http://stackoverflow.com/questions/385966/strange-c-sharp-overflow-error
		private static long UnitDuration(TimeUnit unit)
		{
			long milliseconds = 1L;
			long seconds = 1000 * milliseconds;
			long minutes = 60 * seconds;
			long hours = 60 * minutes;
			long days = 24 * hours;
			long year = 365 * days;


			switch (unit)
			{
				case TimeUnit.Milliseconds: return 1L;
				case TimeUnit.Seconds: return seconds;
				case TimeUnit.Minutes: return minutes;
				case TimeUnit.Hours: return hours;
				case TimeUnit.Days: return days;
				case TimeUnit.Weeks: return 7 * days;
				case TimeUnit.Months: return year / 12;
				case TimeUnit.Years: return year;

				default: return seconds;
			}
		}


		public static long ToMinutes(this int value)
		{
			return Duration(value, TimeUnit.Minutes);
		}

		public static long ToSeconds(this int value)
		{
			return Duration(value, TimeUnit.Seconds);
		}

		public static long ToHours(this int value)
		{
			return Duration(value, TimeUnit.Hours);
		}

		public static long ToDays(this int value)
		{
			return Duration(value, TimeUnit.Days);
		}

		public static long ToWeeks(this int value)
		{
			return Duration(value, TimeUnit.Weeks);
		}

		public static long ToMonths(this int value)
		{
			return Duration(value, TimeUnit.Months);
		}

		public static long ToYears(this int value)
		{
			return Duration(value, TimeUnit.Years);
		}
	}
}
