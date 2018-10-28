using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using static System.Convert;

namespace HelperTools.Helpers
{
	public static class ParserHelper
	{

		#region Parserhelper

		public static string ParseAsString(this string field, string defaultValue = null)
		{
			return !string.IsNullOrWhiteSpace(field) ? field : defaultValue;
		}

		/// <summary>
		/// Converts a field to a specified type. The field could be from the database or querystring.
		/// If the field is empty the default value of the specified type will be returned.
		/// </summary>
		/// <typeparam name="T">The type to convert to</typeparam>
		/// <param name="field">The field.</param>
		/// <returns>The converted field.</returns>
		public static T ParseAs<T>(this object field) where T : struct
		{
			try { return ParseAs(field, default(T)); }
			catch { return default(T); }
		}

		public static T ParseXmlAs<T>(this XmlNode node) where T : struct
		{
			if (node == null)
				return default(T);

			try { return ParseAs(node.InnerText, default(T)); }
			catch { return default(T); }
		}


		public static T ParseAsEnum<T>(this object field)
		{
			Type t = typeof(T);
			if (t.IsEnum)
				return (T)Enum.ToObject(typeof(T), field.ParseAs<int>());

			return default(T);
		}

		/// <summary>
		/// Converts a field to a specified type.
		/// </summary>
		/// <typeparam name="T">The type to convert to</typeparam>
		/// <param name="field">The field.</param>
		/// <param name="defaultValue">The default value of the type to be converted if the field is empty.</param>
		/// <returns>The converted field.</returns>
		public static T ParseAs<T>(this object field, T defaultValue) where T : struct
		{
			try
			{
				Type t = typeof(T);

				if (field == null || System.DBNull.Value.Equals(field))
					return defaultValue; //(T)Convert.ChangeType(defaultValue, typeof(T));

				string f = field.ToString();

				if (t == typeof(int))
				{
					int intValue;
					if (int.TryParse(f, out intValue))
						return (T)ChangeType(intValue, typeof(T));
				}

				if (t == typeof(long))
				{
					long longValue;
					if (long.TryParse(f, out longValue))
						return (T)ChangeType(longValue, typeof(T));
				}

				if (t == typeof(double))
				{
					double doubleValue;
					Regex r = new Regex(@"((?<decimalpoint>[.,]))[^.,]*$");
					f = f.Replace(".", ",").Replace(r, "decimalpoint", ".");
					if (double.TryParse(f, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out doubleValue))
						return (T)ChangeType(doubleValue, typeof(T));
				}

				if (t == typeof(decimal))
				{
					decimal decimalValue;
					Regex r = new Regex(@"((?<decimalpoint>[.,]))[^.,]*$");
					f = f.Replace(".", ",").Replace(r, "decimalpoint", ".");
					if (decimal.TryParse(f, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out decimalValue))
						return (T)ChangeType(decimalValue, typeof(T));
				}

				if (t == typeof(short))
				{
					short shortValue;
					if (short.TryParse(f, out shortValue))
						return (T)ChangeType(shortValue, typeof(T));
				}

				if (t == typeof(sbyte))
				{
					sbyte sbyteValue;
					if (sbyte.TryParse(f, out sbyteValue))
						return (T)ChangeType(sbyteValue, typeof(T));
				}

				if (t == typeof(bool))
				{
					bool boolValue;
					if (bool.TryParse(f, out boolValue))
						return (T)ChangeType(boolValue, typeof(T));
				}

				if (t == typeof(float))
				{
					float floatValue;
					Regex r = new Regex(@"((?<decimalpoint>[.,]))[^.,]*$");
					f = f.Replace(".", ",").Replace(r, "decimalpoint", ".");
					if (float.TryParse(f, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out floatValue))
						return (T)ChangeType(floatValue, typeof(T));
				}

				if (t == typeof(byte))
				{
					byte byteValue;
					if (byte.TryParse(f, out byteValue))
						return (T)ChangeType(byteValue, typeof(T));
				}

				if (t == typeof(char))
				{
					char charValue;
					if (char.TryParse(f, out charValue))
						return (T)ChangeType(charValue, typeof(T));
				}

				if (t == typeof(uint))
				{
					uint intValue;
					if (uint.TryParse(f, out intValue))
						return (T)ChangeType(intValue, typeof(T));
				}

				if (t == typeof(ulong))
				{
					ulong longValue;
					if (ulong.TryParse(f, out longValue))
						return (T)ChangeType(longValue, typeof(T));
				}

				if (t == typeof(ushort))
				{
					ushort ushortValue;
					if (ushort.TryParse(f, out ushortValue))
						return (T)ChangeType(ushortValue, typeof(T));
				}

				if (t == typeof(DateTime))
				{
					DateTime dateValue;
					if (DateTime.TryParse(f, out dateValue))
						return (T)ChangeType(dateValue, typeof(T));
				}

				if (t == typeof(DateTimeOffset))
				{
					DateTimeOffset dateValue;
					if (DateTimeOffset.TryParse(f, out dateValue))
						return (T)ChangeType(dateValue, typeof(T));
				}

				return defaultValue;
			}
			catch
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// Converts a field to a specified type.
		/// If the field is null, the value will be converted to the null value of the specified type.
		/// </summary>
		/// <typeparam name="T">The type to convert to</typeparam>
		/// <param name="field">The field.</param>
		/// <returns>The converted field.</returns>
		public static T? ParseOrDefault<T>(this object field) where T : struct
		{
			try { return ParseOrDefault<T>(field, default(T?)); }
			catch { return default(T?); }
		}

		/// <summary>
		/// Converts a field to a specified type.
		/// If the field is null, the value will be converted to the specified nullable default value.
		/// </summary>
		/// <typeparam name="T">The type to convert to</typeparam>
		/// <param name="field">The field.</param>
		/// <param name="defaultValue">The default value of the type to be converted if the field is empty.</param>
		/// <returns>The converted field.</returns>
		public static T? ParseOrDefault<T>(this object field, T? defaultValue) where T : struct
		{
			try
			{
				if (field == null || System.DBNull.Value.Equals(field))
					return defaultValue;

				if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTimeOffset))
				{
					DateTime d = (DateTime)ChangeType(field, typeof(T));
					if (d == new DateTime(1900, 1, 1) || d == new DateTime(1753, 1, 1))
						return defaultValue;

					return (T)ChangeType(d, typeof(T));
				}

				return ParseAs<T>(field);
			}
			catch
			{
				return defaultValue;
			}
		}

		//ParseOrDefault
		public static T? ParseOrDefault<T>(this object field, T? defaultValue, T toNullValue) where T : struct
		{
			try
			{
				if (field == null || System.DBNull.Value.Equals(field))
					return defaultValue;

				return toNullValue.ToString().Equals(field.ToString(), StringComparison.InvariantCultureIgnoreCase) ? defaultValue : ParseAs<T>(field);
			}
			catch
			{
				return defaultValue;
			}
		}
	}

	#endregion
}