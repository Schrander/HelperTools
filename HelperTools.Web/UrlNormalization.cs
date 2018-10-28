using HelperTools.Normalizations;
using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace HelperTools.Web
{

	/// <summary>
	/// Valideert, normaliseert of formateert een url
	/// </summary>
	public class UrlNormalization : BaseNormalization
	{
		static public readonly UrlNormalization Instance = new UrlNormalization();
		//^(?<drive>([a-zA-Z][:][\\]))(?<directory>([^\*\?\'\<\>\|\@\)\(\}\{]{1,255}[\\]))(?<file>([^\*\?\'\<\>\|\@\)\(\}\{]{1,255}(\.[a-zA-Z]{1,6})?))?
		public override string MaskPattern()
		{
			const string protocol = @"(?n)(?<protocol>http[s]?://)?";
			const string fileProtocol = @"(?<protocol>[fF][iI][lL][eE]:///)";
			const string domain = @"(?<domain>(([a-zA-Z0-9\-\.]+\.)?[a-zA-Z0-9\-]+\.[a-zA-Z]{2,6}/?))";
			const string ip4 = @"(?<domain>(([0-9]{1,3}\.){3}[0-9]{1,3}))";
			const string drive = @"(?<domain>[A-Za-z]:/)";
			const string port = @"(?<port>:[0-9]{1,5})?";
			const string location = @"(?<location>(([^\*\?'\<\>\|@\)\(\}\{]{1,255})/)*)";
			const string file = @"(?<file>([^\*\?'\<\>\|@\)\(\}\{]{1,255}(\.[a-zA-Z0-9]{1,6})?))?";
			const string queryString = @"(?<query>((\?[a-zA-Z0-9%_\.~\-\+=;&:/]*)?))?";
			const string fragmentLocator = @"(?<fragment>((#[a-zA-Z0-9_\-]*)?))?";

			return string.Format("({0}({1}|{2}){3}{6}{7}{8}{9}|{4}{5}{6}{7})", protocol, domain, ip4, port, fileProtocol, drive, location, file, queryString, fragmentLocator);

		}

		//(?<protocol>file:///)(?<domain>[A-Za-z]:/)(?<location>(([^\*\?'\<\>\|@\)\(\}\{]{1,255})/)*)?(?<file>([^\*\?'\<\>\|@\)\(\}\{]{1,255}(\.[a-zA-Z0-9]{1,6})?))?

		public override string ValidationPattern()
		{
			return MaskPattern();
		}

		public override string FormatPattern()
		{
			return "${protocol}${domain}${port}${location}${file}${query}${fragment}";
		}

		public string Normalize(Uri url)
		{
			if (url == null)
				return null;
			return url.AbsoluteUri;
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

		public override string Sanitize(string value)
		{
			return value;
		}
	}
}
