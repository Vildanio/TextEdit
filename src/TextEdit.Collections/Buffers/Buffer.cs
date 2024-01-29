using System.Collections;

namespace TextEdit.Collections
{
	public abstract class Buffer<T> : IBuffer<T>
    {
		#region IBuffer

		#region IReadOnlyBuffer

        public abstract bool IsReadOnly { get; }

		#region IReadOnlyList

		public abstract int Count { get; }

		public abstract T this[int index] { get; set; }

		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public abstract IEnumerator<T> GetEnumerator();

		#endregion

		#endregion

		public abstract ReadOnlySpan<T> AsSpan(int start, int count);

		public abstract ReadOnlyMemory<T> AsMemory(int start, int count);

		public abstract void CopyTo(int index, int count, Span<T> span);

		public abstract void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int count);

		public abstract int IndexOf(T value, int startIndex);

		public abstract int IndexOf(ReadOnlySpan<T> value, int startIndex);

		public abstract int LastIndexOf(T value, int startIndex);

		public abstract int LastIndexOf(ReadOnlySpan<T> value, int startIndex);

		public abstract int IndexOfAny(T value0, T value1, int startIndex);

		public abstract int IndexOfAny(T value0, T value1, T value2, int startIndex);

		public abstract int IndexOfAny(IEnumerable<T> items, int startIndex);

		public abstract int IndexOfAny(ReadOnlySpan<T> items, int startIndex);

		public abstract int LastIndexOfAny(T value0, T value1, int startIndex);

		public abstract int LastIndexOfAny(T value0, T value1, T value2, int startIndex);

		public abstract int LastIndexOfAny(IEnumerable<T> items, int startIndex);

		public abstract int LastIndexOfAny(ReadOnlySpan<T> items, int startIndex);

		#endregion

		#region IList

		#region ICollection

		public virtual void Add(T item)
		{
			Insert(Count, item);
		}

		public virtual bool Remove(T item)
		{
			int index = IndexOf(item);

			if (index >= 0)
			{
				RemoveAt(index);
			}

			return false;
		}

		public virtual void Clear()
		{
			RemoveRange(0, Count);
		}

		public virtual bool Contains(T value)
		{
			return IndexOf(value, 0) >= 0;
		}

		public virtual bool Contains(ReadOnlySpan<T> value)
		{
			return IndexOf(value, 0) >= 0;
		}

		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			ThrowHelper.ThrowIfNull(array);
			ThrowHelper.ThrowIfOutOfRange(arrayIndex, array.Length, maxValueParamName: null);

			if (array.Rank is not 1)
				throw new ArgumentException(null, nameof(array));

			CopyTo(0, array, arrayIndex, array.Length - arrayIndex);
		}

		#endregion

		public virtual int IndexOf(T item)
		{
			return IndexOf(item, 0);
		}

		public abstract void RemoveAt(int index);

        public abstract void Insert(int index, T item);

		#endregion

        public abstract void RemoveRange(int index, int count);

        public abstract void InsertSpan(int index, ReadOnlySpan<T> span);

        public abstract void InsertRange(int index, IEnumerable<T> enumerable);

        #endregion
    }
}