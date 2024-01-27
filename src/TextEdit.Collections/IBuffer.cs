namespace TextEdit.Collections
{
	public interface IBuffer<T> : IReadOnlyBuffer<T>, IList<T>
	{
		public new int Count { get; }

		public new bool IsReadOnly { get; }

		public new T this[int index] { get; set; }

		public new bool Contains(T item);

		public void RemoveRange(int index, int count);

		public void InsertSpan(int index, ReadOnlySpan<T> span);

		public void InsertRange(int index, IEnumerable<T> enumerable);
	}
}