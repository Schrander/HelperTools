using HelperTools.Normalizations;
using HelperTools.Text;
using System.Text.RegularExpressions;

namespace HelperTools.PersonalData
{
	/// <summary>
	/// Valideert, formateert of een string een geldige e-mailadres is.
	/// </summary>
	public class EmailNormalization : BaseNormalization
	{
		//http://en.wikipedia.org/wiki/Email_address
		public static readonly EmailNormalization Instance = new EmailNormalization();

		public override string MaskPattern()
		{
			const string emailPattern = @"(?<email>(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`|~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*([a-z]{1,6}))";

			return string.Concat(InlineExplicitCapture, PrefixPattern, emailPattern, SuffixPattern);
		}

		public override string SuffixPattern => @"($|[\s""\)\]\}\>\,\|\.;])";

		public override string ValidationPattern()
		{
			return MaskPattern();
		}

		public override string FormatPattern()
		{
			string email = "${email}";
			return email.EndsWith(".") ? email.Substring(0, email.Length - 1) : email;
		}

		/// <summary>
		/// Formateert een string naar een e-mailadres.
		/// </summary>
		public override string Normalize(string value)
		{
			string email;
			bool isValid = Validate(value, out email) && email.Length <= 254; //	https://www.rfc-editor.org/errata_search.php?eid=1690
			value = isValid ? new Regex(MaskPattern(), Options).Replace(email, FormatPattern()) : value;

			return value;
		}

		public override RegexOptions Options => base.Options | RegexOptions.IgnoreCase;

		/// <summary>
		/// Valideert of een string een geldig e-mailadres is.
		/// </summary>
		public override bool Validate(string objectToValidate)
		{
			return string.IsNullOrEmpty(objectToValidate) || Regex.IsMatch(objectToValidate, MaskPattern(), Options);
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			sanitized = objectToValidate.Sanitize();
			return string.IsNullOrEmpty(objectToValidate) || Regex.IsMatch(objectToValidate, MaskPattern(), Options);
		}

		public override string Sanitize(string value)
		{
			return !string.IsNullOrEmpty(value) ? value.Sanitize() : null;
		}
	}
}
