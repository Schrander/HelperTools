using System;
using System.Reflection;

namespace HelperTools.Attributes
{
	public class CultureInfoAttribute : Attribute
	{
		public string CultureName { get; set; }

		public CultureInfoAttribute(string cultureName)
		{
			CultureName = cultureName;
		}

		public static string GetCultureName(Enum value)
		{
			Type type = value.GetType();
			MemberInfo[] memInfo = type.GetMember(value.ToString());

			if (memInfo.Length <= 0) 
				return value.ToString();

			object[] attrs = memInfo[0].GetCustomAttributes(typeof(CultureInfoAttribute), false);
			return attrs.Length > 0 ? ((CultureInfoAttribute)attrs[0]).CultureName : value.ToString();
		}
	}
}
