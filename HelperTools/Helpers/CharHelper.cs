using System.Collections.Generic;

namespace HelperTools.Helpers
{
	public static class CharHelper
	{

		public static List<char> AlfabetList
		{
			get
			{
				List<char> list = new List<char> {'#'};

				for (char c = 'A'; c <= 'Z'; c++)
					list.Add(c);

				return list;
			}
		}

	}
}