using System.Collections.Generic;
using HelperTools.Extensions;
using System.Text.RegularExpressions;

namespace HelperTools.Web
{
    public static class HtmlHelper
    {

        /// <summary>
        /// Ellipses a string
        /// </summary>
        /// <param name="value">The source string.</param>
        /// <param name="charAmount">The amount of characters.</param>
        /// <returns>An ellipsed string.</returns>
        public static string EllipsisTrimmedHtml(this string value, int charAmount)
        {
            return value.EllipsisTrim(charAmount, "&hellip;");
        }

        public static string FormatAlineas(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
                
            text = Regex.Replace(text, @"\r\n\r\n", "</p><p>");
            text = $"<p>{text}</p>";

            if (text.EndsWith("<p></p>"))
                text = text.Substring(0, text.Length - "<p></p>".Length);

            return text;
        }

        public static string FirstAlinea(this string text)
        {
            string formatedText = FormatAlineas(text);
            string pattern = @"\<p[^\>]*\>([^\<]*)\<\s*\/\s*p\s*\>";

            var matches = Regex.Matches(formatedText, pattern);

            foreach (Match match in matches)
            {
                return $"<p>{match}</p>";
            }

            return null;
        }


        public static string FirstAlineaEllipsis(this string text, int chars = 200)
        {
            string formatedText = FormatAlineas(text);
            string pattern = @"\<p[^\>]*\>([^\<]*)\<\s*\/\s*p\s*\>";

            var matches = Regex.Matches(formatedText, pattern);

            foreach (Match match in matches)
            {
                return $"<p>{match.ToString().EllipsisTrimmedHtml(chars)}</p>";
            }

            return null;
        }

        public static string RemoveHtmlTags(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            const string pattern = @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>";
            return Regex.Replace(text, pattern, "");
        }


    }
}
