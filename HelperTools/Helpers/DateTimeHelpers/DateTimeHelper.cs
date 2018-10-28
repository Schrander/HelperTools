using System;
using System.Text.RegularExpressions;

namespace HelperTools.Helpers.DateTimeHelpers
{
    public static partial class DateTimeExt
    {

        public static int GetMonthNumber(string monthName)
        {
            if (string.IsNullOrWhiteSpace(monthName))
                throw new ArgumentNullException();

            Regex r = new Regex(@"(?<month>jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)", RegexOptions.IgnoreCase);

            return GetMonth(r.Replace(monthName, "${month}"));
        }

        public static void GetDateTime(string date, out DateTime pubDate)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                pubDate = DateTime.Today;
                return;
            }

            if (DateTime.TryParse(date, out pubDate))
                return;

            date = date.ToLower();

            const string pattern = @"(?<day>[1-9]|[12][0-9]|3[01]) (?<month>jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec) (?<year>(?:19|20)[0-9]{2})";

            MatchCollection matches = Regex.Matches(date, pattern, RegexOptions.IgnoreCase);
            Match match = matches[0];

            if (string.IsNullOrEmpty(match.Value))
            {
                pubDate = DateTime.Today;
                return;
            }

            int month = GetMonth(Regex.Replace(match.Value, pattern, $"${nameof(month)}"));
            int day = Convert.ToInt32(Regex.Replace(match.Value, pattern, $"${nameof(day)}"));
            int year = Convert.ToInt32(Regex.Replace(match.Value, pattern, $"${nameof(year)}"));

            pubDate = !NullableHelper.AnyIsNull(day, month, year) ? new DateTime(year, month, day) : DateTime.Today;
        }



        public static bool IsInEmbargo(DateTime embargoDate)
        {
            return DateTime.Now < embargoDate;
        }


        public static int Weekday(DateTime date, DayOfWeek startOfWeek)
        {
            return -((int)date.DayOfWeek - (int)(startOfWeek + 7) % 7);
        }


        public static DateTime FirstSaturdayOfMonth(int year, int month)
        {
            var input = new DateTime(year, month, 1);
            return input.AddDays(Weekday(input, DayOfWeek.Saturday));
        }

        public static DateTime GetDayByWeekOfDay(this DateTime date, DayOfWeek weekDay)
        {
            return date.AddDays(Weekday(date, weekDay));
        }
    }
}
