using HelperTools.Normalizations;
using HelperTools.Text;
using System.Text.RegularExpressions;

namespace HelperTools.Web
{

	/// <summary>
	/// Valideert, normaliseert of formateert een url
	/// </summary>
	public class HtmlTagNormalization : BaseNormalization
	{
		public static readonly HtmlTagNormalization Instance = new HtmlTagNormalization();
		public override string MaskPattern()
		{
			//const string pattern = @"(?<tag></?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>)";
			string pattern =
					@"(?<tag><(?<tagname>\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*))/?>)(?<innertext>((?<=>).).*)(?<tagend></(\k<tagname>)>)";
			return string.Concat(InlineExplicitCapture, PrefixPattern, pattern, SuffixPattern);
		}


		public override RegexOptions Options => base.Options | RegexOptions.IgnoreCase;

	
		public override string ValidationPattern()
		{
			return MaskPattern();
		}
		
		public override string FormatPattern()
		{
			return "${tag}";
		}


		public override string Normalize(string value)
		{
			return value;
		}

		/// <summary>
		/// Valideert of een string voldoet aan een url.
		/// </summary>
		/// <param name="objectToValidate"></param>
		/// <returns></returns>
		public override bool Validate(string objectToValidate)
		{
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(objectToValidate, ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			sanitized = Sanitize(objectToValidate);
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(sanitized, ValidationPattern(), Options);
		}

		public override string Sanitize(string value)
		{
			return value.Sanitize();
		}
	}
}
