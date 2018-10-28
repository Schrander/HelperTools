using System;

namespace HelperTools.Extensions
{
	public static class TimeSpanExt
	{

		public static string ToDisplayFormat(this TimeSpan t)
		{
			if (t == default(TimeSpan))
				return null;

			string shortForm = string.Empty;
			if (t.Hours > 0) shortForm += $"{t.Hours}h";
			if (t.Minutes > 0) shortForm += $"{t.Minutes.ToString(t.Hours > 0 ? "00" : "0")}:";
			shortForm += $"{t.Seconds.ToString(t.Minutes > 0 ? "00" : "0")}.{t.Milliseconds.ToString("000")}";

			return shortForm;
		}

		public static string ToDelayFormat(this TimeSpan t)
		{
			return "+" + ToDisplayFormat(t);
		}

		public static string ToDelayFormat(this TimeSpan? t)
		{
			return t.HasValue ? ToDelayFormat(t.Value) : null;
		}

		public static string ToDisplayFormat(this TimeSpan? t)
		{
			return t.HasValue ? ToDisplayFormat(t.Value) : null;
		}
	}
}
