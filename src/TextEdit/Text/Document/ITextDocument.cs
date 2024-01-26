namespace TextEdit.Text
{
	/// <summary>
	/// Represents a collection of <see cref="char"/> that provides notifications of changing
	/// </summary>
	public interface ITextDocument : IList<char>
	{
		#region Search

		#region Contains

		/// <summary>
		/// Determines whether the collection contains a specific character span.
		/// </summary>
		/// <param name="value">The character span to locate.</param>
		/// <returns>True if the character span is found; otherwise, false.</returns>
		public bool Contains(ReadOnlySpan<char> value)
		{
			return IndexOf(value, 0) >= 0;
		}

		#endregion

		#region IndexOf

		/// <summary>
		/// Searches for the first occurrence of a character within the collection, starting from a specified index.
		/// </summary>
		/// <param name="value">The character to locate.</param>
		/// <param name="startIndex">The index to start the search from.</param>
		/// <returns>The index of the first occurrence of the character, or -1 if not found.</returns>
		public int IndexOf(char value, int startIndex);

		/// <summary>
		/// Searches for the first occurrence of a character span within the collection, starting from a specified index.
		/// </summary>
		/// <param name="value">The character span to locate.</param>
		/// <param name="startIndex">The index to start the search from.</param>
		/// <returns>The index of the first occurrence of the character span, or -1 if not found.</returns>
		public int IndexOf(ReadOnlySpan<char> value, int startIndex);

		#endregion

		#region LastIndexOf

		/// <summary>
		/// Searches for the last occurrence of a character within the collection, up to a specified index.
		/// </summary>
		/// <param name="value">The character to locate.</param>
		/// <param name="startIndex">The index to limit the search to.</param>
		/// <returns>The index of the last occurrence of the character, or -1 if not found.</returns>
		public int LastIndexOf(char value, int startIndex);

		/// <summary>
		/// Searches for the last occurrence of a character span within the collection, up to a specified index.
		/// </summary>
		/// <param name="value">The character span to locate.</param>
		/// <param name="startIndex">The index to limit the search to.</param>
		/// <returns>The index of the last occurrence of the character span, or -1 if not found.</returns>
		public int LastIndexOf(ReadOnlySpan<char> value, int startIndex);

		#endregion

		#region IndexOfAny

		public int IndexOfAny(char value0, char value1, int startIndex);

		public int IndexOfAny(char value0, char value1, char value2, int startIndex);

		public int IndexOfAny(IEnumerable<char> items, int startIndex);

		public int IndexOfAny(ReadOnlySpan<char> items, int startIndex);

		#endregion

		#region LastIndexOfAny

		public int LastIndexOfAny(char value0, char value1, int startIndex);

		public int LastIndexOfAny(char value0, char value1, char value2, int startIndex);

		public int LastIndexOfAny(IEnumerable<char> items, int startIndex);

		public int LastIndexOfAny(ReadOnlySpan<char> items, int startIndex);

		#endregion

		#endregion

		#region Edit

		public void RemoveRange(int index, int count);

		public void InsertString(int index, string value);

		public void InsertSpan(int index, ReadOnlySpan<char> span);

		public void InsertRange(int index, IEnumerable<char> enumerable);

		#endregion

		#region Copy

		/// <summary>
		/// Copies a specified range of characters to a target span.
		/// </summary>
		/// <param name="index">The starting index of the range.</param>
		/// <param name="count">The number of characters to copy.</param>
		/// <param name="span">The span to copy the characters to.</param>
		public void CopyTo(int index, int count, Span<char> span);

		/// <summary>
		/// Copies a specified range of characters to a target char array, starting at the destination index.
		/// </summary>
		/// <param name="sourceIndex">The starting index of the source range.</param>
		/// <param name="destination">The destination char array.</param>
		/// <param name="destinationIndex">The starting index within the destination array.</param>
		/// <param name="count">The number of characters to copy.</param>
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);

		#endregion

		#region Conversion

		/// <summary>
		/// Converts a range of characters into a string.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of characters in the range.</param>
		/// <returns>A string containing the specified character range.</returns>
		public string AsString(int start, int count);

		/// <summary>
		/// Converts a range of characters into a read-only span.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of characters in the range.</param>
		/// <returns>A read-only span containing the specified character range.</returns>
		public ReadOnlySpan<char> AsSpan(int start, int count);

		/// <summary>
		/// Converts a range of characters into a read-only memory.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of characters in the range.</param>
		/// <returns>A read-only memory containing the specified character range.</returns>
		public ReadOnlyMemory<char> AsMemory(int start, int count);

		#endregion

		#region Events

		/// <summary>
		/// Occurs when char was removed
		/// </summary>
		public event EventHandler<ICharRemovedEventArgs>? CharRemoved;

		/// <summary>
		/// Occurs when char was replaced
		/// </summary>
		public event EventHandler<ICharReplacedEventArgs>? CharReplaced;

		/// <summary>
		/// Occurs when char was inserted
		/// </summary>
		public event EventHandler<ICharInsertedEventArgs>? CharInserted;

		/// <summary>
		/// Occurs when range of chars was removed
		/// </summary>
		public event EventHandler<ICharRangeRemovedEventArgs>? CharRangeRemoved;

		/// <summary>
		/// Occurs when range of chars was inserted
		/// </summary>
		public event EventHandler<ICharRangeInsertedEventArgs>? CharRangeInserted;

		#endregion
	}
}