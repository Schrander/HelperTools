using System.Collections.Generic;

namespace HelperTools.Helpers
{

	public static class ListHelper
	{
		public static List<List<T>> ListSplitter<T>(this List<T> list, int numberItems)
		{
			int i;
			int j;
			List<List<T>> output = new List<List<T>>();
			List<T> l = new List<T>();

			for (i = 0; i < list.Count; i++)
			{
				for (j = 0; j <= numberItems; j++)
				{
					if (i >= list.Count)
						continue;

					l.Add(list[i]);
					i++;

					if (j != numberItems - 1)
						continue;

					output.Add(l);
					j = -1;
					l = new List<T>();
				}
				output.Add(l);
			}
			return output;
		}
	}
}