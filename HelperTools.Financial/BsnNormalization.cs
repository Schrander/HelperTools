using HelperTools.Helpers;
using HelperTools.Normalizations;
using HelperTools.Text;
using System.Text.RegularExpressions;

namespace HelperTools.Financial
{
	public class BsnNormalization : BaseNormalization
	{
		public static readonly BsnNormalization Instance = new BsnNormalization();

		public override string MaskPattern()
		{
			string pattern = @"(?<bsn>[0-9]{4}\.[0-9]{2}\.[0-9]{3})";
			return string.Concat(InlineExplicitCapture, pattern);
		}

		public override string ValidationPattern()
		{
			return @"(?<s1>[0-9]{4})(?<s2>[0-9]{2})(?<s3>[0-9]{3})";
		}

		public override string FormatPattern()
		{
			return "${s1}.${s2}.${s3}";
		}

		public override string Normalize(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			value = Sanitize(value);
			return Validate(value) ? Regex.Replace(value, ValidationPattern(), FormatPattern()) : value;
		}

		public override bool Validate(string objectToValidate)
		{
			objectToValidate = Sanitize(objectToValidate);
			return !string.IsNullOrEmpty(objectToValidate) && ElfproefHelper.IsElfproefBSN(objectToValidate);
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			sanitized = Sanitize(objectToValidate);
			return !string.IsNullOrEmpty(objectToValidate) && ElfproefHelper.IsElfproefBSN(sanitized);
		}

		public override string Sanitize(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			value = value.Sanitize(new[] { " ", "." });
			value = value.TrimStart('0');
			int bsn;
			return int.TryParse(value, out bsn) ? bsn.TrailingZeros(9) : value;
		}
	}
}
