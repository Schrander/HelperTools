
using HelperTools.Text;

namespace HelperTools.Normalizations
{
	public class MoneyNormalization : BaseNormalization
	{
		public static readonly MoneyNormalization Instance = new MoneyNormalization();

		public override string MaskPattern()
		{
			return "C0";
		}

		public override string ValidationPattern()
		{
			return "C0";
		}

		public override string FormatPattern()
		{
			return null;
		}

		public override string Sanitize(string value)
		{
			return value.Sanitize();
		}

		public override string Normalize(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			if (!Validate(value)) return null;
			long v;
			long.TryParse(value, out v);
			return $"{v:C0}";
		}

		public override bool Validate(string objectToValidate)
		{
			long v;
			objectToValidate = Sanitize(objectToValidate);
			return !string.IsNullOrEmpty(objectToValidate) && long.TryParse(objectToValidate, out v);
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			throw new System.NotImplementedException();
		}
	}

}