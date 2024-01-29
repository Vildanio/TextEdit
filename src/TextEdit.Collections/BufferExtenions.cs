using System.Runtime.CompilerServices;

namespace TextEdit.Collections
{
	public static class BufferExtenions
	{
		#region IBuffer

		public static void ReplaceRange<T>(this Buffer<T> buffer, int index, IEnumerable<T> enumerable)
		{
			buffer.RemoveRange(index, enumerable.Count());
			buffer.InsertRange(index, enumerable);
		}

		public static void ReplaceSpan<T>(this Buffer<T> buffer, int index, ReadOnlySpan<T> span)
		{
			buffer.RemoveRange(index, span.Length);
			buffer.InsertSpan(index, span);
		}

		#endregion

		#region IReadOnlyBuffer

		public static void CopyTo<T>(this IReadOnlyBuffer<T> buffer, T[] array, int arrayIndex)
		{
			ThrowHelper.ThrowIfNull(buffer);

			buffer.CopyTo(0, array, arrayIndex, array.Length - arrayIndex);
		}

		public static T[] ToArray<T>(this IReadOnlyBuffer<T> buffer, int start, int count)
		{
			ThrowHelper.ThrowIfNull(buffer);

			T[] array = new T[count];

			buffer.CopyTo(start, array, 0, count);

			return array;
		}

		public static Buffer<T> AsBuffer<T>(this T[] array)
		{
			return new ListBuffer<T>(array);
		}

		public static bool IsEmpty<T>(this IReadOnlyList<T> buffer)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.Count <= 0;
		}

		#region AsSpan

		public static ReadOnlySpan<T> AsSpan<T>(this IReadOnlyBuffer<T> buffer)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.AsSpan(0, buffer.Count);
		}

		public static ReadOnlySpan<T> AsSpan<T>(this IReadOnlyBuffer<T> buffer, int start)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.AsSpan(start, buffer.Count - start);
		}

		#endregion

		#region AsMemory

		public static ReadOnlyMemory<T> AsMemory<T>(this IReadOnlyBuffer<T> buffer)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.AsMemory(0, buffer.Count);
		}

		public static ReadOnlyMemory<T> AsMemory<T>(this IReadOnlyBuffer<T> buffer, int start)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.AsMemory(start, buffer.Count - start);
		}

		#endregion

		#region IndexOf

		public static int IndexOf<T>(this IReadOnlyBuffer<T> buffer, T value)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.IndexOf(value, 0);
		}

		public static int LastIndexOf<T>(this IReadOnlyBuffer<T> buffer, T value)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.LastIndexOf(value, buffer.Count - 1);
		}

		public static int IndexOf<T>(this IReadOnlyBuffer<T> buffer, ReadOnlySpan<T> value)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.IndexOf(value, 0);
		}

		public static int LastIndexOf<T>(this IReadOnlyBuffer<T> buffer, ReadOnlySpan<T> value)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.LastIndexOf(value, buffer.Count - 1);
		}

		#endregion

		#region Validity

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void CheckValid<T>(this IReadOnlyBuffer<T> buffer, int offset)
		{
			if (!buffer.IsValid(offset))
				throw new ArgumentOutOfRangeException(nameof(offset));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsValid<T>(this IReadOnlyBuffer<T> buffer, int offset)
		{
			return offset >= 0 && offset < buffer.Count;
		}

		#endregion

		#endregion

		#region GapBuffer

		public static string ToString(this GapBuffer<char> buffer, int start, int count)
		{
			ThrowHelper.ThrowIfNull(buffer);

			return buffer.AsSpan(start, count).ToString();
		}

		#endregion
	}
}