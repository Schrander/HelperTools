using HelperTools.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class HtmlTest
	{

		[TestMethod]
		public void ToJsonTest()
		{

		string test = @"<XmlTest>
			<Columns>
			 <Column Name=""key1"" DataType=""System.Boolean"">True</Column>
			 <Column Name=""key2"" DataType=""System.String"">Hello World</Column>
			 <Column Name=""key3"" DataType=""System.Int32"">999</Column>
		  </Columns>
			<ExtraColumns>
			 <ExtraColumn Name=""key1"" DataType=""System.Boolean"">False</ExtraColumn>
			 <ExtraColumn Name=""key2"" DataType=""System.String"">Goodbye</ExtraColumn>
			 <ExtraColumn Name=""key3"" DataType=""System.Int32"">123</ExtraColumn>
		  </ExtraColumns>
		  </XmlTest>";
			var json = test.XmlToJSON();

			var xml = JsonHelper.JSONtoXML(json);
		}

	}
}