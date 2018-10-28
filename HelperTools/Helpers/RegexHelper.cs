using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HelperTools.Helpers
{
	public static class RegexHelper
	{

		public static string Replace(this string input, Regex regex, string groupName, string replacement)
		{
			return regex.Replace(input, m => ReplaceNamedGroup(groupName, replacement, m));
		}

		public static string Replace(this Regex regex, string input, string groupName, string replacement)
		{
			return regex.Replace(input, m => ReplaceNamedGroup(groupName, replacement, m));
		}

		public static List<string> GetCustomGroups(this Regex regex)
		{
			return regex.GetGroupNames().Where(w => !w.IsNumeric()).ToList();
		}

		public static string ReplaceCustomGroup(this Regex regex, string input, string group)
		{
			if (string.IsNullOrWhiteSpace(group))
				return null;

			group = regex.GetCustomGroups().FirstOrDefault(a => a.Equals(group, StringComparison.CurrentCultureIgnoreCase));
			return !string.IsNullOrWhiteSpace(group) ? regex.Replace(input, CustomGroupFormat(group)) : null;
		}

		public static string CustomGroupFormat(string group)
		{
			if (string.IsNullOrWhiteSpace(group))
				return null;

			return "${" + group.ToLowerInvariant() + "}";
		}

		private static string ReplaceNamedGroup(string groupName, string replacement, Match m)
		{
			string capture = m.Value;
			capture = capture.Remove(m.Groups[groupName].Index - m.Index, m.Groups[groupName].Length);
			capture = capture.Insert(m.Groups[groupName].Index - m.Index, replacement);
			return capture;
		}
	}
}