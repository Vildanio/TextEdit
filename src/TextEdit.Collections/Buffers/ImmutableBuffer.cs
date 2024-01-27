namespace TextEdit.Collections
{
	/// <summary>
	/// <see cref="Buffer{T}"/> which cannot change.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class ImmutableBuffer<T> : Buffer<T>
	{
		// Derived class should ensure that the collection is actually immutable
		// This class is needed to just simplify implementing immutable buffers.

		public override bool IsReadOnly => true;

		public sealed override T this[int index]
		{
			get => GetItem(index);
			set => throw new InvalidOperationException();
		}

		protected abstract T GetItem(int index);

		public sealed override void Add(T item)
		{
			throw new InvalidOperationException();
		}

		public sealed override void Clear()
		{
			throw new InvalidOperationException();
		}

		public sealed override bool Remove(T item)
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

		public sealed override void Insert(int index, T item)
		{
			throw new InvalidOperationException();
		}

		public sealed override void InsertRange(int index, IEnumerable<T> enumerable)
		{
			throw new InvalidOperationException();
		}

		public sealed override void InsertSpan(int index, ReadOnlySpan<T> span)
		{
			throw new InvalidOperationException();
		}
	}
}