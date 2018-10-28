using System.Collections.Generic;
using System.Linq;

namespace HelperTools.Web
{
	public static class JavaScriptHelper
	{
		public static string CoalesceJS(this string value, bool isEmptyNull, params string[] strings)
		{
			List<string> list = strings.ToList();
			list.Insert(0, value);
			return list.FirstOrDefault(s => isEmptyNull ? !string.IsNullOrEmpty(s) : s != null);
		}

	}
}
