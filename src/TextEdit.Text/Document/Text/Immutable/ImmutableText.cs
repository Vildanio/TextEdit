namespace TextEdit.Text
{
	public abstract class ImmutableText : Text
    {
		public sealed override bool IsReadOnly => true;

		public sealed override char this[int index]
		{
			get => GetItem(index);
			set => throw new InvalidOperationException();
		}

		protected abstract char GetItem(int index);

		public sealed override void Add(char item)
		{
			throw new InvalidOperationException();
		}

		public sealed override void Clear()
		{
			throw new InvalidOperationException();
		}

		public sealed override void Insert(int index, char item)
		{
			throw new InvalidOperationException();
		}

		public sealed override void InsertRange(int index, IEnumerable<char> enumerable)
		{
			throw new InvalidOperationException();
		}

		public sealed override void InsertSpan(int index, ReadOnlySpan<char> span)
		{
			throw new InvalidOperationException();
		}

		public sealed override void InsertString(int index, string text)
		{
			throw new InvalidOperationException();
		}

		public sealed override bool Remove(char item)
		{
			throw new InvalidOperationException();
		}

		public sealed override void RemoveAt(int index)
		{
			throw new InvalidOperationException();
		}

		public sealed override void RemoveRange(int index, int count)
		{
			throw new InvalidOperationException();
		}
	}
}