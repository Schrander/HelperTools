using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using HelperTools.Normalizations;
using HelperTools.Text;

namespace HelperTools.IO
{

	/// <summary>
	/// Valideert, normaliseert of formateert een file
	/// </summary>
	public class FileNameNormalization : BaseNormalization
	{
		static public readonly FileNameNormalization Instance = new FileNameNormalization();

		public override string MaskPattern()
		{
			return @"[^\\\/:\*\?""\<\>\|]{0, 255}";
		}

		// http://www.regexlib.com/REDetails.aspx?regexp_id=90	
		public override string ValidationPattern()
		{
			return MaskPattern();
		}

		public override string FormatPattern()
		{
			return @"[^\\\/:\*\?""\<\>\|]{0, 255}";
		}

		public override string Normalize(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			value = Sanitize(value);
			return value;
		}

		/// <summary>
		/// Valideert of een string voldoet aan een url.
		/// </summary>
		/// <param name="objectToValidate"></param>
		/// <returns></returns>
		public override bool Validate(string objectToValidate)
		{
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(objectToValidate, @"[^\\\/:\*\?""\<\>\|]{0, 255}");
		}

		public override string Sanitize(string value)
		{
			value = !string.IsNullOrEmpty(value) ? value.Sanitize() : null;

			if (!string.IsNullOrWhiteSpace(value))
			{
				value = value.Replace(Environment.NewLine, " ");
				value = Regex.Replace(value, "\t", " ");
				value = WebUtility.HtmlDecode(value);
				value = value.Replace("&nbsp;", " ");
				value = value.Replace("&quot;", "'");
			}

			if (!string.IsNullOrEmpty(value) && !Validate(value))
			{
				if (value.Length > 255)
					value = value.Substring(0, 250);

				Dictionary<char, char> replaceChars = new Dictionary<char, char>();
				replaceChars.Add('\\', '«');
				replaceChars.Add('/', '»');
				replaceChars.Add(':', ';');
				replaceChars.Add('?', '!');
				replaceChars.Add('"', '\'');
				replaceChars.Add('<', '«');
				replaceChars.Add('>', '»');
				replaceChars.Add('|', '«');
				replaceChars.Add('*', '★');

				foreach (KeyValuePair<char, char> item in replaceChars)
				{
					if (value.Contains(item.Key.ToString()))
						value = value.Replace(item.Key, item.Value);
				}
			}
			if (!string.IsNullOrWhiteSpace(value))
				value = value.Trim();

			value.CollapseSpaces();

			return value;
		}
	}
}
