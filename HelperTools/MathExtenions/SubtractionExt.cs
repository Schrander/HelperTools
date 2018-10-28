using System.Linq;
using HelperTools.Helpers;

namespace HelperTools
{
	public static class SubtractionExt
	{


		#region Subtraction

		public static long? Subtraction(params long?[] items)
		{
			if (items.All(i => !i.HasValue))
				return null;

			long? result = null;

			foreach (long? item in items)
			{
				// Eerste item wordt positief benaderd (opgeteld als de waarde van result nog null is)
				if (!result.HasValue)
					result = item.NotNullable();
				// Opvolgende items worden afgetrokken van het eerste item...
				else
					result -= item.NotNullable();
			}

			return result;
		}

		public static double? Subtraction(params double?[] items)
		{
			if (items.All(i => !i.HasValue))
				return null;

			double? value = null;

			foreach (double? item in items)
			{
				if (!value.HasValue)
					value = item.NotNullable();
				else
					value -= item.NotNullable();
			}
			return value;
		}

		public static decimal? Subtraction(params decimal?[] items)
		{
			if (items.All(i => !i.HasValue))
				return null;

			decimal? value = null;

			foreach (decimal? item in items)
			{
				if (!value.HasValue)
					value = item.NotNullable();
				else
					value -= item.NotNullable();
			}
			return value;
		}

		public static int? Subtraction(params int?[] items)
		{
			if (items.All(i => !i.HasValue))
				return null;

			int? value = null;

			foreach (int? item in items)
			{
				if (!value.HasValue)
					value = item.NotNullable();
				else
					value -= item.NotNullable();
			}
			return value;
		}
		#endregion

	}
}