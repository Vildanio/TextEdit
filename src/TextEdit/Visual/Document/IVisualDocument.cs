namespace TextEdit.Visual
{
	public interface IVisualDocument : IList<IVisual>
	{
		#region Search

		#region Contains

		/// <summary>
		/// Determines whether the collection contains a specific visual.
		/// </summary>
		/// <param name="value">The visual to locate.</param>
		/// <returns>True if the visual is found; otherwise, false.</returns>
		public bool Contains(ReadOnlySpan<IVisual> value)
		{
			return IndexOf(value, 0) >= 0;
		}

		#endregion

		#region IndexOf

		/// <summary>
		/// Searches for the first occurrence of a visual within the collection, starting from a specified index.
		/// </summary>
		/// <param name="value">The visual to locate.</param>
		/// <param name="startIndex">The index to start the search from.</param>
		/// <returns>The index of the first occurrence of the visual, or -1 if not found.</returns>
		public int IndexOf(IVisual value, int startIndex);

		/// <summary>
		/// Searches for the first occurrence of a visual within the collection, starting from a specified index.
		/// </summary>
		/// <param name="value">The visual to locate.</param>
		/// <param name="startIndex">The index to start the search from.</param>
		/// <returns>The index of the first occurrence of the visual, or -1 if not found.</returns>
		public int IndexOf(ReadOnlySpan<IVisual> value, int startIndex);

		#endregion

		#region LastIndexOf

		/// <summary>
		/// Searches for the last occurrence of a visual within the collection, up to a specified index.
		/// </summary>
		/// <param name="value">The visual to locate.</param>
		/// <param name="startIndex">The index to limit the search to.</param>
		/// <returns>The index of the last occurrence of the visual, or -1 if not found.</returns>
		public int LastIndexOf(IVisual value, int startIndex);

		/// <summary>
		/// Searches for the last occurrence of a visual within the collection, up to a specified index.
		/// </summary>
		/// <param name="value">The visual to locate.</param>
		/// <param name="startIndex">The index to limit the search to.</param>
		/// <returns>The index of the last occurrence of the visual, or -1 if not found.</returns>
		public int LastIndexOf(ReadOnlySpan<IVisual> value, int startIndex);

		#endregion

		#region IndexOfAny

		public int IndexOfAny(IVisual value0, IVisual value1, int startIndex);

		public int IndexOfAny(IVisual value0, IVisual value1, IVisual value2, int startIndex);

		public int IndexOfAny(IEnumerable<IVisual> items, int startIndex);

		public int IndexOfAny(ReadOnlySpan<IVisual> items, int startIndex);

		#endregion

		#region LastIndexOfAny

		public int LastIndexOfAny(IVisual value0, IVisual value1, int startIndex);

		public int LastIndexOfAny(IVisual value0, IVisual value1, IVisual value2, int startIndex);

		public int LastIndexOfAny(IEnumerable<IVisual> items, int startIndex);

		public int LastIndexOfAny(ReadOnlySpan<IVisual> items, int startIndex);

		#endregion

		#endregion

		#region Edit

		public void RemoveRange(int index, int count);

		public void InsertSpan(int index, ReadOnlySpan<IVisual> span);

		public void InsertRange(int index, IEnumerable<IVisual> enumerable);

		#endregion

		#region Copy

		/// <summary>
		/// Copies a specified range of visuals to a target span.
		/// </summary>
		/// <param name="index">The starting index of the range.</param>
		/// <param name="count">The number of visuals to copy.</param>
		/// <param name="span">The span to copy the visuals to.</param>
		public void CopyTo(int index, int count, Span<IVisual> span);

		/// <summary>
		/// Copies a specified range of visuals to a target IVisual array, starting at the destination index.
		/// </summary>
		/// <param name="sourceIndex">The starting index of the source range.</param>
		/// <param name="destination">The destination IVisual array.</param>
		/// <param name="destinationIndex">The starting index within the destination array.</param>
		/// <param name="count">The number of visuals to copy.</param>
		public void CopyTo(int sourceIndex, IVisual[] destination, int destinationIndex, int count);

		#endregion

		#region Conversion

		/// <summary>
		/// Converts a range of visuals into a string.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of visuals in the range.</param>
		/// <returns>A string containing the specified visual range.</returns>
		public string AsString(int start, int count);

		/// <summary>
		/// Converts a range of visuals into a read-only span.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of visuals in the range.</param>
		/// <returns>A read-only span containing the specified visual range.</returns>
		public ReadOnlySpan<IVisual> AsSpan(int start, int count);

		/// <summary>
		/// Converts a range of visuals into a read-only memory.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of visuals in the range.</param>
		/// <returns>A read-only memory containing the specified visual range.</returns>
		public ReadOnlyMemory<IVisual> AsMemory(int start, int count);

		#endregion

		#region Events

		/// <summary>
		/// Occured when visual was removed
		/// </summary>
		public event EventHandler<IVisualRemovedEventArgs>? VisualRemoved;

		/// <summary>
		/// Occured when visual was inserted
		/// </summary>
		public event EventHandler<IVisualInsertedEventArgs>? VisualInserted;

		/// <summary>
		/// Occured when some visual was replaced with another
		/// </summary>
		public event EventHandler<IVisualReplacedEventArgs>? VisualReplaced;

		/// <summary>
		/// Occured when range of visuals was removed
		/// </summary>
		public event EventHandler<IVisualRangeRemovedEventArgs>? VisualRangeRemoved;

		/// <summary>
		/// Occured when range of visuals was inserted
		/// </summary>
		public event EventHandler<IVisualRangeInsertedEventArgs>? VisualRangeInserted;

		#endregion
	}
}