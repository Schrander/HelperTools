using System.ComponentModel;

namespace HelperTools
{
	[Description("EditMode")]
	public enum EnumEditMode
	{
		[Description("Geen")]
		None = 0,

		[Description("Toevoegen")]
		Add = 1,

		[Description("Bewerken")]
		Edit = 2,

		[Description("Verwijderen")]
		Delete = 3


	}
}