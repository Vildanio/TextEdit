namespace TextEdit.Line
{
	/// <summary>
	/// Represents a collection of text lines that provides notifications of changing
	/// </summary>
	public interface ILineDocument : IList<string>
	{
		#region Search

		#region Contains

		/// <summary>
		/// Determines whether the collection contains a specific line.
		/// </summary>
		/// <param name="value">The line to locate.</param>
		/// <returns>True if the line is found; otherwise, false.</returns>
		public bool Contains(ReadOnlySpan<string> value)
		{
			return IndexOf(value, 0) >= 0;
		}

		#endregion

		#region IndexOf

		/// <summary>
		/// Searches for the first occurrence of a line within the collection, starting from a specified index.
		/// </summary>
		/// <param name="value">The line to locate.</param>
		/// <param name="startIndex">The index to start the search from.</param>
		/// <returns>The index of the first occurrence of the line, or -1 if not found.</returns>
		public int IndexOf(string value, int startIndex);

		/// <summary>
		/// Searches for the first occurrence of a line within the collection, starting from a specified index.
		/// </summary>
		/// <param name="value">The line to locate.</param>
		/// <param name="startIndex">The index to start the search from.</param>
		/// <returns>The index of the first occurrence of the line, or -1 if not found.</returns>
		public int IndexOf(ReadOnlySpan<string> value, int startIndex);

		#endregion

		#region LastIndexOf

		/// <summary>
		/// Searches for the last occurrence of a line within the collection, up to a specified index.
		/// </summary>
		/// <param name="value">The line to locate.</param>
		/// <param name="startIndex">The index to limit the search to.</param>
		/// <returns>The index of the last occurrence of the line, or -1 if not found.</returns>
		public int LastIndexOf(string value, int startIndex);

		/// <summary>
		/// Searches for the last occurrence of a string within the collection, up to a specified index.
		/// </summary>
		/// <param name="value">The string to locate.</param>
		/// <param name="startIndex">The index to limit the search to.</param>
		/// <returns>The index of the last occurrence of the string, or -1 if not found.</returns>
		public int LastIndexOf(ReadOnlySpan<string> value, int startIndex);

		#endregion

		#region IndexOfAny

		public int IndexOfAny(string value0, string value1, int startIndex);

		public int IndexOfAny(string value0, string value1, string value2, int startIndex);

		public int IndexOfAny(IEnumerable<string> items, int startIndex);

		public int IndexOfAny(ReadOnlySpan<string> items, int startIndex);

		#endregion

		#region LastIndexOfAny

		public int LastIndexOfAny(string value0, string value1, int startIndex);

		public int LastIndexOfAny(string value0, string value1, string value2, int startIndex);

		public int LastIndexOfAny(IEnumerable<string> items, int startIndex);

		public int LastIndexOfAny(ReadOnlySpan<string> items, int startIndex);

		#endregion

		#endregion

		#region Edit

		public void RemoveRange(int index, int count);

		public void InsertSpan(int index, ReadOnlySpan<string> span);

		public void InsertRange(int index, IEnumerable<string> enumerable);

		#endregion

		#region Copy

		/// <summary>
		/// Copies a specified range of lines to a target span.
		/// </summary>
		/// <param name="index">The starting index of the range.</param>
		/// <param name="count">The number of lines to copy.</param>
		/// <param name="span">The span to copy the lines to.</param>
		public void CopyTo(int index, int count, Span<string> span);

		/// <summary>
		/// Copies a specified range of lines to a target string array, starting at the destination index.
		/// </summary>
		/// <param name="sourceIndex">The starting index of the source range.</param>
		/// <param name="destination">The destination string array.</param>
		/// <param name="destinationIndex">The starting index within the destination array.</param>
		/// <param name="count">The number of lines to copy.</param>
		public void CopyTo(int sourceIndex, string[] destination, int destinationIndex, int count);

		#endregion

		#region Conversion

		/// <summary>
		/// Converts a range of lines into a string.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of lines in the range.</param>
		/// <returns>A string containing the specified line range.</returns>
		public string AsString(int start, int count);

		/// <summary>
		/// Converts a range of lines into a read-only span.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of lines in the range.</param>
		/// <returns>A read-only span containing the specified line range.</returns>
		public ReadOnlySpan<string> AsSpan(int start, int count);

		/// <summary>
		/// Converts a range of lines into a read-only memory.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of lines in the range.</param>
		/// <returns>A read-only memory containing the specified line range.</returns>
		public ReadOnlyMemory<string> AsMemory(int start, int count);

		#endregion

		#region Events

		/// <summary>
		/// Occurs when line was removed
		/// </summary>
		public event EventHandler<ILineRemovedEventArgs>? LineRemoved;

		/// <summary>
		/// Occurs when line was replaced
		/// </summary>
		public event EventHandler<ILineReplacedEventArgs>? LineReplaced;

		/// <summary>
		/// Occurs when line was inserted
		/// </summary>
		public event EventHandler<ILineInsertedEventArgs>? LineInserted;

		/// <summary>
		/// Occurs when range of lines was removed
		/// </summary>
		public event EventHandler<ILineRangeRemovedEventArgs>? LineRangeRemoved;

		/// <summary>
		/// Occurs when range of lines was inserted
		/// </summary>
		public event EventHandler<ILineRangeInsertedEventArgs>? LineRangeInserted;

		#endregion
	}
}