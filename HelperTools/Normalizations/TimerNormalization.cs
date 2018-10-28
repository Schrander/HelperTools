using HelperTools.Helpers;
using System;
using System.Text.RegularExpressions;

namespace HelperTools.Normalizations
{
	public class TimerNormalization : BaseNormalization
	{
		public static readonly TimerNormalization Instance = new TimerNormalization();

		public override string MaskPattern()
		{
			string pattern = @"(?<timer>(?:(?<day>[0-9]{1,3})d?)?(?:(?<hour>^[1-9]|[01][0-9]|2[0-3])h)?((?<minute>^[1-9]|[0-5][0-9])\:)?(?<second>[0-5][0-9])(?:\.(?<milli>[0-9]{1,7}))?)";
			return string.Concat(InlineExplicitCapture, pattern);

		}

		public override string ValidationPattern()
		{
			return MaskPattern();
		}

		public override string FormatPattern()
		{
			return "${timer}";
		}

		public override string Sanitize(string value)
		{
			throw new NotImplementedException();
		}

		public override string Normalize(string value)
		{
			return value;
		}

		public TimeSpan NormalizeTimeSpan(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return new TimeSpan();

			return Validate(value) ? GetTimeSpan(value) : new TimeSpan();
		}

		private static TimeSpan GetTimeSpan(string input)
		{
			Regex r = new Regex(Instance.MaskPattern());

			int day = r.Replace(input, "${day}").ParseAs<int>();
			int hour = r.Replace(input, "${hour}").ParseAs<int>();
			int minute = r.Replace(input, "${minute}").ParseAs<int>();
			int second = r.Replace(input, "${second}").ParseAs<int>();
			int milli = r.Replace(input, "${milli}").ParseAs<int>();

			TimeSpan t = new TimeSpan(day, hour, minute, second, milli);
			return t;
			
		}
		
		public override bool Validate(string objectToValidate)
		{
			objectToValidate = Sanitize(objectToValidate);
			return !string.IsNullOrEmpty(objectToValidate) && Regex.IsMatch(objectToValidate, ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			throw new NotImplementedException();
		}
	}

}