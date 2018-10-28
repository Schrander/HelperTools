using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace HelperTools
{
	public static class EnumExt
	{


		public static string GetDescription(this Enum value)
		{
			try
			{
				FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
				object[] attribArray = fieldInfo.GetCustomAttributes(false);

				if (attribArray.Length <= 0)
					return value.ToString();

				DescriptionAttribute attribute = null;
				foreach (var item in attribArray)
				{
					attribute = item as DescriptionAttribute;
					if (attribute != null)
						break;

				}
				return attribute != null ? attribute.Description : value.ToString();
			}
			catch (Exception)
			{
				return "Unknown";
			}
		}

		//http://stackoverflow.com/questions/276585/enumeration-extension-methods
		public static IEnumerable<T> GetFlaggedValues<T>(this T enumType, T enumValues) where T : struct, IConvertible
		{
			Type type = enumType.GetType();

			if (!type.IsDefined(typeof(FlagsAttribute), false))
				return new List<T>();


			List<T> items = new List<T>();
			var collection = ToElementsCollection(enumType);
			foreach (var item in collection)
			{
				if (HasFlag(enumValues, item))
					items.Add(item);
			}
			return items;
		}



		public static IEnumerable<T> GetFlaggedValues<T>(this T enumType) where T : struct, IConvertible
		{
			return GetFlaggedValues(enumType, enumType);
		}


		public static void ForEach(this Enum enumType, Action<Enum> action)
		{
			foreach (var type in Enum.GetValues(enumType.GetType()))
				action((Enum)type);
		}

		public static bool HasFlag<T>(this T variable, T value) where T : struct, IConvertible
		{
			if (!Enum.IsDefined(variable.GetType(), value))
				throw new ArgumentException($"Enumeration type mismatch.  The flag is of type '{value.GetType()}', was expecting '{variable.GetType()}'.");

			ulong num = Convert.ToUInt64(value);
			return ((Convert.ToUInt64(variable) & num) == num);
		}

		public static IEnumerable<T> ToElementsCollection<T>(this T value) where T : struct, IConvertible
		{
			if (typeof(T).IsEnum == false)
				throw new Exception("typeof(T).IsEnum == false");

			return Enum.GetValues(typeof(T)).Cast<T>();
		}


		public static IEnumerable<string> ToStringElementsCollection<T>(this T value) where T : struct, IConvertible
		{
			return ToElementsCollection(value).Select(item => item.ToString()).ToList();
		}


		public static List<string> ToString(this Array array)
		{
			return (from object item in array select item.ToString()).ToList();
		}

		public static T ToEnum<T>(int value)
		{
			Type t = typeof(T);
			return t.IsEnum ? (T)Enum.ToObject(typeof(T), value) : default(T);
		}

		public static int ToInt32(this Enum value)
		{
			return (int)Convert.ChangeType(value, typeof(int));
		}

		public static int EnumConcat(params Enum[] value)
		{
			int items = 0;

			foreach (var item in value)
			{
				var i = (int)item.GetType().GetField("value__").GetValue(item);
				items |= i;
			}
			return items;
		}

		public static T ConvertByName<T>(this Enum value) where T : struct, IConvertible
		{
			return (T)Enum.Parse(typeof(T), Enum.GetName(value.GetType(), value));
		}

		public static T ConvertByValue<T>(this Enum value) where T : struct, IConvertible
		{
			return (T)(dynamic)(int)(object)value;
		}
		public static T Add<T>(this Enum type, T value)
		{
			try
			{
				return (T)(object)((int)(object)type | (int)(object)value);
			}
			catch (Exception ex)
			{
				throw new ArgumentException($"Could not append value from enumerated type '{typeof(T).Name}'.", ex);
			}
		}

		public static T Remove<T>(this Enum type, T value)
		{
			try
			{
				return (T)(object)((int)(object)type & ~(int)(object)value);
			}
			catch (Exception ex)
			{
				throw new ArgumentException($"Could not remove value from enumerated type '{typeof(T).Name}'.", ex);
			}
		}
	}
}

