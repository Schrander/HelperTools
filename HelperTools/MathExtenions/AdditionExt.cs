using System.Linq;

namespace HelperTools
{
	public static partial class MathExt
	{

		#region Addition

		public static long? Addition(params long?[] items)
		{
			if (items.All(i => !i.HasValue))
				return null;

			return items.Sum(i => i ?? 0);
		}

		public static double? Addition(params double?[] items)
		{
			if (items.All(i => !i.HasValue))
				return null;

			return items.Sum(i => i ?? 0);
		}

		public static decimal? Addition(params decimal?[] items)
		{
			if (items.All(i => !i.HasValue))
				return null;

			return items.Sum(i => i ?? 0);
		}

		public static int? Addition(params int?[] items)
		{
			if (items.All(i => !i.HasValue))
				return null;

			return items.Sum(i => i ?? 0);
		}

		#endregion

	}
}