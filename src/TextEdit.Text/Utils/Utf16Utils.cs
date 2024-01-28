namespace TextEdit.Text
{
	internal static class Utf16Utils
	{
		/// <summary>
		/// Determines if char at <paramref name="index"/> in <paramref name="chars"/> is part of surrogate pair or not
		/// </summary>
		/// <param name="chars"></param>
		/// <param name="index"></param>
		/// <returns>2 if the char is part of surrogate pair and 1 if it is not</returns>
		public static int GetCharLength(this IReadOnlyList<char> chars, int index)
		{
			char character = chars[index];

			if (char.IsLowSurrogate(character) && index + 1 < chars.Count)
			{
				if (char.IsHighSurrogate(chars[index + 1]))
				{
					return 2;
				}
			}
			else if(char.IsHighSurrogate(character) && index > 0)
			{
				if (char.IsLowSurrogate(chars[index - 1]))
				{
					return 2;
				}
			}

			return 1;
		}
	}
}
