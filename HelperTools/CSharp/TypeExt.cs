using System;
using System.ComponentModel;

namespace HelperTools.CSharp
{
	public static class TypeExt
	{

		public static string GetDescription(this Type type)
		{
			var descriptions = (DescriptionAttribute[])
				type.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return descriptions.Length != 0 ? descriptions[0].Description : "<not defined>";
		}
	}
}
