using System.Linq;
using System.Text.RegularExpressions;

namespace HelperTools.Web
{
	/// <summary>
	/// Valideert, normaliseert of formateert een string naar een url
	/// </summary>
	public class ImageUrlNormalization : UrlNormalization
	{
		public new static readonly ImageUrlNormalization Instance = new ImageUrlNormalization();

		private readonly string[] _validImageExtensions = { ".jpg", ".gif", ".png", ".jpe", ".jpeg", ".bmp" };

		public override string ValidationPattern()
		{
			//const string protocol = @"(?<protocol>((http[s]?://)?))";
			//const string domain = @"(?<domain>((([-a-zA-Z0-9]+\.)?[-a-zA-Z0-9]+\.[a-zA-Z]{2,6})))";
			//const string ip4 = @"(?<domain>(((\d{1,3}\.){3}\d{1,3})))";
			//const string port = @"(?<port>((\:\d+)?))";
			//const string location = @"((?<location>((\/)[-a-zA-Z\d%_\.~+=;&]*)))?";
			const string protocol = @"(?<protocol>http[s]?://)?";
			const string domain = @"(?<domain>((([-a-zA-Z0-9]+\.)?[-a-zA-Z0-9]+\.[a-zA-Z]{2,6})))";
			const string ip4 = @"(?<domain>(((\d{1,3}\.){3}\d{1,3})))";
			const string port = @"(:(?<port>((\d+)?)))?";
			const string path = @"(?<path>((([-a-zA-Z\d%_\.~+=;&]+)?(\/))+)?)?";
			const string file = @"(?<file>[-a-zA-Z\d%_\.~+=;&]+)?";

			return $"{protocol}({domain}|{ip4}){port}{path}{file}";
		}

		public override string FormatPattern()
		{
			return "${protocol}${domain}${port}${location}";
		}

		public override bool Validate(string objectToValidate)
		{
			if (string.IsNullOrWhiteSpace(objectToValidate))
				return false;

			return Regex.IsMatch(objectToValidate, ValidationPattern()) && _validImageExtensions.Any(objectToValidate.Contains);
		}
	}
}
