using System.Collections.Generic;
using System.Linq;
using HelperTools.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class ListExtensionTest
	{

		[TestMethod]
		public void SanitizeTest()
		{
			var list = new List<int>() { 1, 3, 4, 7, 7, 8, 9, 0 };
			list = list.Sanitize();

			var decimals = new List<decimal>() { 1m, 3m, 4m, 7m, 7m, 8m, 9m, 0m };
			decimals = decimals.Sanitize();

			Assert.IsTrue(!list.Contains(0));
			Assert.IsTrue(!decimals.Contains(0));
		}

		public class NameAge
		{
			public string Name { get; set; }
			public int Age { get; set; }

			public NameAge(string name, int age)
			{
				Name = name;
				Age = age;
			}
		}

		[TestMethod]
		public void GetDuplicatesTest()
		{
			var list = new List<int> { 1, 2, 4, 6, 6, 8, 6, 8 };
			var duplicates = list.GetDuplicates();

			var list2 = new List<NameAge>();
			list2.Add(new NameAge("Sander", 38));
			list2.Add(new NameAge("Pieter", 38));
			list2.Add(new NameAge("Erwin", 44));
			list2.Add(new NameAge("Jan", 18));
			list2.Add(new NameAge("Mark", 18));
			list2.Add(new NameAge("Mark", 48));
			list2.Add(new NameAge("Mark", 48));


			var duplicates2 = list2.GroupBy(g => g.Age).Select(s => s.ToList()).ToList();

			var d = list2.GroupByNestedList(g => g.Age);
			var e = list2.GetDuplicates(g => g.Age);
			var f = list2.GetDuplicates(g => g.Name);

		}

	}
}