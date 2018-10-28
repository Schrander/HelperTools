
using System.ComponentModel;

namespace HelperTools
{
	[Description("Tijdseenheid")]
	public enum TimeUnit
	{
		[Description("Dagen")]
		Days,

		[Description("Jaren")]
		Years,

		[Description("Maanden")]
		Months,

		[Description("Uren")]
		Hours,

		[Description("Milliseconds")]
		Milliseconds,

		[Description("Seconds")]
		Seconds,

		[Description("Minutes")]
		Minutes,

		[Description("Weeks")]
		Weeks,

	}
}
