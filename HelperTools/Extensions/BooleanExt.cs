using System.Linq;

namespace HelperTools
{
	public static class BooleanExt
	{
		/// <summary>
		/// Converts a boolean to 1 or 0.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static int ToInt32(this bool item)
		{
			return item ? 1 : 0;
		}

		/// <summary>
		/// Converts a boolean to 1 or 0.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static int ToInt32(this bool? item)
		{
			return item.HasValue ? ToInt32(item.Value) : 0;
		}

		/// <summary>
		/// Als eentje van de collectie waar is dan is het geheel waar.
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		public static bool Any(params bool[] items)
		{
			return items.Any(i => i);
		}

		/// <summary>
		/// Als eentje van de collectie waar is dan is het geheel waar.
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		public static bool Any(params bool?[] items)
		{
			return items.Any(i => i ?? false);
		}

		/// <summary>
		/// Alle opgegeven values moeten waar zijn voordat de geheel waar is.
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		public static bool AllAreTrue(params bool[] items)
		{
			return items.All(i => i);
		}

		/// <summary>
		/// Alle opgegeven values moeten waar zijn voordat de geheel waar is.
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		public static bool AllAreTrue(params bool?[] items)
		{
			return items.All(i => i ?? false);
		}

		public static bool AllAreNullOrFalse(params bool?[] items)
		{
			return items.All(i => !i.HasValue || !i.Value);
		}

		/// <summary>
		/// Checks for the value HasValue and is true.
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns><c>true</c> if value has value and is true otherwise <c>false</c></returns>
		public static bool IsTrue(this bool? value)
		{
			return value.HasValue && value.Value;
		}

		/// <summary>
		/// Checks for the value HasValue and is false or has not a value.
		/// </summary>
		/// <param name="value">the value</param>
		/// <returns><c>true</c> if value has value and is false otherwise <c>false</c></returns>
		public static bool IsFalse(this bool? value)
		{
			return value.HasValue && !value.Value || !value.HasValue;
		}

	}
}