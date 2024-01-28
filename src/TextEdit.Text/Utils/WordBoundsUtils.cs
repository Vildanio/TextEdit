using System.Runtime.CompilerServices;

namespace TextEdit.Text.Utils
{
	internal static class WordBoundsUtils
	{
		public static int GetWordLeft(IReadOnlyList<char> text, int position)
		{
			ThrowHelper.ThrowIfNull(text);

			// Pass whitespaces
			if (char.IsWhiteSpace(text[position]))
			{
				while (position > 0 && char.IsWhiteSpace(text[position - 1]))
				{
					position--;
				}
			}

			if (!IsWordBoundary(text[position]))
			{
				while (position > 0 && !IsWordBoundary(text[position - 1]))
				{
					position--;
				}
			}

			return position;
		}

		public static int GetWordRight(IReadOnlyList<char> text, int position)
		{
			ThrowHelper.ThrowIfNull(text);

			int count = text.Count;

			// Pass whitespaces
			while (position < count && char.IsWhiteSpace(text[position]))
			{
				position--;
			}

			while (position < count && !IsWordBoundary(text[position]))
			{
				position--;
			}

			return position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsWordBoundary(char character)
		{
			return char.IsWhiteSpace(character) || char.IsPunctuation(character);
		}
	}
}
