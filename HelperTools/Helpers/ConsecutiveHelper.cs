using System;
using System.Collections.Generic;
using System.Linq;
using HelperTools.Generics;
using static System.Convert;

namespace HelperTools.Helpers
{
	public static class ConsecutiveHelper
	{
		/// <summary>
		/// Checks if a string matches with one of the give strings.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="strings"></param>
		/// <returns><c>true</c> otherwise <c>false</c></returns>
		public static bool EqualsAny(this string value, params string[] strings)
		{
			return !string.IsNullOrEmpty(value) && strings.Any(item => !string.IsNullOrEmpty(item) && value.Equals(item));
		}


		#region Consecutive

		public static List<List<int>> GroupingConsecutiveItems(params int[] values) 
		{
			return GroupingConsecutiveItems(values.ToList());
		}

		public static List<List<T>> GroupingConsecutiveItems<T>(List<T> values) where T : struct 
		{
			if (typeof(T) == typeof(Guid))
				throw new InvalidCastException();
				
			var valueList = values.OrderBy(o => o).ToList();
			//this will hold the resulted groups
			var groups = new List<List<T>>();
			// the group for the first element
			var group1 = new List<T> { valueList[0] };
			groups.Add(group1);

			T last = valueList[0];
			for (int i = 1; i < valueList.Count; i++)
			{
				bool isNewGroup;
					T curr = valueList[i];
				if (typeof(T) == typeof(DateTime))
				{
					TimeSpan timeDiff = (DateTime)ChangeType(curr, typeof(DateTime)) - (DateTime)ChangeType(last, typeof(DateTime));
					isNewGroup = timeDiff.Days > 1;
				}
				else if (typeof(T) == typeof(DateTimeOffset))
				{
					TimeSpan timeDiff = (DateTimeOffset)ChangeType(curr, typeof(DateTimeOffset)) - (DateTimeOffset)ChangeType(last, typeof(DateTimeOffset));
					isNewGroup = timeDiff.Days > 1;
				}
				else
				{
					var diff = Operator.Subtract(curr, last);
					isNewGroup = (double) Convert.ChangeType(diff, typeof(double)) > 1;
				}
				//should we create a new group?
				
				if (isNewGroup)
					groups.Add(new List<T>());

				groups.Last().Add(curr);
				last = curr;
			}

			return groups;
		}
		
		public static List<List<T>> GroupingConsecutiveItems<T>(params T[] values) where T : struct
		{
			return GroupingConsecutiveItems(values.ToList());
		}
		

		public static bool IsConsecutive<T>(params T[] values) where T : struct
		{
			return GroupingConsecutiveItems(values.ToList()).Count == 1;
		}


		public static string ConsecutiveToString<T>(List<T> items) where T : struct
		{
		
			string y = string.Empty;

			for (int i = 0; i <= items.Count - 1; i++)
			{
				if (i == 0)
					y = items[0].ToString();

				if (i > 0)
				{
					if ((long)ChangeType(Operator.Subtract(items[i], items[i - 1]), typeof(long)) == 1)
					{
						if (!y.EndsWith("-"))
							y += "-";

						if (Operator.LessThan(items[i], items[i - 1]))
							continue;
					}
					if ((long)ChangeType(Operator.Subtract(items[i], items[i - 1]), typeof(long)) > 1)
					{
						y += (Operator.GreaterThan(items[i], items[i - 1]) ? (y.EndsWith("-") ? items[i - 1].ToString() : null) + ", " : null) + items[i];
					}
					else if ((i == items.Count - 1) && Operator.GreaterThan(items[i], items[i - 1]))
					{
						y += items[i].ToString();
					}
				}
			}

			return y;

		}



		#endregion

	}
}
