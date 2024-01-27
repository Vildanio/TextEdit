using TextEdit.Collections;

namespace TextEdit.Text
{
    /// <summary>
    /// Provides read-only access to underlying <see cref="char"/> buffer
    /// </summary>
    public interface IReadOnlyText : IReadOnlyBuffer<char>
    {
        public new static IReadOnlyText Empty { get; } = new StringText();

		/// <summary>
		/// Creates a new instance of the text with the same value
		/// </summary>
		/// <returns></returns>
		public IReadOnlyText Clone();

		/// <summary>
		/// Converts a range of characters into a string.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of characters in the range.</param>
		/// <returns>A string containing the specified character range.</returns>
		public string AsString(int start, int count);

		public string ToString()
		{
			return AsString(0, Count);
		}
    }
}