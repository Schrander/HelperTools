using System.ComponentModel;

namespace HelperTools.SystemTools
{

	[Description("Operating System type")]
	public enum OperatingSystemType
	{
		[Description("Onbekend")]
		Unknown = -1,

		[Description("Windows ME")]
		WindowsME,

		[Description("Windows NT 3.51")]
		WindowsNT,

		[Description("Windows NT 4")]
		WindowsNT4,

		[Description("Windows XP")]
		WindowsXP = 5,

		[Description("Windows Vista")]
		WindowsVista = 6,

		[Description("Windows 7")]
		Windows7 = 7,

		[Description("Windows 8")]
		Windows8 = 8,

		[Description("Windows 8.1")]
		Windows8_1,

		[Description("Windows 10")]
		Windows10 = 10,

		[Description("Windows 95")]
		Windows95 = 95,

		[Description("Windows 98")]
		Windows98 = 98,

		[Description("Windows 98 SE")]
		Windows98SE,

		[Description("Windows 2000")]
		Windows2000 = 2000,
	}
}