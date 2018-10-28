using System;

namespace HelperTools.Helpers.DateTimeHelpers
{
    public static partial class DateTimeExt
	{

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

	    public static string GetMonth(int month)
	    {
	        switch (month)
	        {
	            case 1:  return "Januari";
	            case 2:  return "Februari";
	            case 3:  return "Maart";
	            case 4:  return "April";
	            case 5:  return "May";
	            case 6:  return "Juni";
	            case 7:  return "Juli";
	            case 8:  return "Augustus";
	            case 9:  return "September";
	            case 10: return "Oktober";
	            case 11: return "November";
	            case 12: return "December";
	            default: return "Januari";
            }
	    }

        public static int GetMonth(string month)
		{
			if (string.IsNullOrWhiteSpace(month))
				return 1;

			switch (month.ToLower())
			{
				case "jan": return 1;
				case "feb": return 2;
				case "mar": return 3;
				case "apr": return 4;
				case "may": return 5;
				case "jun": return 6;
				case "jul": return 7;
				case "aug": return 8;
				case "sep": return 9;
				case "oct": return 10;
				case "nov": return 11;
				case "dec": return 12;
				default: return 1;
			}
		}
	}
}