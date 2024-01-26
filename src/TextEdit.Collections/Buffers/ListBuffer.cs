using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TextEdit.Collections
{
	/// <summary>
	/// Implements <see cref="Buffer{T}"/> through <see cref="List{T}"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ListBuffer<T> : Buffer<T>
    {
        #region Fields

        private const int DefaultCapacity = 4;

        private int size;
        private T[] items;
        private int version;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBuffer{T}"/> class
        /// </summary>
        public ListBuffer()
            : this(DefaultCapacity)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBuffer{T}"/> class.
        /// The buffer is initially empty, but will have room for the given number of elements
        /// before any reallocations are required.
        /// </summary>
        /// <param name="capacity"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ListBuffer(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            if (capacity == 0)
                items = Array.Empty<T>();
            else
                items = new T[capacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBuffer{T}"/> class
        /// copying the contents of the given collection. The size and capacity of the new list will both be equal to the size of the given collection.
        /// </summary>
        /// <param name="enumerable"></param>
        public ListBuffer(IEnumerable<T> enumerable)
        {
            ThrowHelper.ThrowIfNull(enumerable);

            if (enumerable is ICollection<T> collection)
            {
                int count = collection.Count;

                if (count == 0)
                {
                    items = Array.Empty<T>();
                }
                else
                {
                    items = new T[count];
                    collection.CopyTo(items, 0);
                    size = count;
                }
            }
            else
            {
                items = Array.Empty<T>();

                using (IEnumerator<T> en = enumerable!.GetEnumerator())
                {
                    while (en.MoveNext())
                    {
                        Add(en.Current);
                    }
                }
            }
        }

        internal ListBuffer(T[] items, bool stub)
        {
            this.items = items;
        }

        #endregion

        #region Buffer

        #region IReadOnlyBuffer

        public override bool IsImmutable => true;

		#region IReadOnlyList

		#region IEnumerable

		// Returns an enumerator for this list with the given
		// permission for removal of elements. If modifications made to the list
		// while an enumeration is in progress, the MoveNext and
		// GetObject methods of the enumerator will throw an exception.
		//
		public override IEnumerator<T> GetEnumerator()
			=> new Enumerator(this);

		#region Enumerator

		private struct Enumerator : IEnumerator<T>, IEnumerator
		{
			private int _index;
			private T? _current;
			private readonly int _version;
			private readonly ListBuffer<T> buffer;

			internal Enumerator(ListBuffer<T> list)
			{
				buffer = list;
				_index = 0;
				_version = list.version;
				_current = default;
			}

			public void Dispose()
			{

			}

			public bool MoveNext()
			{
				ListBuffer<T> localList = buffer;

				if (_version == localList.version && ((uint)_index < (uint)localList.size))
				{
					_current = localList.items[_index];
					_index++;
					return true;
				}
				return MoveNextRare();
			}

			private bool MoveNextRare()
			{
				if (_version != buffer.version)
					throw new InvalidOperationException();

				_index = buffer.size + 1;
				_current = default;
				return false;
			}

			public T Current => _current!;

			object? IEnumerator.Current
			{
				get
				{
					if (_index == 0 || _index == buffer.size + 1)
						throw new InvalidOperationException();

					return Current;
				}
			}

			void IEnumerator.Reset()
			{
				if (_version != buffer.version)
					throw new InvalidOperationException();

				_index = 0;
				_current = default;
			}
		}

		#endregion

		#endregion

		// Read-only property describing how many elements are in the List.
		public override int Count => size;

		// Sets or Gets the element at the given index.
		public override T this[int index]
		{
			get
			{
				// Following trick can reduce the range check by one
				if ((uint)index >= (uint)size)
					throw new ArgumentOutOfRangeException(nameof(index));

				return items[index];
			}

			set
			{
				if ((uint)index >= (uint)size)
					throw new ArgumentOutOfRangeException(nameof(index));

				items[index] = value;
				version++;
			}
		}

		#endregion

		// TODO: Implement
		// To use MemoryExtensions methods the T should be IEquatable<T>

		public override int IndexOf(ReadOnlySpan<T> value, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int LastIndexOf(ReadOnlySpan<T> value, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int IndexOfAny(T value0, T value1, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int IndexOfAny(T value0, T value1, T value2, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int IndexOfAny(IEnumerable<T> items, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int IndexOfAny(ReadOnlySpan<T> items, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int LastIndexOfAny(T value0, T value1, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int LastIndexOfAny(T value0, T value1, T value2, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int LastIndexOfAny(IEnumerable<T> items, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override int LastIndexOfAny(ReadOnlySpan<T> items, int startIndex)
        {
            throw new NotImplementedException();
        }

		public override ReadOnlySpan<T> AsSpan(int start, int count)
        {
            return items.AsSpan(start, count);
        }

		public override ReadOnlyMemory<T> AsMemory(int start, int count)
        {
            return AsMemory(start, count);
        }

		public override void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            if (size - index < count)
                throw new ArgumentOutOfRangeException(nameof(count));

            // Delegate rest of error checking to Array.Copy.
            Array.Copy(items, index, array, arrayIndex, count);
        }

		public override void CopyTo(int start, int count, Span<T> span)
        {
            items.AsSpan(start, count).CopyTo(span);
        }

        #endregion

        public override void InsertSpan(int index, ReadOnlySpan<T> span)
        {
            int count = span.Length;

            if (count > 0)
            {
                if (items.Length - size < count)
                {
                    Grow(size + count);
                }

                if (index < size)
                {
                    Array.Copy(items, index, items, index + count, size - index);
                }

                span.CopyTo(items.AsSpan(index, count));

                size += count;
            }
        }

		// Removes a range of elements from this list.
		public override void RemoveRange(int index, int count)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (count < 0 || size - index < count)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count > 0)
            {
                size -= count;
                if (index < size)
                {
                    Array.Copy(items, index + count, items, index, size - index);
                }

                version++;
                if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                {
                    Array.Clear(items, size, count);
                }
            }
        }

		// Inserts the elements of the given collection at a given index. If
		// required, the capacity of the list is increased to twice the previous
		// capacity or the new size, whichever is larger.  Ranges may be added
		// to the end of the list by setting index to the List's size.
		//
		public override void InsertRange(int index, IEnumerable<T> collection)
        {
            ThrowHelper.ThrowIfNull(collection);

            if ((uint)index > (uint)size)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (collection is ICollection<T> c)
            {
                int count = c.Count;
                if (count > 0)
                {
                    if (items.Length - size < count)
                    {
                        Grow(size + count);
                    }
                    if (index < size)
                    {
                        Array.Copy(items, index, items, index + count, size - index);
                    }

                    // If we're inserting a List into itself, we want to be able to deal with that.
                    if (this == c)
                    {
                        // Copy first part of _items to insert location
                        Array.Copy(items, 0, items, index, index);
                        // Copy last part of _items back to inserted location
                        Array.Copy(items, index + count, items, index * 2, size - index);
                    }
                    else
                    {
                        c.CopyTo(items, index);
                    }
                    size += count;
                }
            }
            else
            {
                using (IEnumerator<T> en = collection.GetEnumerator())
                {
                    while (en.MoveNext())
                    {
                        Insert(index++, en.Current);
                    }
                }
            }
            version++;
        }

		#region IList

		#region ICollection

		// Gets and sets the capacity of this list.  The capacity is the size of
		// the internal array used to hold items.  When set, the internal
		// array of the list is reallocated to the given capacity.
		//
		public int Capacity
		{
			get => items.Length;
			set
			{
				if (value < size)
					throw new ArgumentOutOfRangeException(nameof(value));

				if (value != items.Length)
				{
					if (value > 0)
					{
						T[] newItems = new T[value];
						if (size > 0)
						{
							Array.Copy(items, newItems, size);
						}
						items = newItems;
					}
					else
					{
						items = Array.Empty<T>();
					}
				}
			}
		}

		// Adds the given object to the end of this list. The size of the list is
		// increased by one. If required, the capacity of the list is doubled
		// before adding the new element.
		//
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Add(T item)
		{
			version++;
			T[] array = items;
			int size = this.size;
			if ((uint)size < (uint)array.Length)
			{
				this.size = size + 1;
				array[size] = item;
			}
			else
			{
				AddWithResize(item);
			}
		}

		// Removes the element at the given index. The size of the list is
		// decreased by one.
		public override bool Remove(T item)
		{
			int index = IndexOf(item);
			if (index >= 0)
			{
				RemoveAt(index);
				return true;
			}

			return false;
		}

		// Non-inline from List.Add to improve its code quality as uncommon path
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithResize(T item)
		{
			Debug.Assert(this.size == items.Length);
			int size = this.size;
			Grow(size + 1);
			this.size = size + 1;
			items[size] = item;
		}

		// Clears the contents of List.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Clear()
		{
			version++;
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				int size = this.size;
				this.size = 0;
				if (size > 0)
				{
					Array.Clear(items, 0, size); // Clear the elements so that the gc can reclaim the references.
				}
			}
			else
			{
				size = 0;
			}
		}

		// Contains returns true if the specified element is in the List.
		// It does a linear, O(n) search.  Equality is determined by calling
		// EqualityComparer<T>.Default.Equals().
		//
		public override bool Contains(T item)
		{
			// PERF: IndexOf calls Array.IndexOf, which internally
			// calls EqualityComparer<T>.Default.IndexOf, which
			// is specialized for different types. This
			// boosts performance since instead of making a
			// virtual method call each iteration of the loop,
			// via EqualityComparer<T>.Default.Equals, we
			// only make one virtual call to EqualityComparer.IndexOf.

			return size != 0 && IndexOf(item) >= 0;
		}

		public override void CopyTo(T[] array, int arrayIndex)
		{
			// Delegate rest of error checking to Array.Copy.
			Array.Copy(items, 0, array, arrayIndex, size);
		}

		#endregion

		// Returns the index of the first occurrence of a given value in a range of
		// this list. The list is searched forwards from beginning to end.
		// The elements of the list are compared to the given value using the
		// Object.Equals method.
		//
		// This method uses the Array.IndexOf method to perform the
		// search.
		//
		public override int IndexOf(T item)
            => Array.IndexOf(items, item, 0, size);

		// Returns the index of the first occurrence of a given value in a range of
		// this list. The list is searched forwards, starting at index
		// index and ending at count number of elements. The
		// elements of the list are compared to the given value using the
		// Object.Equals method.
		//
		// This method uses the Array.IndexOf method to perform the
		// search.
		//
		public override int IndexOf(T item, int index)
        {
            if (index > size)
                throw new ArgumentOutOfRangeException(nameof(index));

            return Array.IndexOf(items, item, index, size - index);
        }

        // Returns the index of the first occurrence of a given value in a range of
        // this list. The list is searched forwards, starting at index
        // index and upto count number of elements. The
        // elements of the list are compared to the given value using the
        // Object.Equals method.
        //
        // This method uses the Array.IndexOf method to perform the
        // search.
        //
        public int IndexOf(T item, int index, int count)
        {
            if (index > size)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (count < 0 || index > size - count)
                throw new ArgumentOutOfRangeException(nameof(count));

            return Array.IndexOf(items, item, index, count);
        }

		// Returns the index of the last occurrence of a given value in a range of
		// this list. The list is searched backwards, starting at index
		// index and ending at the first element in the list. The
		// elements of the list are compared to the given value using the
		// Object.Equals method.
		//
		// This method uses the Array.LastIndexOf method to perform the
		// search.
		//
		public override int LastIndexOf(T item, int index)
        {
            if (index >= size)
                throw new ArgumentOutOfRangeException(nameof(index));

            return LastIndexOf(item, index, index + 1);
        }

        // Returns the index of the last occurrence of a given value in a range of
        // this list. The list is searched backwards, starting at index
        // index and upto count elements. The elements of
        // the list are compared to the given value using the Object.Equals
        // method.
        //
        // This method uses the Array.LastIndexOf method to perform the
        // search.
        //
        public int LastIndexOf(T item, int index, int count)
        {
            if ((Count != 0) && (index < 0))
                throw new ArgumentOutOfRangeException(nameof(index));

            if ((Count != 0) && (count < 0))
                throw new ArgumentOutOfRangeException(nameof(count));

            if (size == 0)
            {  // Special case for empty list
                return -1;
            }

            if (index >= size)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (count > index + 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            return Array.LastIndexOf(items, item, index, count);
        }

		// Removes the element at the given index. The size of the list is
		// decreased by one.
		public override void RemoveAt(int index)
        {
            if ((uint)index >= (uint)size)
                throw new ArgumentOutOfRangeException(nameof(index));

            size--;
            if (index < size)
            {
                Array.Copy(items, index + 1, items, index, size - index);
            }
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                items[size] = default!;
            }
            version++;
        }

        // Inserts an element into this list at a given index. The size of the list
        // is increased by one. If required, the capacity of the list is doubled
        // before inserting the new element.
        //
        public override void Insert(int index, T item)
        {
            // Note that insertions at the end are legal.
            if ((uint)index > (uint)size)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (size == items.Length) Grow(size + 1);
            if (index < size)
            {
                Array.Copy(items, index, items, index + 1, size - index);
            }
            items[index] = item;
            size++;
            version++;
        }

        #endregion


		#endregion

        #region Helpers

        /// <summary>
        /// Ensures that the capacity of this list is at least the specified <paramref name="capacity"/>.
        /// If the current capacity of the list is less than specified <paramref name="capacity"/>,
        /// the capacity is increased by continuously twice current capacity until it is at least the specified <paramref name="capacity"/>.
        /// </summary>
        /// <param name="capacity">The minimum capacity to ensure.</param>
        /// <returns>The new capacity of this list.</returns>
        public int EnsureCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            if (items.Length < capacity)
            {
                Grow(capacity);
                version++;
            }

            return items.Length;
        }

        /// <summary>
        /// Increase the capacity of this list to at least the specified <paramref name="capacity"/>.
        /// </summary>
        /// <param name="capacity">The minimum capacity to ensure.</param>
        private void Grow(int capacity)
        {
            Debug.Assert(items.Length < capacity);

            int newcapacity = items.Length == 0 ? DefaultCapacity : 2 * items.Length;

            // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
            // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
            if ((uint)newcapacity > Array.MaxLength) newcapacity = Array.MaxLength;

            // If the computed capacity is still less than specified, set to the original argument.
            // Capacities exceeding Array.MaxLength will be surfaced as OutOfMemoryException by Array.Resize.
            if (newcapacity < capacity) newcapacity = capacity;

            Capacity = newcapacity;
        }

        // ToArray returns an array containing the contents of the List.
        // This requires copying the List, which is an O(n) operation.
        public T[] ToArray()
        {
            if (size == 0)
                return Array.Empty<T>();

            T[] array = new T[size];
            Array.Copy(items, array, size);
            return array;
        }

        public List<T> ToList()
        {
            return new List<T>(items);
        }

        // Sets the capacity of this list to the size of the list. This method can
        // be used to minimize a list's memory overhead once it is known that no
        // new elements will be added to the list. To completely clear a list and
        // release all memory referenced by the list, execute the following
        // statements:
        //
        // list.Clear();
        // list.TrimExcess();
        //
        public void TrimExcess()
        {
            int threshold = (int)(((double)items.Length) * 0.9);
            if (size < threshold)
            {
                Capacity = size;
            }
        }

        public ListBuffer<T> GetRange(int index, int count)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (count < 0 || size - index < count)
                throw new ArgumentOutOfRangeException(nameof(count));

            ListBuffer<T> list = new ListBuffer<T>(count);

            Array.Copy(items, index, list.items, 0, count);

            list.size = count;

            return list;
        }

        #endregion
	}
}
