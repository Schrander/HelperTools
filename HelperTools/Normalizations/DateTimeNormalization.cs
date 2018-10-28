using HelperTools.Helpers;
using HelperTools.Text;
using System.Text.RegularExpressions;

namespace HelperTools.Normalizations
{
	public class DateTimeNormalization : BaseNormalization
	{
		public static readonly DateTimeNormalization Instance = new DateTimeNormalization();

		public override string MaskPattern()
		{
			string pattern = @"(?:0[1-9]|[12][0-9]|3[01])-(?:0[1-9]|1[012])-(?:[1-9]{1}[0-9]{3})";
			return string.Concat(InlineExplicitCapture, pattern);
		}

		public override string ValidationPattern()
		{
			return @"(?<day>0[1-9]|[12][0-9]|3[01])(?<month>0[1-9]|1[012])(?<year>[1-9]{1}[0-9]{3})";
		}

		public override string FormatPattern()
		{
			return "${year}${month}${day}";
		}

		public override string Normalize(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			value = Sanitize(value);
			return Validate(value) ? Regex.Replace(value, ValidationPattern(), FormatPattern()) : value;
		}

		public override bool Validate(string objectToValidate)
		{
			if (string.IsNullOrWhiteSpace(objectToValidate) || objectToValidate.Length != 8)
				return false;

			objectToValidate = Sanitize(objectToValidate);
			return Regex.IsMatch(objectToValidate, ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			sanitized = Sanitize(objectToValidate);
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(sanitized, ValidationPattern(), Options);
		}

		public override string Sanitize(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			value = value.Sanitize(new[] { " ", "-" });
			value = value.TrimStart('0');
			int date;
			return int.TryParse(value, out date) ? date.TrailingZeros(8) : value;
		}
	}
}
