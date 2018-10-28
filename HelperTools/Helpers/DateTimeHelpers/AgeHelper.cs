using System;

namespace HelperTools.Helpers.DateTimeHelpers
{
	public static partial class DateTimeExt
	{

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
				throw new Exception("Invalid date", ex.InnerException);
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

		public static int? AgeInHours(this DateTime? date,DateTime refDate)
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


	}
}