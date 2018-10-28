using System.Linq;
using System.Text.RegularExpressions;
using HelperTools.Helpers;
using HelperTools.Normalizations;
using HelperTools.Text;

namespace HelperTools.PersonalData
{
	/// <summary>
	/// Valideert, normaliseert of formateert een achternaam.
	/// </summary>
	public class LastNameNormalization : BaseNormalization
	{
		public static readonly LastNameNormalization Instance = new LastNameNormalization();

		public override string MaskPattern()
		{
			return RegexPatternHelper.GetAlfanumeriekMask(100);
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
		/// Formateert een achternaam.
		/// </summary>
		/// <param name="value">de achternaam</param>
		/// <returns></returns>
		public override string Normalize(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			value = value.ToTitleCase();

			MatchCollection matches = Regex.Matches(value, @"[v][ \.]", RegexOptions.IgnoreCase);
			value = matches.Cast<Match>().Aggregate(value, (current, m) => current.Replace(m.Value, "v. "));

			matches = Regex.Matches(value, @"[d][ \.]", RegexOptions.IgnoreCase);
			value = matches.Cast<Match>().Aggregate(value, (current, m) => current.Replace(m.Value, "d. "));
			
			value = value.Replace("v. d. ", "v.d. ");

			return value;
		}

		//public override void Normalize(ref string value)
		//{
		//	value = Normalize(value);
		//}

		public override bool Validate(string objectToValidate)
		{
			return string.IsNullOrEmpty(objectToValidate) || Regex.IsMatch(Sanitize(objectToValidate), ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			throw new System.NotImplementedException();
		}

		public override string Sanitize(string value)
		{
			return !string.IsNullOrEmpty(value) ? value.Sanitize(new[] { " " }) : null;
		}
	}
}
