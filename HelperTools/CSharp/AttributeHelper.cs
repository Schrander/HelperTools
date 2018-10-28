using System;
using System.Collections.Generic;
using System.Reflection;

namespace HelperTools.CSharp
{
	public static class AttributeHelper
	{
		public static T GetAttributesClass<T>(object obj) where T : Attribute
		{
			object[] items = obj.GetType().GetCustomAttributes(typeof(T), false);
			return items.Length <= 0 ? default(T) : (T)items[0];
		}

		public static List<T> GetAttributeMembers<T>(object obj)
		{
			try
			{
				List<T> list = new List<T>();
				MemberInfo[] members = obj.GetType().GetMembers();
				foreach (MemberInfo member in members)
				{

				}
				return list;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		
		public static Dictionary<string, object> GetPropertyAttributes(PropertyInfo property)
		{
			Dictionary<string, object> attribs = new Dictionary<string, object>();
			// look for attributes that takes one constructor argument
			foreach (CustomAttributeData attribData in property.GetCustomAttributesData())
			{
				string typeName = attribData.Constructor.DeclaringType.Name;
				if (typeName.EndsWith("Attribute")) typeName = typeName.Substring(0, typeName.Length - 9);

				foreach (var item in attribData.ConstructorArguments)
					attribs[typeName] = attribData.ConstructorArguments[0].Value;
			}
			return attribs;
		}

		public static T GetAttributeMember<T>(object obj, string memberName)
		{
			try
			{
				MemberInfo[] members = obj.GetType().GetMember(memberName);
				foreach (MemberInfo member in members)
				{
					object[] memberAttributes = member.GetCustomAttributes(typeof(T), false);
					if (memberAttributes.Length > 0)
						return (T)memberAttributes[0];
				}
				return default(T);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
