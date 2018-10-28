using System.ComponentModel;

namespace HelperTools.IO
{
	[Description("Document type")]
	public enum EnumDocumentType
	{
		[Description("xps")]
		Xps,

		[Description("pdf")]
		Pdf,

		[Description("html")]
		Html,

		[Description("xml")]
		Xml,

	}
}