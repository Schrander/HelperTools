using HelperTools.Helpers;
using HelperTools.Normalizations;
using HelperTools.Text;
using System.Text.RegularExpressions;

namespace HelperTools.Web
{

	/// <summary>
	/// Valideert, normaliseert of formateert een url
	/// </summary>
	public class UrlNormalization : BaseNormalization
	{
		public static readonly UrlNormalization Instance = new UrlNormalization();
		//^(?<drive>([a-zA-Z][:][\\]))(?<directory>([^\*\?\'\<\>\|\@\)\(\}\{]{1,255}[\\]))(?<file>([^\*\?\'\<\>\|\@\)\(\}\{]{1,255}(\.[a-zA-Z]{1,6})?))?
		public override string MaskPattern()
		{
			const string protocol = @"(?<protocol>(http[s]?://)?)";
			const string domain = @"(?<domain>(([a-z0-9\-\.]+\.)?[a-z0-9\-]+\.(?<topdomain>[a-z]{2,24}(?=(\s|$|/|:)))))";
			const string ipv4 = @"(?<domain>([0-9]\.|[1-9][0-9]\.|1[0-9]{2}\.|2[0-4][0-9]\.|25[0-5]\.){3}([0-9](?=(\s|$|/|:))|[1-9][0-9](?=(\s|$|/|:))|1[0-9]{2}(?=(\s|$|/|:))|2[0-4][0-9](?=(\s|$|/|:))|25[0-5](?=(\s|$|/|:))))";
			const string port = @"(:?)(?<port>([0-9]{1,5})?)(?=(\s|$|/))";
			const string location = @"(?<location>(([a-zA-Z0-9%_\.~\-\+=;&]{0,255})/)*)";
			const string file = @"(?<file>([a-z0-9%_\.~\-\+=;&]{1,255})?)";
			const string queryString = @"((?<query>\?([a-z0-9%_\.~\-\+=;&:/]*))?)";
			const string fragmentLocator = @"(?<fragment>(#[a-z0-9_\-]*)?)";

			string mainDomain = $"({domain}|{ipv4})";

			string urlPattern = string.Format(@"(?<url>{0})", string.Concat(protocol, mainDomain, port, location, file, queryString, fragmentLocator));

			return string.Concat(InlineExplicitCapture, PrefixPattern, urlPattern, SuffixPattern);
		}


		public override RegexOptions Options => base.Options | RegexOptions.IgnoreCase;

		//(?<protocol>file:///)(?<domain>[A-Za-z]:/)(?<location>(([^\*\?'\<\>\|@\)\(\}\{]{1,255})/)*)?(?<file>([^\*\?'\<\>\|@\)\(\}\{]{1,255}(\.[a-zA-Z0-9]{1,6})?))?

		public override string ValidationPattern()
		{
			return MaskPattern();
		}

		public string Protocol(string value)
		{
			return Regex.Replace(value, MaskPattern(), "${protocol}", Options);
		}

		public string Domain(string value)
		{
			return Regex.Replace(value, MaskPattern(), "${domain}", Options);
		}

		public int? Port(string value)
		{
			return Regex.Replace(value, MaskPattern(), "${port}", Options).ParseOrDefault<int>();
		}

		public string Location(string value)
		{
			return Regex.Replace(value, MaskPattern(), "${location}", Options);
		}


		public string File(string value)
		{
			return Regex.Replace(value, MaskPattern(), "${file}", Options);
		}

		public string QueryString(string value)
		{
			return Regex.Replace(value, MaskPattern(), "${query}", Options);
		}

		public string FragmentLocator(string value)
		{
			return Regex.Replace(value, MaskPattern(), "${fragment}", Options);
		}

		public override string FormatPattern()
		{
			return "${url}";
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
