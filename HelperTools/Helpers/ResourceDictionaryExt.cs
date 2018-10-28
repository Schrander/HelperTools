using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using static System.Convert;

namespace HelperTools.Helpers
{
	public static class ResourceDictionaryExt
	{
		public static Dictionary<T, TU> GetResourceItems<T, TU>(ResourceManager manager)
			where T : struct
			where TU : struct
		{
			return GetResourceItems<T, TU>(manager, 0);
		}

		public static Dictionary<T, TU> GetResourceItems<T, TU>(ResourceManager manager, int substring)
			where T : struct
			where TU : struct
		{
			Dictionary<T, TU> list = new Dictionary<T, TU>();

			foreach (DictionaryEntry item in manager.GetResourceSet(new CultureInfo("en-US"), true, true))
			{
				T key = (T)ChangeType(item.Key.ToString().Substring(substring), typeof(T));
				TU value = (TU)ChangeType(item.Value.ToString().Replace('.', ','), typeof(TU));

				list.Add(key, value);
			}
			return list;
		}
	}
}