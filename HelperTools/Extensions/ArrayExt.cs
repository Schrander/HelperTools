using System;
using System.Collections.Generic;

namespace HelperTools.Extensions
{

	public static class ArrayExtension
	{
		public static void ForEach(this Array array, Action<Array, int[]> action)
		{
			if (array.LongLength == 0)
				return;

			ArrayTraverse walker = new ArrayTraverse(array);
			do action(array, walker.Position);
			while (walker.Step());
		}


		public static T[] RemoveAt<T>(this T[] sourceArray, int index)
		{
			try
			{
				if (index > sourceArray.Length - 1)
					throw new IndexOutOfRangeException();

				T[] destArray = new T[sourceArray.Length - 1];

				if (index > 0)
					Array.Copy(sourceArray, 0, destArray, 0, index);

				if (index < sourceArray.Length - 1)
					Array.Copy(sourceArray, index + 1, destArray, index, sourceArray.Length - index - 1);

				return destArray;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		public static bool Equals<T>(this T[] a1, T[] a2)
		{
			if (ReferenceEquals(a1, a2))
				return true;

			if (a1 == null || a2 == null)
				return false;

			if (a1.Length != a2.Length)
				return false;

			EqualityComparer<T> comparer = EqualityComparer<T>.Default;
			for (int i = 0; i < a1.Length; i++)
			{
				if (!comparer.Equals(a1[i], a2[i]))
					return false;
			}

			return true;
		}

	}

	internal class ArrayTraverse
	{
		public int[] Position;
		private int[] maxLengths;

		public ArrayTraverse(Array array)
		{
			maxLengths = new int[array.Rank];
			for (int i = 0; i < array.Rank; ++i)
				maxLengths[i] = array.GetLength(i) - 1;

			Position = new int[array.Rank];
		}

		public bool Step()
		{
			for (int i = 0; i < Position.Length; ++i)
			{
				if (Position[i] < maxLengths[i])
				{
					Position[i]++;
					for (int j = 0; j < i; j++)
						Position[j] = 0;

					return true;
				}
			}
			return false;
		}
	}
}

