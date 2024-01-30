using System.Diagnostics;
using System.Runtime.CompilerServices;
using TextEdit.Collections;
using TextEdit.Text;

namespace TextEdit.Utils
{
	public static class CollectionExtensions
	{
		public static IBuffer<char> AsBuffer(this ITextDocument textDocument)
		{
			return new TextDocumentBuffer(textDocument);
		}

		/// <summary>
		/// Returns the given <paramref name="enumerator"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerator"></param>
		/// <returns>To use enumerators in foreach statement</returns>
		public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator)
		{
			return enumerator;
		}

		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}

		#region BinarySearch

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetMedian(int low, int hi)
		{
			Debug.Assert(low <= hi);
			Debug.Assert(hi - low >= 0, "Length overflow!");

			return low + (hi - low >> 1);
		}

		/// <summary>
		/// Performs a binary search on the entire contents of an IReadOnlyList
		/// </summary>
		/// <typeparam name="T">The list element type</typeparam>
		/// <param name="list">The list to be searched</param>
		/// <param name="value">The value to search for</param>
		/// <param name="comparer">The comparer</param>
		/// <returns>The index of the found item; otherwise the bitwise complement of the index of the next larger item</returns>
		public static int BinarySearch<T>(this IReadOnlyList<T> list, T value, IComparer<T> comparer)
		{
			return list.BinarySearch(0, list.Count, value, comparer);
		}

		/// <summary>
		/// Performs a binary search on a a subset of an IReadOnlyList
		/// </summary>
		/// <typeparam name="T">The list element type</typeparam>
		/// <param name="list">The list to be searched</param>
		/// <param name="index">The start of the range to be searched</param>
		/// <param name="length">The length of the range to be searched</param>
		/// <param name="value">The value to search for</param>
		/// <param name="comparer">A comparer</param>
		/// <returns>The index of the found item; otherwise the bitwise complement of the index of the next larger item</returns>
		public static int BinarySearch<T>(this IReadOnlyList<T> list, int index, int length, T value, IComparer<T> comparer)
		{
			// Based on this: https://referencesource.microsoft.com/#mscorlib/system/array.cs,957
			var lo = index;
			var hi = index + length - 1;

			while (lo <= hi)
			{
				var i = GetMedian(lo, hi);

				var c = comparer.Compare(list[i], value);

				if (c == 0)
					return i;

				if (c < 0)
				{
					lo = i + 1;
				}
				else
				{
					hi = i - 1;
				}
			}
			return ~lo;
		}

		#endregion
	}
}