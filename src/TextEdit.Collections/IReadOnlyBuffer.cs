namespace TextEdit.Collections
{
    /// <summary>
    /// Provides read-only access to potentially mutable collection of elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyBuffer<T> : IReadOnlyList<T>
    {
        public static IReadOnlyBuffer<T> Empty => ImmutableArrayBuffer<T>.Empty;

        /// <summary>
        /// Gets value indicating whether collection is immutable or not.
        /// </summary>
        public bool IsImmutable { get; }

        #region Search

        #region Contains

        /// <summary>
        /// Determines whether the collection contains a specific character.
        /// </summary>
        /// <param name="value">The character to locate.</param>
        /// <returns>True if the character is found; otherwise, false.</returns>
        public bool Contains(T value)
        {
            return IndexOf(value, 0) >= 0;
        }

        /// <summary>
        /// Determines whether the collection contains a specific string.
        /// </summary>
        /// <param name="value">The string to locate.</param>
        /// <returns>True if the string is found; otherwise, false.</returns>
        public bool Contains(ReadOnlySpan<T> value)
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
        public int IndexOf(T value, int startIndex);

        /// <summary>
        /// Searches for the first occurrence of a string within the collection, starting from a specified index.
        /// </summary>
        /// <param name="value">The string to locate.</param>
        /// <param name="startIndex">The index to start the search from.</param>
        /// <returns>The index of the first occurrence of the string, or -1 if not found.</returns>
        public int IndexOf(ReadOnlySpan<T> value, int startIndex);

        #endregion

        #region LastIndexOf

        /// <summary>
        /// Searches for the last occurrence of a character within the collection, up to a specified index.
        /// </summary>
        /// <param name="value">The character to locate.</param>
        /// <param name="startIndex">The index to limit the search to.</param>
        /// <returns>The index of the last occurrence of the character, or -1 if not found.</returns>
        public int LastIndexOf(T value, int startIndex);

        /// <summary>
        /// Searches for the last occurrence of a string within the collection, up to a specified index.
        /// </summary>
        /// <param name="value">The string to locate.</param>
        /// <param name="startIndex">The index to limit the search to.</param>
        /// <returns>The index of the last occurrence of the string, or -1 if not found.</returns>
        public int LastIndexOf(ReadOnlySpan<T> value, int startIndex);

		#endregion

		#region IndexOfAny

		public int IndexOfAny(T value0, T value1, int startIndex);

		public int IndexOfAny(T value0, T value1, T value2, int startIndex);

		public int IndexOfAny(IEnumerable<T> items, int startIndex);

		public int IndexOfAny(ReadOnlySpan<T> items, int startIndex);

		#endregion

		#region LastIndexOfAny

		public int LastIndexOfAny(T value0, T value1, int startIndex);

		public int LastIndexOfAny(T value0, T value1, T value2, int startIndex);

		public int LastIndexOfAny(IEnumerable<T> items, int startIndex);

		public int LastIndexOfAny(ReadOnlySpan<T> items, int startIndex);

		#endregion

		#endregion

		#region Copy

		/// <summary>
		/// Copies a specified range of characters to a target span.
		/// </summary>
		/// <param name="index">The starting index of the range.</param>
		/// <param name="count">The number of characters to copy.</param>
		/// <param name="span">The span to copy the characters to.</param>
		public void CopyTo(int index, int count, Span<T> span);

        /// <summary>
        /// Copies a specified range of characters to a target char array, starting at the destination index.
        /// </summary>
        /// <param name="sourceIndex">The starting index of the source range.</param>
        /// <param name="destination">The destination char array.</param>
        /// <param name="destinationIndex">The starting index within the destination array.</param>
        /// <param name="count">The number of characters to copy.</param>
        public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int count);

        #endregion

        #region Conversion

        /// <summary>
        /// Converts a range of characters into a read-only span.
        /// </summary>
        /// <param name="start">The starting index of the range.</param>
        /// <param name="count">The number of characters in the range.</param>
        /// <returns>A read-only span containing the specified character range.</returns>
        public ReadOnlySpan<T> AsSpan(int start, int count);

        /// <summary>
        /// Converts a range of characters into a read-only memory.
        /// </summary>
        /// <param name="start">The starting index of the range.</param>
        /// <param name="count">The number of characters in the range.</param>
        /// <returns>A read-only memory containing the specified character range.</returns>
        public ReadOnlyMemory<T> AsMemory(int start, int count);

        #endregion
    }
}