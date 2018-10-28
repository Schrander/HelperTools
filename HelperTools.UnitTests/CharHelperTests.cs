using HelperTools.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class CharHelperTests
	{
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }


		[TestMethod]
		public void AlfabetListTest()
		{
			var alfabet = CharHelper.AlfabetList;
			Assert.AreEqual(27, alfabet.Count);
		}

	}

}
