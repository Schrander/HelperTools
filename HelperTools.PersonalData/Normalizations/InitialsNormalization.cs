using System;
using System.Linq;
using System.Text.RegularExpressions;
using HelperTools.Helpers;
using HelperTools.Normalizations;
using HelperTools.Text;

namespace HelperTools.PersonalData
{
	/// <summary>
	/// Valideert, normaliseert of formateert een string naar initialen.
	/// </summary>
	public class InitialsNormalization : BaseNormalization
	{
		public static readonly InitialsNormalization Instance = new InitialsNormalization();

		public override string MaskPattern() {
			return RegexPatternHelper.GetAlfanumeriekMask(15);
		}

		public override string ValidationPattern() {
			return RegexPatternHelper.GetAlfanumeriekMask(15);
		}

		public override string FormatPattern() {
			return null;
		}

		/// <summary>
		/// Formateert een string naar initialen.
		/// Example: 'abc' >> 'A.B.C.'
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public override string Normalize(string value) {
			if (string.IsNullOrWhiteSpace(value))
				return null;

			value = value.CollapseSpaces();

			string result = null;
			string[] words = value.Split('.');

			foreach (string[] items in words.Select(word => word.Split(' ')))
				result = items.Where(item => !string.IsNullOrEmpty(item)).Aggregate(result, (current, item) => current + SetLetter(item));

			return result;
		}


		//public override void Normalize(ref string value) {
		//	value = Normalize(value);
		//}

		private static string SetLetter(string value) {
			if (value.Equals("th", StringComparison.OrdinalIgnoreCase))
				return "Th.";

			return value.Aggregate<char, string>(null, (current, c) => current + (c.ToString().ToUpper() + "."));
		}

		public override bool Validate(string objectToValidate) {
			return string.IsNullOrEmpty(objectToValidate) || Regex.IsMatch(Sanitize(objectToValidate), ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			throw new NotImplementedException();
		}

		public override string Sanitize(string value) {
			return !string.IsNullOrEmpty(value) ? value.Sanitize(new[] { " ", "." }) : null;
		}
	}
}
