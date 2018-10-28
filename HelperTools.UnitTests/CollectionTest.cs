using System.Collections.Generic;
using HelperTools.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class CollectionTest
	{

		[TestMethod]
		public void SanitizeTest()
		{
			var list = new List<int>() { 2001, 2002, 2003, 2005, 2006 };
			var items = ConsecutiveHelper.GroupingConsecutiveItems(list);

		}


	}
}