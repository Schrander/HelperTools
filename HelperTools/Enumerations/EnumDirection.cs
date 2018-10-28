using System.ComponentModel;

namespace HelperTools.Enumerations
{
	[Description("Richting")]
	public enum EnumDirection : int
	{
		[Description("Naar beneden")]
		Downward = 1,
		[Description("Naar boven")]
		Upward = 2
	}
}