using TextEdit.Collections;

namespace TextEdit.Utils
{
	internal static class Extensions
	{
		#region EnumerateLines

		#region EnumerateLineOffsets

		public static IEnumerable<int> EnumerateLineOffsets(this IReadOnlyBuffer<char> text)
		{
			return text.EnumerateLineOffsets(0);
		}

		public static IEnumerable<int> EnumerateLineOffsets(this IReadOnlyBuffer<char> text, int offset)
		{
			return text.EnumerateLineOffsets(offset, text.Count - offset);
		}

		public static IEnumerable<int> EnumerateLineOffsets(this IReadOnlyBuffer<char> text, int offset, int count)
		{
			TextLineEnumerator enumerator = GetTextLineEnumerator(text, offset, count);

			while (enumerator.MoveNext())
			{
				yield return enumerator.LineEnd;
			}
		}

		#endregion

		#region EnumerateLineLengths

		public static IEnumerable<int> EnumerateLineLengths(this IReadOnlyBuffer<char> text)
		{
			return text.EnumerateLineLengths(0);
		}

		public static IEnumerable<int> EnumerateLineLengths(this IReadOnlyBuffer<char> text, int offset)
		{
			return text.EnumerateLineLengths(offset, text.Count - offset);
		}

		public static IEnumerable<int> EnumerateLineLengths(this IReadOnlyBuffer<char> text, int offset, int count)
		{
			TextLineEnumerator enumerator = GetTextLineEnumerator(text, offset, count);

			while (enumerator.MoveNext())
			{
				yield return enumerator.LineLength;
			}
		}

		#endregion

		#region TextLineEnumerator

		public static TextLineEnumerator GetTextLineEnumerator(this IReadOnlyBuffer<char> text)
		{
			ThrowHelper.ThrowIfNull(text);

			return new TextLineEnumerator(text, 0, text.Count);
		}

		public static TextLineEnumerator GetTextLineEnumerator(this IReadOnlyBuffer<char> text, int offset)
		{
			ThrowHelper.ThrowIfNull(text);

			if (offset < 0 || offset >= text.Count)
				throw new ArgumentOutOfRangeException(nameof(offset));

			return new TextLineEnumerator(text, offset, text.Count - offset);
		}

		public static TextLineEnumerator GetTextLineEnumerator(this IReadOnlyBuffer<char> text, int offset, int count)
		{
			ThrowHelper.ThrowIfNull(text);
			ThrowHelper.ThrowIfOutOfRange(offset, count, text.Count, maxCountParamName: null);

			return new TextLineEnumerator(text, offset, count);
		}

		#endregion

		#endregion
	}
}
