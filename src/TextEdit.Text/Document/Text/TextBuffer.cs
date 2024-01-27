using TextEdit.Collections;

namespace TextEdit.Text
{
	public abstract class TextBuffer : Text
    {
        protected IBuffer<char> Buffer { get; }

		protected TextBuffer(IBuffer<char> buffer)
		{
			this.Buffer = buffer;
		}

		#region Text

		#region IReadOnlyText

		#region IReadOnlyList

		public sealed override char this[int index]
		{
			get
			{
				return Buffer[index];
			}

			set
			{
				Buffer[index] = value;
			}
		}

		public sealed override int Count => Buffer.Count;

		#region IEnumerable

		public sealed override IEnumerator<char> GetEnumerator()
		{
			return Buffer.GetEnumerator();
		}

		#endregion

		#endregion

		public sealed override bool IsReadOnly => Buffer.IsReadOnly;

		public sealed override ReadOnlyMemory<char> AsMemory(int start, int count)
		{
			return Buffer.AsMemory(start, count);
		}

		public sealed override ReadOnlySpan<char> AsSpan(int start, int count)
		{
			return Buffer.AsSpan(start, count);
		}

		public sealed override void CopyTo(int index, int count, Span<char> span)
		{
			Buffer.CopyTo(index, count, span);
		}

		public sealed override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			Buffer.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		public sealed override int IndexOf(char value, int startIndex)
		{
			return Buffer.IndexOf(value, startIndex);
		}

		public sealed override int IndexOf(ReadOnlySpan<char> value, int startIndex)
		{
			return Buffer.IndexOf(value, startIndex);
		}

		public sealed override int IndexOfAny(char value0, char value1, int startIndex)
		{
			return Buffer.IndexOfAny(value0, value1, startIndex);
		}

		public sealed override int IndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return Buffer.IndexOfAny(value0, value1, value2, startIndex);
		}

		public sealed override int IndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return Buffer.IndexOfAny(items, startIndex);
		}

		public sealed override int IndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return Buffer.IndexOfAny(items, startIndex);
		}

		public sealed override int LastIndexOf(char value, int startIndex)
		{
			return Buffer.LastIndexOf(value, startIndex);
		}

		public sealed override int LastIndexOf(ReadOnlySpan<char> value, int startIndex)
		{
			return Buffer.LastIndexOf(value, startIndex);
		}

		public sealed override int LastIndexOfAny(char value0, char value1, int startIndex)
		{
			return Buffer.LastIndexOfAny(value0, value1, startIndex);
		}

		public sealed override int LastIndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return Buffer.LastIndexOfAny(value0, value1, value2, startIndex);
		}

		public sealed override int LastIndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return Buffer.LastIndexOfAny(items, startIndex);
		}

		public sealed override int LastIndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return Buffer.LastIndexOfAny(items, startIndex);
		}

		#endregion

		#region IList

		public sealed override void RemoveAt(int index)
		{
			Buffer.RemoveAt(index);
		}

		public sealed override void Insert(int index, char item)
		{
			Buffer.Insert(index, item);
		}

		#endregion

		public sealed override void RemoveRange(int index, int count)
		{
			Buffer.RemoveRange(index, count);
		}

		public sealed override void InsertSpan(int index, ReadOnlySpan<char> span)
		{
			Buffer.InsertSpan(index, span);
		}

		public sealed override void InsertRange(int index, IEnumerable<char> enumerable)
		{
			Buffer.InsertRange(index, enumerable);
		}

		public override void InsertString(int index, string text)
		{
			Buffer.InsertSpan(index, text);
		}

		#endregion
	}
}