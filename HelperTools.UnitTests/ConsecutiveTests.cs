using System;
using System.Collections.Generic;
using HelperTools.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class ConsecutiveTests
	{

		[TestMethod]
		public void ConsecutiveTest()
		{
			List<int> ints = new List<int>() {2001, 2002, 2003, 2005, 2007};
			List<long> longs = new List<long>() {3, 4, 5, 6, 7, 8};

			var groupedInts = ConsecutiveHelper.GroupingConsecutiveItems(ints);
			var groupedLongs = ConsecutiveHelper.GroupingConsecutiveItems(longs);
		}

		[TestMethod]
		public void IsConsecutiveTest()
		{
			List<DateTime> dates = new List<DateTime>();
			dates.Add(new DateTime(2016, 4, 30));
			dates.Add(new DateTime(2016, 5, 1));
			dates.Add(new DateTime(2016, 5, 2));
			dates.Add(new DateTime(2016, 5, 3));
			Assert.IsTrue(ConsecutiveHelper.IsConsecutive(dates.ToArray()));

			dates.Add(new DateTime(2016, 5, 6));
			Assert.IsFalse(ConsecutiveHelper.IsConsecutive(dates.ToArray()));

			dates = new List<DateTime>();
			dates.Add(new DateTime(2016, 12, 31));
			dates.Add(new DateTime(2017, 1, 1));
			Assert.IsTrue(ConsecutiveHelper.IsConsecutive(dates.ToArray()));

			dates = new List<DateTime>();
			dates.Add(new DateTime(2016, 12, 31, 23, 50, 0));
			dates.Add(new DateTime(2017, 1, 1, 0, 10, 0));
			Assert.IsTrue(ConsecutiveHelper.IsConsecutive(dates.ToArray()));

			dates = new List<DateTime>();
			dates.Add(new DateTime(2016, 12, 31, 23, 0, 0));
			dates.Add(new DateTime(2017, 1, 1, 23, 15, 0));
			Assert.IsTrue(ConsecutiveHelper.IsConsecutive(dates.ToArray()));

			Assert.IsTrue(ConsecutiveHelper.IsConsecutive(DateTime.Today));
		}

		[TestMethod]
		public void GroupConsecutiveDatesTest()
		{
			List<DateTime> dates = new List<DateTime>();
			dates.Add(new DateTime(2016, 5, 1));
			dates.Add(new DateTime(2016, 5, 2));
			dates.Add(new DateTime(2016, 5, 3));

			dates.Add(new DateTime(2016, 5, 5));

			dates.Add(new DateTime(2016, 6, 1));
			dates.Add(new DateTime(2016, 6, 2));
			dates.Add(new DateTime(2016, 6, 3));

			dates.Add(new DateTime(2016, 6, 5));

			dates.Add(new DateTime(2016, 12, 30));
			dates.Add(new DateTime(2016, 12, 31));
			dates.Add(new DateTime(2017, 1, 1));

			Assert.AreEqual(5, ConsecutiveHelper.GroupingConsecutiveItems(dates.ToArray()).Count);

		}

		[TestMethod]
		public void ConsecutiveListTest()
		{
			List<int> years = new List<int>() { 2001, 2002, 2003, 2005, 2007 };

			string t = ConsecutiveHelper.ConsecutiveToString(years);
			Assert.AreEqual("2001-2003, 2005, 2007", ConsecutiveHelper.ConsecutiveToString(years));
		}
	}
}