using System.ComponentModel;

namespace HelperTools.SystemTools
{
	[Description("Windows versie")]
	public enum WindowsType
	{
		[Description("Onbekend")]
		Unknown = -1,

		[Description("Windows ME")]
		ME,

		[Description("Windows NT 3.51")]
		NT,

		[Description("Windows NT 4")]
		NT4,

		[Description("Windows XP")]
		XP = 5,

		[Description("Windows Vista")]
		Vista = 6,

		[Description("Windows 7")]
		W7 = 7,

		[Description("Windows 8")]
		W8 = 8,

		[Description("Windows 8.1")]
		W8_1,

		[Description("Windows 10")]
		W10 = 10,

		[Description("Windows 95")]
		W95 = 95,

		[Description("Windows 98")]
		W98 = 98,

		[Description("Windows 98 SE")]
		W98SE,

		[Description("Windows 2000")]
		W2000 = 2000,

	}
}