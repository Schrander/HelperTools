using System;
using HelperTools.Helpers;
using HelperTools.Helpers.DateTimeHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class DateHelperTest
	{


		[TestMethod]
		public void IsWorkhourTest()
		{
			Assert.IsFalse(new DateTime(2014, 11, 7, 7, 59, 59, DateTimeKind.Local).IsWorkHour()); // fr
			Assert.IsTrue(new DateTime(2014, 11, 7, 8, 0, 0, DateTimeKind.Local).IsWorkHour()); // fr
			Assert.IsTrue(new DateTime(2014, 11, 7, 8, 0, 1, DateTimeKind.Local).IsWorkHour()); // fr
			Assert.IsTrue(new DateTime(2014, 11, 7, 16, 59, 59, DateTimeKind.Local).IsWorkHour()); // fr
			Assert.IsFalse(new DateTime(2014, 11, 7, 17, 0, 0, DateTimeKind.Local).IsWorkHour()); // fr
			Assert.IsFalse(new DateTime(2014, 11, 7, 17, 0, 1, DateTimeKind.Local).IsWorkHour()); // fr
			Assert.IsFalse(new DateTime(2014, 11, 7, 17, 1, 0, DateTimeKind.Local).IsWorkHour()); // fr
			Assert.IsFalse(new DateTime(2014, 11, 7, 18, 0, 0, DateTimeKind.Local).IsWorkHour()); // fr

			Assert.IsFalse(new DateTime(2014, 11, 8, 12, 0, 0, DateTimeKind.Local).IsWorkHour()); // sa
			Assert.IsFalse(new DateTime(2014, 11, 9, 12, 0, 0, DateTimeKind.Local).IsWorkHour()); // su
			Assert.IsFalse(new DateTime(2014, 11, 10, 6, 0, 0, DateTimeKind.Local).IsWorkHour()); // mo
			Assert.IsTrue(new DateTime(2014, 11, 10, 8, 0, 0, DateTimeKind.Local).IsWorkHour()); // mo
			Assert.IsTrue(new DateTime(2014, 11, 10, 12, 0, 0, DateTimeKind.Local).IsWorkHour()); // mo
			Assert.IsFalse(new DateTime(2014, 11, 10, 17, 1, 0, DateTimeKind.Local).IsWorkHour()); // mo
		}


		[TestMethod]
		public void WeeknumberTest()
		{
			Assert.AreEqual(53, new DateTime(2016, 1, 1).WeekNumber());
			Assert.AreEqual(1, new DateTime(2016, 1, 5).WeekNumber());
			Assert.AreEqual(29, new DateTime(2016, 7, 18).WeekNumber());
		}

		[TestMethod]
		public void FirstWeekNumberTest()
		{
			Assert.AreEqual(53, new DateTime(2016, 1, 1).FirstWeekNumberOfYear());
			Assert.AreEqual(1, new DateTime(2015, 1, 1).FirstWeekNumberOfYear());
		}

		[TestMethod]
		public void LastWeekNumberTest()
		{
			Assert.AreEqual(52, new DateTime(2016, 1, 1).LastWeekNumberOfYear());
			Assert.AreEqual(53, new DateTime(2015, 1, 1).LastWeekNumberOfYear());
		}

		[TestMethod]
		public void NumberOfWeeksInYearTest()
		{
			Assert.AreEqual(53, new DateTime(2016, 1, 1).NumberOfWeeksOfYear());
			Assert.AreEqual(53, new DateTime(2015, 1, 1).NumberOfWeeksOfYear());
			Assert.AreEqual(54, new DateTime(2012, 1, 1).NumberOfWeeksOfYear());
		}

		[TestMethod]
		public void LastDayOfMonthTest()
		{
			Assert.AreEqual(29, new DateTime(2016, 2, 16).LastDayOfMonth().Day);
			Assert.AreEqual(31, new DateTime(2015, 12, 14).LastDayOfMonth().Day);
			Assert.AreEqual(28, new DateTime(2015, 2, 14).LastDayOfMonth().Day);
		}

		[TestMethod]
		public void FirstDayOfMonthTest()
		{
			Assert.AreEqual(1, new DateTime(2016, 1, 16).FirstDayOfMonth().Day);
			Assert.AreEqual(1, new DateTime(2015, 2, 16).FirstDayOfMonth().Day);
			Assert.AreEqual(1, new DateTime(2016, 5, 16).FirstDayOfMonth().Day);
		}

		[TestMethod]
		public void GetDateTimeTest()
		{
			Assert.AreEqual(new DateTime(2016, 4, 1), DateTimeExt.GetDateTime("20160401"));
		}

		[TestMethod]
		public void NumberOfWeeks()
		{
			DateTime d = new DateTime(2015, 1, 15);
			Assert.AreEqual(9, d.NumberOfWeeks(d.AddMonths(2)));
		}

		[TestMethod]
		public void NightsArrivalDepartureTest()
		{
			DateTime arrival1 = new DateTime(2016, 2, 26, 23, 48, 0);
			DateTime departure1 = new DateTime(2016, 3, 2, 16, 48, 0);

			DateTime arrival2 = new DateTime(2016, 4, 12, 23, 48, 0);
			DateTime departure2 = new DateTime(2016, 4, 14, 16, 48, 0);

			Assert.AreEqual(5, arrival1.NightsArrivalDeparture(departure1));
			Assert.AreEqual(2, arrival2.NightsArrivalDeparture(departure2));
		}

		[TestMethod]
		public void NightsFromTillTest()
		{
			DateTime from1 = new DateTime(2016, 2, 26, 23, 48, 0);
			DateTime till1 = new DateTime(2016, 3, 2, 16, 48, 0);

			DateTime from2 = new DateTime(2016, 4, 12, 23, 48, 0);
			DateTime till2 = new DateTime(2016, 4, 14, 16, 48, 0);

			Assert.AreEqual(5, from1.NightsFromTill(till1));
			Assert.AreEqual(2, from2.NightsFromTill(till2));
		}

		[TestMethod]
		public void NightsFromToTest()
		{
			DateTime from1 = new DateTime(2016, 2, 26, 23, 48, 0);
			DateTime to1 = new DateTime(2016, 3, 2, 16, 48, 0);

			DateTime from2 = new DateTime(2016, 4, 12, 23, 48, 0);
			DateTime to2 = new DateTime(2016, 4, 14, 16, 48, 0);

			Assert.AreEqual(4, from1.NightsFromTo(to1));
			Assert.AreEqual(1, from2.NightsFromTo(to2));
		}

		[TestMethod]
		public void DaysArrivalDepartureTest()
		{
			DateTime arrival1 = new DateTime(2016, 2, 26, 23, 48, 0);
			DateTime departure1 = new DateTime(2016, 3, 2, 16, 48, 0);

			DateTime arrival2 = new DateTime(2016, 4, 12, 23, 48, 0);
			DateTime departure2 = new DateTime(2016, 4, 14, 16, 48, 0);

			Assert.AreEqual(6, arrival1.DaysArrivalDeparture(departure1));
			Assert.AreEqual(3, arrival2.DaysArrivalDeparture(departure2));
		}

		[TestMethod]
		public void DaysFromTillTest()
		{
			DateTime from1 = new DateTime(2016, 2, 26, 23, 48, 0);
			DateTime till1 = new DateTime(2016, 3, 2, 16, 48, 0);

			DateTime from2 = new DateTime(2016, 4, 12, 23, 48, 0);
			DateTime till2 = new DateTime(2016, 4, 14, 16, 48, 0);

			Assert.AreEqual(6, from1.DaysFromTill(till1));
			Assert.AreEqual(3, from2.DaysFromTill(till2));
		}

		[TestMethod]
		public void DaysFromToTest()
		{
			DateTime from1 = new DateTime(2016, 2, 26, 23, 48, 0);
			DateTime to1 = new DateTime(2016, 3, 2, 16, 48, 0);

			DateTime from2 = new DateTime(2016, 4, 12, 23, 48, 0);
			DateTime to2 = new DateTime(2016, 4, 14, 16, 48, 0);

			Assert.AreEqual(5, from1.DaysFromTo(to1));
			Assert.AreEqual(2, from2.DaysFromTo(to2));
		}

		[TestMethod]
		public void IsInDateRangeTest()
		{
			DateTime from18 = new DateTime(2016, 4, 18);
			DateTime from18_0900 = new DateTime(2016, 4, 18, 9, 0, 0);

			DateTime to18 = new DateTime(2016, 4, 18);
			DateTime to20 = new DateTime(2016, 4, 20);
			DateTime to20_1500 = new DateTime(2016, 4, 20, 15, 0, 0);

			DateTime d17 = new DateTime(2016, 4, 17);
			DateTime d18 = new DateTime(2016, 4, 18);
			DateTime d18_0859 = new DateTime(2016, 4, 18, 8, 59, 0);
			DateTime d18_0900 = new DateTime(2016, 4, 18, 9, 0, 0);
			DateTime d18_0901 = new DateTime(2016, 4, 18, 9, 1, 0);
			DateTime d19 = new DateTime(2016, 4, 19);
			DateTime d20 = new DateTime(2016, 4, 20);
			DateTime d20_1400 = new DateTime(2016, 4, 20, 14, 0, 0);
			DateTime d20_1459 = new DateTime(2016, 4, 20, 14, 59, 0);
			DateTime d20_1500 = new DateTime(2016, 4, 20, 15, 0, 0);
			DateTime d20_1515 = new DateTime(2016, 4, 20, 15, 15, 0);
			DateTime d21 = new DateTime(2016, 4, 21);

			Assert.IsFalse(d17.IsInDateRange(from18, to18));
			Assert.IsTrue(d18.IsInDateRange(from18, to18));
			Assert.IsFalse(d19.IsInDateRange(from18, to18));

			Assert.IsFalse(d17.IsInDateRange(from18, to20));
			Assert.IsTrue(d18.IsInDateRange(from18, to20));
			Assert.IsTrue(d19.IsInDateRange(from18, to20));
			Assert.IsTrue(d20.IsInDateRange(from18, to20));
			Assert.IsTrue(d20_1400.IsInDateRange(from18, to20));
			Assert.IsFalse(d21.IsInDateRange(from18, to20));

			Assert.IsFalse(d17.IsInDateRange(from18, to20_1500));
			Assert.IsTrue(d18.IsInDateRange(from18, to20_1500));
			Assert.IsTrue(d19.IsInDateRange(from18, to20_1500));
			Assert.IsTrue(d20.IsInDateRange(from18, to20_1500));
			Assert.IsTrue(d20_1400.IsInDateRange(from18, to20_1500));
			Assert.IsTrue(d20_1459.IsInDateRange(from18, to20_1500));
			Assert.IsTrue(d20_1500.IsInDateRange(from18, to20_1500));
			Assert.IsFalse(d20_1515.IsInDateRange(from18, to20_1500));
			Assert.IsFalse(d21.IsInDateRange(from18, to20_1500));

			Assert.IsFalse(d17.IsInDateRange(from18_0900, to20_1500));
			Assert.IsFalse(d18.IsInDateRange(from18_0900, to20_1500));
			Assert.IsFalse(d18_0859.IsInDateRange(from18_0900, to20_1500));
			Assert.IsTrue(d18_0900.IsInDateRange(from18_0900, to20_1500));
			Assert.IsTrue(d18_0901.IsInDateRange(from18_0900, to20_1500));
			Assert.IsTrue(d19.IsInDateRange(from18_0900, to20_1500));
			Assert.IsTrue(d20.IsInDateRange(from18_0900, to20_1500));
			Assert.IsTrue(d20_1400.IsInDateRange(from18_0900, to20_1500));
			Assert.IsTrue(d20_1459.IsInDateRange(from18_0900, to20_1500));
			Assert.IsTrue(d20_1500.IsInDateRange(from18_0900, to20_1500));
			Assert.IsFalse(d20_1515.IsInDateRange(from18_0900, to20_1500));
			Assert.IsFalse(d21.IsInDateRange(from18_0900, to20_1500));
		}
		
		[TestMethod]
		public void DateTimeTrimTest()
		{
			DateTime date = new DateTime(2016, 8, 16, 14, 40, 15, 345);

			Assert.AreEqual(new DateTime(2016, 8, 16, 0, 0, 0), date.TrimTime());
			Assert.AreEqual(new DateTime(2016, 8, 16, 14, 0, 0), date.FloorToHour());
			Assert.AreEqual(new DateTime(2016, 8, 16, 14, 40, 0), date.FloorToMinute());
		}

		[TestMethod]
		public void DateTimeRoundTest()
		{
			var dt1 = new DateTime(2016, 8, 16, 15, 57, 15, 345);
			var dt2 = new DateTime(2016, 8, 16, 16, 02, 15, 345);
			var dt7 = new DateTime(2016, 8, 16, 16, 07, 15, 345);
			var dt3 = new DateTime(2016, 8, 16, 16, 14, 15, 345);
			var dt8 = new DateTime(2016, 8, 16, 16, 11, 15, 345);
			var dt4 = new DateTime(2016, 8, 16, 16, 17, 15, 345);
			var dt5 = new DateTime(2016, 8, 16, 16, 26, 15, 345);
			var dt6 = new DateTime(2016, 8, 16, 16, 32, 15, 345);

			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 0, 0), dt1.RoundToMinutes(15));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 0, 0), dt2.RoundToMinutes(15));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 15, 0), dt3.RoundToMinutes(15));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 15, 0), dt4.RoundToMinutes(15));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 30, 0), dt5.RoundToMinutes(15));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 30, 0), dt6.RoundToMinutes(15));

			Assert.AreEqual(new DateTime(2016, 8, 16, 15, 55, 0), dt1.RoundToMinutes(5));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 0, 0), dt2.RoundToMinutes(5));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 5, 0), dt7.RoundToMinutes(5));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 10, 0), dt8.RoundToMinutes(5));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 15, 0), dt3.RoundToMinutes(5));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 15, 0), dt4.RoundToMinutes(5));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 25, 0), dt5.RoundToMinutes(5));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 30, 0), dt6.RoundToMinutes(5));

			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 0, 0), dt1.RoundToMinutes(10));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 0, 0), dt2.RoundToMinutes(10));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 10, 0), dt3.RoundToMinutes(10));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 20, 0), dt4.RoundToMinutes(10));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 30, 0), dt5.RoundToMinutes(10));
			Assert.AreEqual(new DateTime(2016, 8, 16, 16, 30, 0), dt6.RoundToMinutes(10));
		}

		[TestMethod]
		public void DateTimeSetTime()
		{
			var date = new DateTime(2016, 8, 16, 15, 57, 15, 345);

			Assert.AreEqual(new DateTime(2016, 8, 16, 15, 0, 0, 0), date.SetTime(15));
			Assert.AreEqual(new DateTime(2016, 8, 16, 15, 10, 0, 0), date.SetTime(15, 10));
			Assert.AreEqual(new DateTime(2016, 8, 16, 15, 10, 4, 0), date.SetTime(15, 10, 4));
			Assert.AreEqual(new DateTime(2016, 8, 16, 15, 10, 4, 244), date.SetTime(15, 10, 4, 244));
		}
	}
}