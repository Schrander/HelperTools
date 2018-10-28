using HelperTools.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class StringFormattingTests
	{
		[TestMethod]
		public void RemoveDiacriticsTest()
		{
			string uuml = "ü";
			string u = TextFormatting.RemoveDiacritics(uuml,true);

		}
	}
}
