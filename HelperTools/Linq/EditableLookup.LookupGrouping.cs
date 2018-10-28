using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HelperTools.Linq
{
	partial class EditableLookup<TKey, TElement>
	{
		internal sealed class LookupGrouping : IGrouping<TKey, TElement>
		{
			private List<TElement> items = new List<TElement>();
			public TKey Key { get; }

			public LookupGrouping(TKey key)
			{
				this.Key = key;
			}
			public int Count => items.Count;

			public void Add(TElement item)
			{
				items.Add(item);
			}

			public bool Contains(TElement item)
			{
				return items.Contains(item);
			}

			public bool Remove(TElement item)
			{
				return items.Remove(item);
			}

			public void TrimExcess()
			{
				items.TrimExcess();
			}

			public IEnumerator<TElement> GetEnumerator()
			{
				return items.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}
	}
}
