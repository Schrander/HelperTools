using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HelperTools.Normalizations
{

	// Online RegEx Tester
	// http://derekslager.com/blog/posts/2007/09/a-better-dotnet-regular-expression-tester.ashx
	//
	public abstract class BaseNormalization
	{
		public abstract string MaskPattern();
		public abstract string ValidationPattern();
		public abstract string FormatPattern();
		public abstract string Sanitize(string value);
		public abstract string Normalize(string value);
		public abstract bool Validate(string objectToValidate);
		public abstract bool Validate(string objectToValidate, out string sanitized);


		public virtual string InlineExplicitCapture => @"(?n)";

		public virtual string PrefixPattern => @"(^|[\s""\(\[\{\<\,\|:;])";

		public virtual string SuffixPattern => @"($|[\s""\)\]\}\>\,\|;])";

		// If all you wanted was English digits for \d, English letters, English digits and underscore for
		// \w and whitespace characters you can understand for \s, then you need to set the ECMAScript option. 
		// \d+ will match much more chars than sec the 0 to 9 by default.
		// http://www.rexegg.com/regex-csharp.html
		public virtual RegexOptions Options => RegexOptions.CultureInvariant | RegexOptions.ECMAScript;

		public virtual IEnumerable<string> Matches(string text)
		{
			var matches = Regex.Matches(text, MaskPattern(), Options);

			List<string> list = (from Match m in matches select new Regex(MaskPattern(), Options).Replace(m.Value, FormatPattern())).ToList();

			return list.Distinct().ToArray();
		}
	}
}
