using TextEdit.Collections;

namespace TextEdit.Text
{
	/// <summary>
	/// <see cref="ImmutableText"/> implemented through <see cref="IReadOnlyText"/>
	/// </summary>
	public class ReadOnlyText : ImmutableText
	{
		#region Static

		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyText"/> class using the given <paramref name="text"/> without copy.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static ReadOnlyText CreateUnsafe(IReadOnlyText text)
		{
			return new ReadOnlyText(text, default);
		}

		#endregion

		private readonly IReadOnlyText text;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyText"/> class.
		/// </summary>
		/// <param name="text"></param>
		public ReadOnlyText(IReadOnlyText text)
			: this(text.Clone(), default)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyText"/> class.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="stbub"></param>
		private ReadOnlyText(IReadOnlyText text, bool stbub)
		{
			this.text = text;
		}

		#endregion

		protected override char GetItem(int index)
		{
			return text[index];
		}

		public override int Count => text.Count;

		public override ReadOnlyMemory<char> AsMemory(int start, int count)
		{
			return text.AsMemory(start, count);
		}

		public override ReadOnlySpan<char> AsSpan(int start, int count)
		{
			return text.AsSpan(start, count);
		}

		public override string AsString(int start, int count)
		{
			return text.AsString(start, count);
		}

		public override bool Contains(char item)
		{
			return text.Contains(item);
		}

		public override void CopyTo(int index, int count, Span<char> span)
		{
			text.CopyTo(index, count, span);
		}

		public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			text.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		public override void CopyTo(char[] array, int arrayIndex)
		{
			text.CopyTo(array, arrayIndex);
		}

		public override IEnumerator<char> GetEnumerator()
		{
			return text.GetEnumerator();
		}

		public override int IndexOf(char value, int startIndex)
		{
			return text.IndexOf(value, startIndex);
		}

		public override int IndexOf(ReadOnlySpan<char> value, int startIndex)
		{
			return text.IndexOf(value, startIndex);
		}

		public override int IndexOf(char item)
		{
			return text.IndexOf(item);
		}

		public override int IndexOfAny(char value0, char value1, int startIndex)
		{
			return text.IndexOfAny(value0, value1, startIndex);
		}

		public override int IndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return text.IndexOfAny(value0, value1, value2, startIndex);
		}

		public override int IndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return text.IndexOfAny(items, startIndex);
		}

		public override int IndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return text.IndexOfAny(items, startIndex);
		}

		public override int LastIndexOf(char value, int startIndex)
		{
			return text.LastIndexOf(value, startIndex);
		}

		public override int LastIndexOf(ReadOnlySpan<char> value, int startIndex)
		{
			return text.LastIndexOf(value, startIndex);
		}

		public override int LastIndexOfAny(char value0, char value1, int startIndex)
		{
			return text.LastIndexOfAny(value0, value1, startIndex);
		}

		public override int LastIndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return text.LastIndexOfAny(value0, value1, value2, startIndex);
		}

		public override int LastIndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return text.LastIndexOfAny(items, startIndex);
		}

		public override int LastIndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return text.LastIndexOfAny(items, startIndex);
		}

		public override IText Clone()
		{
			return new ReadOnlyText(text.Clone());
		}
	}
}