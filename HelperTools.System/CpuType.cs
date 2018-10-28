using System.ComponentModel;

namespace HelperTools.SystemTools
{
	[Description("CPU-type")]
	public enum CpuType
	{
		[Description("32-bit")]
		X86 = 32,

		[Description("64-bit")]
		X64 = 64
	}
}