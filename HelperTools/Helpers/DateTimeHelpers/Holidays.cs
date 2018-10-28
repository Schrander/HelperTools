using System;

namespace HelperTools.Helpers.DateTimeHelpers
{
    public static class Holidays
    {

        public static DateTime EasterSunday(int year)
        {
            int g = year % 19;
            int century = year / 100;
            int h = (century - (int)(century / 4) - (int)((8 * century + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));
            int day = i - ((year + (int)(year / 4) + i + 2 - century + (int)(century / 4)) % 7) + 28;
            int month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }

        public static DateTime EasterMonday(int year)
        {
            return EasterSunday(year).AddDays(1);
        }

        public static DateTime PentacostSunday(int year)
        {
            return EasterSunday(year).AddDays(49);
        }

        public static DateTime PentacostMonday(int year)
        {
            return PentacostSunday(year).AddDays(1);
        }

        public static DateTime AscensionDay(int year)
        {
            return PentacostSunday(year).AddDays(-10);
        }

        public static DateTime KingsDay(int year)
        {
            return new DateTime(year, 4, 27);
        }

        public static DateTime OldYearsDay(int year)
        {
            return new DateTime(year, 12, 31);
        }

        public static DateTime NewYearsDay(int year)
        {
            return new DateTime(year, 1, 1);
        }

        public static DateTime FirstChristmasEve(int year)
        {
            return new DateTime(year, 12, 24);
        }

        public static DateTime FirstChristmasDay(int year)
        {
            return new DateTime(year, 12, 25);
        }

        public static DateTime SecondChristmasDay(int year)
        {
            return new DateTime(year, 12, 26);
        }

        public static DateTime MothersDay(int year)
        {
            return new DateTime(year, 5, 1).GetDayByWeekOfDay(DayOfWeek.Sunday).AddDays(7);
        }



    }
}