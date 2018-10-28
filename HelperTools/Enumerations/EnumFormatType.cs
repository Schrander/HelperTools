using System.ComponentModel;

namespace HelperTools.Text
{
	[Description("Formatering")]
	public enum EnumFormatType
	{
		[Description("Upper case")]
		UpperCase,

		[Description("Lower case")]
		LowerCase,

		[Description("Pascal case")]
		PascalCase,

		[Description("Camel case")]
		CamelCase,

		[Description("Title case")]
		TitleCase,

		[Description("First letter upper case")]
		FirstLetterUpperCase,

		[Description("First letter lower case")]
		FirstLetterLowerCase,
	}

}
