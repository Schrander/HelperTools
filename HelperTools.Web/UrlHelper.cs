using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HelperTools.Web
{
	public static class UrlHelper
	{
		static Regex RegexStartsWithSlash = new Regex("^/[a-zA-Z0-9]");
		static Regex RegexLinkElements = new Regex(@"(?<link><(?<tag>link).*?(?<href>href=[""'](?<linkhref>.*?)[""']).*?/>)");
		static Regex RegexScriptElements = new Regex(@"<(?<tag>script).*?(?<src>src=[""'](?<scriptsrc>.*?)[""']).*?>(?<text>[\s\S]*?)</\k<tag>>");
		static Regex RegexImgElements = new Regex(@"(?<img><(?<tag>img).*?(?<src>src=[""'](?<imgsrc>.*?)[""']).*?/>)");
		static Regex RegexDisallowed = new Regex(@"[/:?#\[\]@!$€&'`‘’“”()*+\.,;=\s\""\<\>\\]+");
		static Regex RegexDisallowedExtra = new Regex(@"[^\.\-\s()+@a-zA-Z0-9]+");

		public static string UrlPattern
		{
			get
			{
				const string protocol = @"(?<protocol>http[s]?://)?";
				const string domain = @"(?<domain>((([-a-zA-Z0-9]+\.)?[-a-zA-Z0-9]+\.[a-zA-Z]{2,6})))";
				const string ip4 = @"(?<domain>(((\d{1,3}\.){3}\d{1,3})))";
				const string port = @"(:(?<port>((\d+)?)))?";
				const string path = @"(?<path>((([-a-zA-Z\d%_\.~+=;&]+)?(\/))+)?)?";
				const string file = @"(?<file>[-a-zA-Z\d%_\.~+=;&]+)?";

				return $"{protocol}({domain}|{ip4}){port}{path}{file}";
			}
		}


		/// <summary>
		/// Formats the slug.
		/// </summary>
		/// <param name="slug">The slug.</param>
		/// <param name="transformGerman">if set to <c>true</c> [transform german].</param>
		/// <returns></returns>
		public static string FormatSlug(string slug, bool transformGerman = false)
		{
			return string.IsNullOrWhiteSpace(slug) ? slug : RemoveDiacritics(slug, transformGerman).Trim('-').ToLowerInvariant();
		}

		/// <summary>
		/// Formats a multipart slug
		/// Beware it removes starting and trailing slashes
		/// </summary>
		/// <param name="multiPartSlug">The multi part slug.</param>
		/// <param name="allowLeadingSlash">if set to <c>true</c> [allow leading slash].</param>
		/// <param name="transformGerman">if set to <c>true</c> [transform german].</param>
		/// <returns></returns>
		public static string FormatMultiPartSlug(string multiPartSlug, bool allowLeadingSlash = false,
			bool transformGerman = false)
		{
			if (string.IsNullOrWhiteSpace(multiPartSlug)) return multiPartSlug;

			var addLeadingSlash = allowLeadingSlash && multiPartSlug.StartsWith("/");
			var result = string.Empty;
			var parts = multiPartSlug.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var part in parts)
			{
				if (part.StartsWith("{") && part.EndsWith("}"))
				{
					//Replace instruction, so leave it
					result = result.Length == 0 ? part : string.Concat(result, "/", part);
				}
				else
				{
					result = result.Length == 0
						? FormatSlug(part, transformGerman)
						: string.Concat(result, "/", FormatSlug(part, transformGerman));
				}
			}

			if (addLeadingSlash) result = "/" + result;
			return result;
		}

		/// <summary>
		/// Removes the diacritics.
		/// </summary>
		/// <param name="slug">The slug.</param>
		/// <param name="transformGerman">if set to <c>true</c> [transform german].</param>
		/// <param name="skipDisallowed">if set to <c>true</c> [skip disallowed].</param>
		/// <returns></returns>
		public static string RemoveDiacritics(string slug, bool transformGerman = false, bool skipDisallowed = false)
		{
			if (string.IsNullOrWhiteSpace(slug))
				return slug;

			if (transformGerman)
			{
				StringBuilder sbTransform = new StringBuilder();

				for (int ich = 0; ich < slug.Length; ich++)
				{
					switch (slug[ich])
					{
						case 'Ö':
							sbTransform.Append("Oe");
							break;
						case 'ö':
							sbTransform.Append("oe");
							break;
						case 'Ä':
							sbTransform.Append("Ae");
							break;
						case 'ä':
							sbTransform.Append("ae");
							break;
						case 'Ü':
							sbTransform.Append("Ue");
							break;
						case 'ü':
							sbTransform.Append("ue");
							break;
						default:
							sbTransform.Append(slug[ich]);
							break;
					}
				}

				slug = sbTransform.ToString();
			}

			//http://unicode.org/reports/tr15/#Norm_Forms
			// Ä -> A"
			string stFormD = slug.Normalize(NormalizationForm.FormD);
			StringBuilder sb = new StringBuilder();

			for (int ich = 0; ich < stFormD.Length; ich++)
			{
				UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
				if (uc != UnicodeCategory.NonSpacingMark)
				{
					switch (stFormD[ich])
					{
						case 'ß':
							sb.Append("ss");
							break;
						case 'æ':
							sb.Append("ae");
							break;
						case 'Æ':
							sb.Append("Ae");
							break;
						case 'ø':
							sb.Append("o");
							break;
						case 'Ø':
							sb.Append("O");
							break;
						case '²':
							//Skip
							break;
						case 'ł':
							sb.Append("l");
							break;
						default:
							sb.Append(stFormD[ich]);
							break;
					}
				}
			}

			var prettyUrl = skipDisallowed ? sb.ToString() : RegexDisallowed.Replace(sb.ToString(), "-").Trim('-');

			return (prettyUrl.Normalize(NormalizationForm.FormC));
		}

		/// <summary>
		/// Removes the diacritics.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="transformGerman">if set to <c>true</c> [transform german].</param>
		/// <param name="skipDisallowed">if set to <c>true</c> [skip disallowed].</param>
		/// <returns></returns>
		public static string RemoveDiacriticsEx(string text, bool transformGerman = false, bool skipDisallowed = false)
		{
			if (string.IsNullOrWhiteSpace(text))
				return text;

			if (transformGerman)
			{
				StringBuilder sbTransform = new StringBuilder();

				for (int ich = 0; ich < text.Length; ich++)
				{
					switch (text[ich])
					{
						case 'Ö':
							sbTransform.Append("Oe");
							break;
						case 'ö':
							sbTransform.Append("oe");
							break;
						case 'Ä':
							sbTransform.Append("Ae");
							break;
						case 'ä':
							sbTransform.Append("ae");
							break;
						case 'Ü':
							sbTransform.Append("Ue");
							break;
						case 'ü':
							sbTransform.Append("ue");
							break;
						default:
							sbTransform.Append(text[ich]);
							break;
					}
				}

				text = sbTransform.ToString();
			}

			string stFormD = text.Normalize(NormalizationForm.FormD);
			StringBuilder sb = new StringBuilder();

			for (int ich = 0; ich < stFormD.Length; ich++)
			{
				UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
				if (uc != UnicodeCategory.NonSpacingMark)
				{
					switch (stFormD[ich])
					{
						case 'ß':
							sb.Append("ss");
							break;
						case 'æ':
							sb.Append("ae");
							break;
						case 'Æ':
							sb.Append("AE");
							break;
						case 'ø':
							sb.Append("o");
							break;
						case 'Ø':
							sb.Append("O");
							break;
						case '°':
							sb.Append("o");
							break;
						case '²':
							//Skip
							break;
						default:
							sb.Append(stFormD[ich]);
							break;
					}
				}
			}

			var prettyText = skipDisallowed ? sb.ToString() : RegexDisallowedExtra.Replace(sb.ToString(), "-").Trim('-');

			return (prettyText.Normalize(NormalizationForm.FormC));
		}

		/// <summary>
		/// Relative paths to absolute paths.
		/// </summary>
		/// <param name="isSecure">if set to <c>true</c> [is secure].</param>
		/// <param name="authority">The authority.</param>
		/// <param name="path">The path.</param>
		/// <param name="stripQuerystring">if set to <c>true</c> [strip querystring].</param>
		/// <param name="stripProtocol">if set to <c>true</c> [strip protocol].</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">url</exception>
		public static string Absolutize(bool isSecure, string authority, string path, bool stripQuerystring, bool stripProtocol)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));

			if (!path.StartsWith("http") && !path.StartsWith("//"))
			{
				string host = string.Concat(stripProtocol ? "//" : "http://", authority.ToLowerInvariant());
				path = AddDomain(host, path);
			}

			if (isSecure)
				path = path.Replace("http:", "https:");

			if (stripQuerystring)
			{
				var pos = (path.IndexOfAny(new[] { '?', '#' }));
				if (pos >= 0)
					path = path.Substring(0, pos);
			}

			return path;
		}

		/// <summary>
		/// Adds as querystyring.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="nvc">The NVC.</param>
		/// <param name="slugifyValues">if set to <c>true</c> [slugify values].</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">path</exception>
		public static string AddAsQuerystring(string path, NameValueCollection nvc, bool slugifyValues = false)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));

			var starter = !path.Contains('?');
			var loner = path.EndsWith("?", StringComparison.OrdinalIgnoreCase) || path.EndsWith("&", StringComparison.OrdinalIgnoreCase);

			foreach (var key in nvc.AllKeys)
			{
				var d_key = RemoveDiacritics(key);
				var value = nvc[key];

				if (value != null)
				{
					var values = value.Split(',');

					foreach (var item in values)
					{
						// querystring values
						var d_value = slugifyValues ? RemoveDiacritics(item) : item;

						if (loner)
						{
							path = string.Concat(path, key, '=', d_value);
							loner = false;
						}
						else if (starter)
						{
							path = string.Concat(path, '?', key, '=', d_value);
							starter = false;
						}
						else
						{
							path = string.Concat(path, '&', key, '=', d_value);
						}
					}
				}
			}

			return path;
		}

		/// <summary>
		/// Replaces the or add subdomain.
		/// img src=www.campingbooking -&gt; img src=cdn.campingbooking.com
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="subdomainTo">The subdomain to.</param>
		/// <param name="html">The HTML.</param>
		/// <returns></returns>
		public static string ReplaceOrAddSubdomain(string host, string subdomainTo, string html)
		{
			if (NullableHelper.AnyIsNull(host, subdomainTo, html))
				return html;

			// all imgs
			using (var regexImgReplacer = new RegexSubdomainReplacer("imgsrc", host, subdomainTo))
			{
				html = RegexImgElements.Replace(html, regexImgReplacer.Replace);
			}

			// all script
			using (var regexScriptReplacer = new RegexSubdomainReplacer("scriptsrc", host, subdomainTo))
			{
				html = RegexScriptElements.Replace(html, regexScriptReplacer.Replace);
			}

			// all link
			using (var regexLinkReplacer = new RegexSubdomainReplacer("linkhref", host, subdomainTo))
			{
				html = RegexLinkElements.Replace(html, regexLinkReplacer.Replace);
			}

			// any plain stuff
			if (html.StartsWith("//") || html.StartsWith("http"))
				html = ReplaceOrAddSubdomainPath(host, subdomainTo, html);

			return html;
		}

		/// <summary>
		/// Replaces the or add subdomain.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="subdomainTo">The subdomain to.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static string ReplaceOrAddSubdomainPath(string host, string subdomainTo, string path)
		{
			if (NullableHelper.AnyIsNull(host, subdomainTo, path))
				return path;

			// path: //www.campingbooking.com 
			// host: campingbooking.com 
			// subdomainto: cdn. 
			// -> //cdn.campingbooking.com
			Regex regexHostNoDomain = new Regex(string.Concat("//www.", host));
			path = regexHostNoDomain.Replace(path, string.Concat("//", subdomainTo, host));

			// path: //www.campingbooking.com 
			// host: www.campingbooking.com 
			// subdomainto: cdn. 
			// -> //cdn.campingbooking.com
			var hostWithNewSubdomain = host.Replace("www.", subdomainTo);
			path = path.Replace("//" + host, "//" + hostWithNewSubdomain);

			// path: //campingbooking.com 
			// host: campingbooking.com 
			// subdomainto: cdn. 
			// -> //cdn.campingbooking.com
			Regex regexHostNoWWW = new Regex(string.Concat("//", host));
			path = regexHostNoWWW.Replace(path, string.Concat("//", subdomainTo, host));

			return path;
		}

		/// <summary>
		/// Adds the domain to any relative path
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="html">The HTML.</param>
		/// <returns></returns>
		public static string AddDomain(string host, string html)
		{
			if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(html))
				return html;

			// all imgs
			using (var regexImgReplacer = new RegexDomainAdder("imgsrc", host))
			{
				html = RegexImgElements.Replace(html, regexImgReplacer.Replace);
			}

			// all script
			using (var regexScriptReplacer = new RegexDomainAdder("scriptsrc", host))
			{
				html = RegexScriptElements.Replace(html, regexScriptReplacer.Replace);
			}

			// all link
			using (var regexLinkReplacer = new RegexDomainAdder("linkhref", host))
			{
				html = RegexLinkElements.Replace(html, regexLinkReplacer.Replace);
			}

			// any plain stuff
			if (html.StartsWith("/"))
				html = AddDomainPath(host, html);

			//Also take ~/ into account
			else if (html.StartsWith("~/"))
				html = AddDomainPath(host, html.Substring(1));

			return html;
		}

		/// <summary>
		/// Adds the domain to a relative path
		/// </summary>
		/// <param name="host">The host.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static string AddDomainPath(string host, string path)
		{
			if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(path))
				return path;

			if (RegexStartsWithSlash.IsMatch(path))
				path = string.Concat(host, path);

			// homepage 
			if (path.Equals("/"))
				path = host;

			return path;
		}

		public static string ImageToLowerNoTilde(string src)
		{
			if (string.IsNullOrEmpty(src))
				return string.Empty;

			if (src.StartsWith("~"))
				src = src.Substring(1);

			return src.ToLowerInvariant();
		}


	}

	public class RegexSubdomainReplacer : IDisposable
	{
		private string _groupToReplace;
		private string _subdomainTo;
		private string _host;

		public RegexSubdomainReplacer(string groupToReplace, string host, string subdomainTo)
		{
			_groupToReplace = groupToReplace;
			_subdomainTo = subdomainTo;
			_host = host;
		}

		public string Replace(Match match)
		{
			var matchGroup = match.Groups[_groupToReplace];

			if (matchGroup != null)
			{
				string imgsrc = matchGroup.Value;

				var newsubdomain = UrlHelper.ReplaceOrAddSubdomainPath(_host, _subdomainTo, matchGroup.Value);

				return match.Value.Replace(matchGroup.Value, newsubdomain);
			}
			return match.Value;
		}

		public void Dispose()
		{

		}
	}

	public class RegexDomainAdder : IDisposable
	{
		private string _groupToReplace;
		private string _host;

		public RegexDomainAdder(string groupToReplace, string host)
		{
			_groupToReplace = groupToReplace;
			_host = host;
		}

		public string Replace(Match match)
		{
			var matchGroup = match.Groups[_groupToReplace];

			if (matchGroup == null)
				return match.Value;

			string imgsrc = matchGroup.Value;

			string newdomain = UrlHelper.AddDomainPath(_host, matchGroup.Value);
			return match.Value.Replace(matchGroup.Value, newdomain);
		}

		public void Dispose()
		{

		}
	}
	
}