using System.ComponentModel;

namespace HelperTools
{
	[Description("Datumformatering")]
	public enum DateTimeFormat
	{
		[Description("1-4-2012 - 12:00")]
		ShortDateShortTime = 1,

		[Description("zondag 1 april 2012 - 12:00")]
		LongDateShortTime = 2,

		[Description("zondag 1 april 2012 - 12:00:00")]
		LongDateLongTime = 3,

		[Description("1-4-2012")]
		ShortDateNoTime = 4,

		[Description("zondag 1 april 2012")]
		LongDateNoTime = 5,

		[Description("1 april 2012")]
		LongMonthNoTime = 6,

		[Description("1 april 2012 - 12:00")]
		LongMonthShortTime = 7,

		[Description("1 april 2012 - 12:00:00")]
		LongMonthLongTime = 8,

		[Description("1 apr 2012 - 12:00:00")]
		ShortMonthLongTime = 9,

		[Description("1 apr 2012 - 12:00")]
		ShortMonthShortTime = 10,

		[Description("1 apr 2012")]
		ShortMonthNoTime = 11,

		[Description("zo 1 apr 2012 - 12:00:00")]
		AfkDateLongTime = 12,

		[Description("zo 1 apr 2012 - 12:00")]
		AfkDateShortTime = 13,

		[Description("zo 1 apr 2012")]
		AfkDateNoTime = 14,

		[Description("1 apr 2012 or 10:15")]
		ShortMonthWhenTodayTime = 15,
	}
}