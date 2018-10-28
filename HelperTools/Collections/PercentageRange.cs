using System.ComponentModel;

namespace HelperTools.Collections
{
	[Description("Percentage bereik")]
	public enum PercentageRange
	{
		[Description("0 to 1 range")]
		RangeZeroToOne,

		[Description("0 to 100 range")]
		RangeZeroToHunderd
	}
}