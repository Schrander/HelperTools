using System.Text.RegularExpressions;
using HelperTools.Helpers;
using HelperTools.Text;

namespace HelperTools.Normalizations
{
	/// <summary>
	/// Valideert, normaliseert of formateert een string naar een titel.
	/// </summary>
	public class TitleNormalization : BaseNormalization
	{
		public static readonly TitleNormalization Instance = new TitleNormalization();

		public override string MaskPattern()
		{
			return null;
		}

		public override string ValidationPattern()
		{
			return RegexPatternHelper.GetAlfanumeriekMask(100);
		}

		public override string FormatPattern()
		{
			return null;
		}


		/// <summary>
		/// Formateert een string naar een titel
		/// Example: "jan van galenstraat" becomes "Jan van Galenstraat"
		/// </summary>
		public override string Normalize(string value)
		{
			return !string.IsNullOrWhiteSpace(value) ? value.ToTitleCase() : null;
		}

		public  void Normalize(ref string value)
		{
			value = !string.IsNullOrWhiteSpace(value) ? value.ToTitleCase() : null;
		}

		public override bool Validate(string objectToValidate)
		{
			objectToValidate = Sanitize(objectToValidate);
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(objectToValidate, ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			throw new System.NotImplementedException();
		}

		public override string Sanitize(string value)
		{
			return !string.IsNullOrWhiteSpace(value) ? value.Sanitize() : null;
		}
	}
}
