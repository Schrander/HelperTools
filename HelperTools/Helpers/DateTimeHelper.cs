using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HelperTools.Helpers;

namespace HelperTools
{
	public static class DateTimeHelper
	{

		public static void GetDateTime(string date, out DateTime pubDate)
		{
			if (string.IsNullOrEmpty(date))
			{
				pubDate = DateTime.Today;
				return;
			}

			if (DateTime.TryParse(date, out pubDate))
				return;

			date = date.ToLower();

			const string pattern = @"(?<day>([1-9]|[12][0-9]|3[01])) (?<month>(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)) (?<year>(20[0-9]{2}))";

			MatchCollection matches = Regex.Matches(date, pattern);
			Match match = matches[0];

			if (string.IsNullOrEmpty(match.Value))
			{
				pubDate = DateTime.Today;
				return;
			}

			int month = GetMonth(Regex.Replace(match.Value, pattern, "${month}"));
			int day = Convert.ToInt32(Regex.Replace(match.Value, pattern, "${day}"));
			int year = Convert.ToInt32(Regex.Replace(match.Value, pattern, "${year}"));

			pubDate = !NullableHelper.AnyIsNull(day, month, year) ? new DateTime(year, month, day) : DateTime.Today;
		}


		/// <summary>
		/// Determines whether the specified datetime is a workhour.
		/// A workhour is mo~fr 8:00 to 17:00 Localtime
		/// </summary>
		/// <param name="datetimeLocal">The localtime to calculae with.</param>
		/// <returns></returns>
		public static bool IsWorkhour(this DateTime? datetimeLocal )
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


		public static int GetMonth(string month)
		{
			if (string.IsNullOrEmpty(month))
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

		public static bool IsEmbargoRelease(DateTime embargoDate)
		{
			return DateTime.Now < embargoDate;
		}

		#region Consecutive

		public static bool IsConsecutive(params DateTime[] dates)
		{
			return GroupingConsecutiveDates(dates).Count == 1;
		}

		public static List<List<DateTime>> GroupingConsecutiveDates(params DateTime[] dates)
		{
			var dateList = dates.OrderBy(o => o).ToList();
			//this will hold the resulted groups
			var groups = new List<List<DateTime>>();
			// the group for the first element
			var group1 = new List<DateTime>() { dates[0] };
			groups.Add(group1);

			DateTime lastDate = dates[0];
			for (int i = 1; i < dateList.Count; i++)
			{
				DateTime currDate = dates[i];
				TimeSpan timeDiff = currDate - lastDate;
				//should we create a new group?
				bool isNewGroup = timeDiff.Days > 1;
				if (isNewGroup)
					groups.Add(new List<DateTime>());

				groups.Last().Add(currDate);
				lastDate = currDate;
			}

			return groups;
		}

		#endregion

		public static DateTime? GetDateTime(string date)
		{
			if (string.IsNullOrEmpty(date))
				return default(DateTime?);

			if (NumberHelper.IsInteger(date) && date.Length == 8)
				date = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);

			DateTime oDate;
			bool isSuccess = DateTime.TryParse(date, out oDate);
			return isSuccess ? oDate : default(DateTime?);
		}
	}
}
