using System;
using HelperTools.Extensions;

namespace HelperTools.Helpers
{

	public static class SqlHelper
	{

		public static object ToSqlFormat(this object item)
		{
			if (item == null)
				return "Null";

			if (item is string)
			{
				string text = (string)item;

				if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text.Trim()))
					return "Null";
				return $"'{text.Replace("'", "''")}'";
			}

			if (item is TimeSpan)
				return $"\"{((TimeSpan) item).ToDisplayFormat()}\"";

			if (item is DateTime)
				return $"#{item}#";

			if (item is double)
				return item.ToString().Replace(",", ".");

			if (item is bool)
				return (bool)item ? "1" : "0";

			return item;
		}
	}
}