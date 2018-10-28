using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HelperTools.Extensions
{
	public static class ObjectExt
	{

		public static T Clone<T>(this T original)
		{
			return (T)Clone((object)original);
		}

		private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

		public static bool IsPrimitive(this Type type)
		{
			return type == typeof(string) || type.IsValueType & type.IsPrimitive;
		}

		public static object Clone(this object originalObject)
		{
			return InternalClone(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()));
		}

		private static object InternalClone(object originalObject, IDictionary<object, object> visited)
		{
			if (originalObject == null)
				return null;

			var typeToReflect = originalObject.GetType();
			if (IsPrimitive(typeToReflect))
				return originalObject;

			if (visited.ContainsKey(originalObject))
				return visited[originalObject];

			if (typeof(Delegate).IsAssignableFrom(typeToReflect))
				return null;

			var cloneObject = CloneMethod.Invoke(originalObject, null);
			if (typeToReflect.IsArray)
			{
				var arrayType = typeToReflect.GetElementType();
				if (IsPrimitive(arrayType) == false)
				{
					Array clonedArray = (Array)cloneObject;
					clonedArray.ForEach((array, indices) => array.SetValue(InternalClone(clonedArray.GetValue(indices), visited), indices));
				}
			}

			visited.Add(originalObject, cloneObject);
			CloneFields(originalObject, visited, cloneObject, typeToReflect);
			RecursiveCloneBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
			return cloneObject;
		}

		private static void RecursiveCloneBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
		{
			if (typeToReflect.BaseType != null)
			{
				RecursiveCloneBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
				CloneFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
			}
		}

		private static void CloneFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
		{
			foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
			{
				if (filter != null && filter(fieldInfo) == false)
					continue;

				if (IsPrimitive(fieldInfo.FieldType))
					continue;

				var originalFieldValue = fieldInfo.GetValue(originalObject);
				var clonedFieldValue = InternalClone(originalFieldValue, visited);
				fieldInfo.SetValue(cloneObject, clonedFieldValue);
			}
		}


		/// <summary>
		/// Checks if all items does have a value.
		/// </summary>
		/// <param name="items"></param>
		/// <returns><c>true</c> if all items does have a value otherwise <c>false</c></returns>
		public static bool AllHasValue(params object[] items)
		{
			return items.All(item => item != null);
		}

		/// <summary>
		/// Checks for all items are null.
		/// </summary>
		/// <param name="items"></param>
		/// <returns><c>true</c> if all items are true otherwise <c>false</c></returns>
		public static bool AllAreNull(params object[] items)
		{
			return items.All(item => item == null);
		}

		public static bool AllAreNullOrZero<T>(params T?[] items) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new ArgumentException("DateTime not allowed.");

			return items.All(item => !item.HasValue || Convert.ToInt64(item.Value) == 0);
		}

		/// <summary>
		/// Checks for one or more items are null.
		/// </summary>
		/// <param name="items"></param>
		/// <returns><c>true</c> if one or more items are null otherwise <c>false</c></returns>
		public static bool AnyIsNull(params object[] items)
		{
			return items.Any(item => item == null);
		}

		public static bool AnyIsNullOrZero<T>(params T?[] items) where T : struct
		{
			if (typeof(T) == typeof(DateTime))
				throw new ArgumentException("DateTime not allowed.");

			return items.Any(item => !item.HasValue || Convert.ToInt64(item.Value) == 0);
		}


		#region TrueFalse value

		public static bool HasValue(this object value)
		{
			return value != null;
		}

		/// <summary>
		/// Checks if value has value. If so the trueValue will returned otherwise null
		/// </summary>
		/// <param name="value">the value.</param>
		/// <param name="trueValue">true value</param>
		/// <returns>trueValue if <c>true</c> otherwise null</returns>
		public static string HasValue(this object value, string trueValue)
		{
			return value != null ? trueValue : null;
		}

		/// <summary>
		/// Checks if value has value. If so the trueValue will returned otherwise the nullValue
		/// </summary>
		/// <param name="value">the value.</param>
		/// <param name="trueValue">true value</param>
		/// <param name="nullValue">null value</param>
		/// <returns>trueValue if <c>true</c> otherwise nullValue</returns>
		public static string HasValue(this object value, string trueValue, string nullValue)
		{
			return value != null ? trueValue : nullValue;
		}

		/// <summary>
		/// Checks if value has value. If so the trueValue will returned otherwise null
		/// </summary>
		/// <param name="value">the value.</param>
		/// <param name="trueValue">true value</param>
		/// <returns>trueValue if <c>true</c> otherwise null</returns>
		public static object HasValue(this object value, object trueValue)
		{
			return value != null ? trueValue : null;
		}

		/// <summary>
		/// Checks if value has value. If so the trueValue will returned otherwise the nullValue
		/// </summary>
		/// <param name="value">the value.</param>
		/// <param name="trueValue">true value</param>
		/// <param name="nullValue">null value</param>
		/// <returns>trueValue if <c>true</c> otherwise nullValue</returns>
		public static object HasValue(this object value, object trueValue, object nullValue)
		{
			return value != null ? trueValue : nullValue;
		}


		/// <summary>
		/// Checks if value has value. If so the trueValue will returned otherwise null
		/// </summary>
		/// <param name="value">the value.</param>
		/// <param name="trueValue">true value</param>
		/// <returns>trueValue if <c>true</c> otherwise null</returns>
		public static T? HasValue<T>(this object value, T? trueValue)
			where T : struct
		{
			return value != null ? trueValue : null;
		}

		/// <summary>
		/// Checks if value has value. If so the trueValue will returned otherwise the nullValue
		/// </summary>
		/// <param name="value">the value.</param>
		/// <param name="trueValue">true value</param>
		/// <param name="nullValue">null value</param>
		/// <returns>trueValue if <c>true</c> otherwise nullValue</returns>
		public static T? HasValue<T>(this object value, T? trueValue, T? nullValue)
			where T : struct
		{
			return value != null ? trueValue : nullValue;
		}

		/// <summary>
		/// Checks if value has value. If so the trueValue will returned otherwise null
		/// </summary>
		/// <param name="value">the value.</param>
		/// <param name="trueValue">true value</param>
		/// <returns>trueValue if <c>true</c> otherwise null</returns>
		public static T TrueFalseValue<T>(this object value, T trueValue)
			where T : struct
		{
			return value != null ? trueValue : default(T);
		}

		/// <summary>
		/// Checks if value has value. If so the trueValue will returned otherwise the nullValue
		/// </summary>
		/// <param name="value">the value.</param>
		/// <param name="trueValue">true value</param>
		/// <param name="nullValue">null value</param>
		/// <returns>trueValue if <c>true</c> otherwise nullValue</returns>
		public static T TrueFalseValue<T>(this object value, T trueValue, T falseValue)
			where T : struct
		{
			return value != null ? trueValue : falseValue;
		}

		#endregion

	}

}

public class ReferenceEqualityComparer : EqualityComparer<object>
{
	public override bool Equals(object x, object y)
	{
		return ReferenceEquals(x, y);
	}

	public override int GetHashCode(object obj)
	{
		return obj == null ? 0 : obj.GetHashCode();
	}
}