using System;

namespace HelperTools.Helpers
{
	/// <summary>
	/// Type of buffer returned by CachingBufferManager.
	/// </summary>
	class CachedBuffer : IBuffer
	{
		volatile bool available;
		readonly bool clearOnDispose;

		internal CachedBuffer(int size, bool clearOnDispose)
		{
			Bytes = new byte[size];
			this.clearOnDispose = clearOnDispose;
		}

		internal bool Available
		{
			get { return available; }
			set { available = value; }
		}

		public byte[] Bytes { get; }

		public void Dispose()
		{
			if (clearOnDispose)
				Array.Clear(Bytes, 0, Bytes.Length);

			available = true;
		}
	}
}
