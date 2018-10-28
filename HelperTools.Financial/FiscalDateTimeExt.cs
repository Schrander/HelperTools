using System;
using HelperTools.Helpers.DateTimeHelpers;

namespace HelperTools.Financial
{
	public static class FiscalDateTimeExt
	{

		#region FiscalAge
		// Door 1 seconde erbij te smokkelen zal deze persoon nog geen 18 of 35/40 jaar zijn op de schenkingsdatum
		// Voor diegene die 18 wordt op de schenkingsdatum geldt de regeling niet.
		// Voor diegene die 35 (of 40) wordt op de schenkingsdatum geldt de regeling wel.
		// Fiscaal gezien wordt een datum gesteld op 0:00. Een persoon zal nimmer exact op dat tijdstip geboren zijn.

		public static int? FiscalAge(this DateTime? date)
		{
			return date.HasValue ? FiscalAge(date.Value) : default(int?);
		}

		public static int? FiscalAge(this DateTime date, DateTime? refDate)
		{
			return refDate.HasValue ? FiscalAge(date, refDate.Value) : default(int?);
		}

		public static int? FiscalAge(this DateTime? date, DateTime refDate)
		{
			return date.HasValue ? FiscalAge(date.Value, refDate) : default(int?);
		}

		public static int? FiscalAge(this DateTime? date, DateTime? refDate)
		{
			return (date.HasValue && refDate.HasValue) ? FiscalAge(date.Value, refDate.Value) : default(int?);
		}

		public static int FiscalAge(this DateTime date)
		{
			return FiscalAge(date, DateTime.Now);
		}

		public static int FiscalAge(this DateTime date, DateTime refDate)
		{
			return date.AddSeconds(1).Age(refDate);
		}

		#endregion

		#region IsFiscalAge

		public static bool IsFiscalAge(this DateTime? date, int age)
		{
			return date.HasValue && IsFiscalAge(date.Value, DateTime.Now, age);
		}

		public static bool IsFiscalAge(this DateTime? date, DateTime refDate, int age)
		{
			return date.HasValue && IsFiscalAge(date.Value, refDate, age);
		}

		public static bool IsFiscalAge(this DateTime date, DateTime? refDate, int age)
		{
			return refDate.HasValue && IsFiscalAge(date, refDate.Value, age);
		}

		public static bool IsFiscalAge(this DateTime? date, DateTime? refDate, int age)
		{
			return date.HasValue && refDate.HasValue && IsFiscalAge(date, refDate.Value, age);
		}

		public static bool IsFiscalAge(this DateTime date, int age)
		{
			return IsFiscalAge(date, DateTime.Now, age);
		}

		public static bool IsFiscalAge(this DateTime date, DateTime refDate, int age)
		{
			return date.AddSeconds(1).IsAge(refDate, age);
		}

		#endregion

		#region YearFraction

		/// <summary>
		/// Excel's YearFraction.
		/// </summary>
		/// <param name="startDate">start date.</param>
		/// <param name="endDate">end date.</param>
		/// <returns></returns>
		public static double? YearFraction(this DateTime startDate, DateTime endDate)
		{
			double years = startDate.Years(endDate);
			int leapYears = startDate.LeapYears(endDate);

			if (startDate > endDate)
				return null;

			int days = (endDate - startDate).Days;
			return days / (365 + (leapYears / years));
		}

		/// <summary>
		/// Excel's YearFraction.
		/// </summary>
		/// <param name="startDate">start date.</param>
		/// <param name="endDate">end date.</param>
		/// <returns></returns>
		public static double? YearFraction(this DateTime startDate, DateTime? endDate)
		{
			return endDate.HasValue ? YearFraction(startDate, endDate.Value) : default(double?);
		}

		/// <summary>
		/// Excel's YearFraction.
		/// </summary>
		/// <param name="startDate">start date.</param>
		/// <param name="endDate">end date.</param>
		/// <returns></returns>
		public static double? YearFraction(this DateTime? startDate, DateTime endDate)
		{
			return startDate.HasValue ? YearFraction(startDate.Value, endDate) : default(double?);
		}

		/// <summary>
		/// Excel's YearFraction.
		/// </summary>
		/// <param name="startDate">start date.</param>
		/// <param name="endDate">end date.</param>
		/// <returns></returns>
		public static double? YearFraction(this DateTime? startDate, DateTime? endDate)
		{
			if (!startDate.HasValue || !endDate.HasValue)
				return null;

			return YearFraction(startDate.Value, endDate.Value);
		}

		#endregion
	}
}