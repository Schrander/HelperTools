using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace HelperTools.Web
{
	public static class ListItemExt
	{
		public static ListItem[] ToListItems(this Enum enumProperty)
		{
			List<ListItem> list = new List<ListItem>();
			foreach (Enum item in Enum.GetValues(enumProperty.GetType()))
				list.Add(new ListItem(item.GetDescription(), item.ToInt32().ToString()));

			return list.ToArray();
		}
	}
}

