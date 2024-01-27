namespace TextEdit.Collections
{
	/// <summary>
	/// <see cref="IReadOnlyBuffer{T}"/> implementes through <see cref="string"/>
	/// </summary>
	public class StringBuffer : ImmutableBuffer<char>, IEquatable<StringBuffer>
    {
        public static StringBuffer Empty { get; } = new StringBuffer(string.Empty);

        public string String { get; }

		#region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringBuffer"/> class
        /// </summary>
        /// <param name="str"></param>
		public StringBuffer(string str)
        {
            this.String = str;
        }

		#endregion

		#region IReadOnlyBuffer

		#region IReadOnlyList

		#region IEnumerable

		public override IEnumerator<char> GetEnumerator()
		{
			return String.GetEnumerator();
		}

		#endregion

		public override int Count => String.Length;

		protected override char GetItem(int index) => String[index];

		#endregion

        public string AsString(int start, int count)
        {
            return String.Substring(start, count);
        }

		public override ReadOnlyMemory<char> AsMemory(int start, int count)
        {
            return String.AsMemory(start, count);
        }

		public override ReadOnlySpan<char> AsSpan(int start, int count)
        {
            return String.AsSpan(start, count);
        }

		public override void CopyTo(int index, int count, Span<char> span)
        {
            String.AsSpan(index, count).CopyTo(span);
        }

		public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            String.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

		public override int IndexOf(char value, int startIndex)
        {
            return String.IndexOf(value, startIndex);
        }

		public override int IndexOf(ReadOnlySpan<char> value, int startIndex)
        {
            return String.AsSpan(startIndex).IndexOf(value);
        }

		public override int LastIndexOf(char value, int startIndex)
        {
            return String.LastIndexOf(value, startIndex);
        }

		public override int LastIndexOf(ReadOnlySpan<char> value, int startIndex)
        {
            return String.AsSpan(0, startIndex).IndexOf(value);
        }

		public override int IndexOfAny(char value0, char value1, int startIndex)
        {
            return String.AsSpan(startIndex).IndexOfAny(value0, value1);
        }

		public override int IndexOfAny(char value0, char value1, char value2, int startIndex)
        {
            return String.AsSpan(startIndex).IndexOfAny(value0, value1, value2);
        }

		public override int IndexOfAny(IEnumerable<char> items, int startIndex)
        {
            // TODO: Optimize
            return String.IndexOfAny(items.ToArray(), startIndex);
        }

		public override int IndexOfAny(ReadOnlySpan<char> items, int startIndex)
        {
            return String.AsSpan(startIndex).IndexOfAny(items);
        }

		public override int LastIndexOfAny(char value0, char value1, int startIndex)
        {
            return String.AsSpan(0, startIndex).LastIndexOfAny(value0, value1);
        }

		public override int LastIndexOfAny(char value0, char value1, char value2, int startIndex)
        {
            return String.AsSpan(0, startIndex).LastIndexOfAny(value0, value1, value2);
        }

		public override int LastIndexOfAny(IEnumerable<char> items, int startIndex)
        {
            // TODO: Optimize
            return String.LastIndexOfAny(items.ToArray(), startIndex);
        }

		public override int LastIndexOfAny(ReadOnlySpan<char> items, int startIndex)
        {
            return String.AsSpan(0, startIndex).LastIndexOfAny(items);
        }

		#endregion

		#region Object

		public bool Equals(StringBuffer? other)
		{
			return other is not null && other.String == this.String;
		}

		public override bool Equals(object? obj)
		{
			return obj is StringBuffer stringBuilderBuffer && this.Equals(stringBuilderBuffer);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(String);
		}

		public override string ToString()
		{
            return String;
		}

		#endregion
	}
}
