using System.Buffers;
using System.Collections;
using System.Runtime.CompilerServices;
using TextEdit.Collections;
using TextEdit.Utils;

namespace TextEdit.Collections
{
	/// <summary>
	/// Represents a strongly typed collection of objects that can be accessed by index. Insertions and 
	/// deletions to the collection near the same relative index are optimized.
	/// </summary>
	/// <typeparam name="T">The type of elements in the buffer</typeparam>
	public class GapBuffer<T> : Buffer<T>
        where T : IEquatable<T>
    {
        #region Fields

        private const int DefaultCapacity = 4;

        private T[] array;
        private int gapStart;
        private int gapEnd;
        private int version;

        private int GapCount => gapEnd - gapStart;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GapBuffer{T}"/> class.
        /// </summary>
        public GapBuffer()
            : this(DefaultCapacity)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GapBuffer{T}"/> class.
        /// </summary>
        /// <param name="capacity"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public GapBuffer(int capacity)
        {
            array = new T[capacity];
            gapEnd = capacity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GapBuffer{T}"/> class.
        /// </summary>
        /// <param name="span"></param>
        public GapBuffer(ReadOnlySpan<T> span)
        {
            array = new T[span.Length];

            span.CopyTo(array);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GapBuffer{T}"/> class
        /// </summary>
        /// <param name="enumerable"></param>
        public GapBuffer(IEnumerable<T> enumerable)
        {
            ThrowHelper.ThrowIfNull(enumerable);

            if (enumerable is GapBuffer<T> buffer)
            {
                this.array = buffer.array;
                gapEnd = buffer.gapEnd;
                gapStart = buffer.gapStart;
            }
            else
            {
                this.array = enumerable.ToArray();
            }
        }

		#endregion Constructors

        #region Helpers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfOutOfRange(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfOutOfRange(int index, int count,

            [CallerArgumentExpression(nameof(index))] string? indexParamName = null,
            [CallerArgumentExpression(nameof(count))] string? countParamName = null)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(indexParamName));

            if (count < 0 || index + count >= Count)
                throw new ArgumentOutOfRangeException(nameof(countParamName));
        }

        /// <summary>
        /// Sets the <see cref="Capacity"/> to the actual number of elements in the <see cref="GapBuffer{T}"/>, 
        /// if that number is less than a threshold value. 
        /// </summary>
        public void TrimExcess()
        {
            int size = Count;
            int threshold = (int)(array.Length * 0.9);
            if (size < threshold)
            {
                Capacity = size;
            }
        }

        // Moves the gap start to the given index
        private void PlaceGapStart(int index)
        {
            // Are we already there?
            if (index == gapStart)
                return;

            // Is there even a gap?
            if (GapCount == 0)
            {
                gapStart = gapEnd = index;
                return;
            }

            // Which direction do we move the gap?
            if (index < gapStart)
            {
                // Move the gap near (by copying the items at the beginning of the gap to the end)
                int count = gapStart - index;
                int deltaCount = GapCount < count ? GapCount : count;
                Array.Copy(array, index, array, gapEnd - count, count);
                gapStart -= count;
                gapEnd -= count;

                // Clear the contents of the gap
                Array.Clear(array, index, deltaCount);
            }
            else
            {
                // Move the gap far (by copying the items at the end of the gap to the beginning)
                int count = index - gapStart;
                int deltaIndex = index > gapEnd ? index : gapEnd;
                Array.Copy(array, gapEnd, array, gapStart, count);
                gapStart += count;
                gapEnd += count;

                // Clear the contents of the gap
                Array.Clear(array, deltaIndex, gapEnd - deltaIndex);
            }
        }

        /// <summary>
        /// Ensures that the <see cref="gapStart"/> == <paramref name="index"/> and the <see cref="GapCount"/> >= <paramref name="count"/>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        private void EnsureGap(int index, int count)
        {
            PlaceGapStart(index);
            EnsureGapCapacity(count);
        }

        // Expands the interal array if the required size isn't available
        private void EnsureGapCapacity(int required)
        {
            // Is the available space in the gap?
            if (required > GapCount)
            {
                Capacity = Math.Max(Count + required, DefaultCapacity);
            }
        }

        private void ClearGap()
        {
            if (GapCount > 0)
            {
                int gapCount = GapCount;

                Array.Copy(array, gapEnd, array, gapStart, GapCount);

                gapStart = Count + gapCount;
                gapEnd = gapCount;
            }
        }

        #endregion

        #region Buffer

        #region IReadOnlyBuffer

		#region IReadOnlyList

		/// <summary>
		/// Gets the number of elements actually contained in the <see cref="GapBuffer{T}"/>.
		/// </summary>
		/// <value>
		/// The number of elements actually contained in the <see cref="GapBuffer{T}"/>.
		/// </value>
		public override int Count => array.Length - GapCount;

		/// <summary>
		/// Gets or sets the total number of elements the internal data structure can hold 
		/// without resizing.
		/// </summary>
		/// <value>The number of elements that the <see cref="GapBuffer{T}"/> can contain before 
		/// resizing is required.</value>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <see cref="Capacity"/> is set to a value that is less than <see cref="Count"/>. 
		/// </exception>
		public int Capacity
		{
			get
			{
				return array.Length;
			}
			set
			{
				// Is there any work to do?
				if (value == array.Length)
					return;

				// Look for naughty boys and girls
				if (value < Count)
					throw new ArgumentOutOfRangeException(nameof(value));

				if (value > 0)
				{
					// Allocate a new buffer
					T[] newBuffer = new T[value];
					int newGapEnd = newBuffer.Length - (array.Length - gapEnd);

					// Copy the spans into the front and back of the new buffer
					Array.Copy(array, 0, newBuffer, 0, gapStart);
					Array.Copy(array, gapEnd, newBuffer, newGapEnd, newBuffer.Length - newGapEnd);
					array = newBuffer;
					gapEnd = newGapEnd;
				}
				else
				{
					// Reset everything
					array = new T[DefaultCapacity];
					gapStart = 0;
					gapEnd = array.Length;
				}
			}
		}

		#endregion

		#region IEnumerable

		public override IEnumerator<T> GetEnumerator()
		{
			return new Enumerator(this);
		}

		/// <summary>
		/// Enumerates the elements of a <see cref="GapBuffer{T}"/>. 
		/// </summary>
		private sealed class Enumerator : IEnumerator<T>, IEnumerator
		{
			#region Fields

			private T _current;
			private int _index;
			private int _version;
			private readonly GapBuffer<T> _gapBuffer;

			#endregion Fields

			#region Constructors

			internal Enumerator(GapBuffer<T> buffer)
			{
				_gapBuffer = buffer;
				_index = 0;
				_version = _gapBuffer.version;
				_current = default!;
			}

			#endregion Constructors

			#region Properties

			/// <summary>
			/// Gets the element at the current position of the enumerator.
			/// </summary>
			/// <value>The element in the <see cref="GapBuffer{T}"/> at the current 
			/// position of the enumerator.</value>
			public T Current => _current;

			object? IEnumerator.Current
			{
				get
				{
					// Is it possible to have a current item?
					if (_index == 0 || _index == _gapBuffer.Count + 1)
						throw new InvalidOperationException();

					return _current;
				}
			}

			#endregion Properties

			#region Methods

			/// <summary>
			/// Advances the enumerator to the next element of the <see cref="GapBuffer{T}"/>.
			/// </summary>
			/// <returns><b>true</b> if the enumerator was successfully advanced to the next element; 
			/// <b>false</b> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="InvalidOperationException">
			/// The collection was modified after the enumerator was created. 
			/// </exception>
			public bool MoveNext()
			{
				// Check version numbers
				if (_version != _gapBuffer.version)
					throw new InvalidOperationException();

				// Advance the index
				if (_index < _gapBuffer.Count)
				{
					_current = _gapBuffer[_index];
					_index++;
					return true;
				}

				// The pointer is at the end of the collection
				_index = _gapBuffer.Count + 1;
				_current = default!;
				return false;
			}

			/// <summary>
			/// Releases all resources used by the <see cref="GapBuffer{T}.Enumerator"/>. 
			/// </summary>
			public void Dispose()
			{
				// Nothing to release here
			}

			// Explicit IEnumerator implementation
			void IEnumerator.Reset()
			{
				// Check the version
				if (_version != _gapBuffer.version)
					throw new InvalidOperationException();

				// Reset the pointer
				_index = 0;
				_current = default!;
			}

			#endregion Methods
		}

		#endregion

        public override bool IsImmutable => false;

		public override int IndexOf(ReadOnlySpan<T> value, int startIndex)
        {
            return array.AsSpan(startIndex).IndexOf(value) + startIndex;
		}

        public override int LastIndexOf(ReadOnlySpan<T> value, int startIndex)
        {
#pragma warning disable CS8631
            return array.AsSpan(0, startIndex).LastIndexOf(value) + startIndex;
#pragma warning restore CS8631
		}

        public override int IndexOfAny(T value0, T value1, int startIndex)
        {
            // The ReadOnlySpan.IndexOfAny method thinks that the startIndex is 0
            return array.AsSpan(startIndex).IndexOfAny(value0, value1) + startIndex;
		}

        public override int IndexOfAny(T value0, T value1, T value2, int startIndex)
        {
            return array.AsSpan(startIndex).IndexOfAny(value0, value1, value2) + startIndex;
		}

        public override int IndexOfAny(IEnumerable<T> items, int startIndex)
        {
            // TODO: Optimize
            return array.AsSpan(startIndex).IndexOfAny(items.ToArray().AsSpan()) + startIndex;
		}

        public override int IndexOfAny(ReadOnlySpan<T> items, int startIndex)
        {
            return array.AsSpan(startIndex).IndexOfAny(items) + startIndex;
		}

        public override int LastIndexOfAny(T value0, T value1, int startIndex)
        {
            return array.AsSpan(0, startIndex).LastIndexOfAny(value0, value1) + startIndex;
		}

        public override int LastIndexOfAny(T value0, T value1, T value2, int startIndex)
        {
            return array.AsSpan(0, startIndex).LastIndexOfAny(value0, value1, value2) + startIndex;
		}

        public override int LastIndexOfAny(IEnumerable<T> items, int startIndex)
        {
            // TODO: Optimize
            return array.AsSpan(0, startIndex).LastIndexOfAny(items.ToArray().AsSpan()) + startIndex;
		}

        public override int LastIndexOfAny(ReadOnlySpan<T> items, int startIndex)
        {
            return array.AsSpan(0, startIndex).LastIndexOfAny(items) + startIndex;
		}

        public override ReadOnlySpan<T> AsSpan(int start, int count)
        {
            return AsMemory(start, count).Span;
        }

        public override ReadOnlyMemory<T> AsMemory(int start, int count)
        {
            if (NeedValidation(start, count))
            {
                return array.AsMemory(start, count);
            }

            return this.ToArray(start, count).AsMemory(0, count);
        }

		#endregion

		#region IList

		#region ICollection

		private bool NeedValidation(int start, int count)
		{
			return GapCount > 0 && start < gapStart && start + count > gapStart + 1;
		}

		public override void CopyTo(int start, int count, Span<T> span)
		{
			if (start < 0 || start > Count)
				throw new ArgumentOutOfRangeException(nameof(start));

			if (count < 0 || start + count > Count)
				throw new ArgumentOutOfRangeException(nameof(start));

			if (count > span.Length)
				throw new ArgumentOutOfRangeException(nameof(count));

			if (count == 0)
				return;

			// We should separate copyig into two parts only when 
			// the range starts before the gapStart and ends after the gapStart

			// If the given range overlaps the gap
			if (NeedValidation(start, count))
			{
				int start1 = start;
				int count1 = gapStart - start;

				int start2 = gapEnd;
				int count2 = count - count1;

				Span<T> span1 = array.AsSpan(start1, count1);
				Span<T> span2 = array.AsSpan(start2, count2);

				span2.CopyTo(span);
				span1.CopyTo(span);
			}
			else
			{
				array.AsSpan(start, count).CopyTo(span);
			}
		}

		public override void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
		{
			CopyTo(sourceIndex, length, destination.AsSpan(destinationIndex, length));
		}

		/// <summary>
		/// Removes all elements from the <see cref="GapBuffer{T}"/>.
		/// </summary>
		public override void Clear()
		{
			// Clearing the buffer means simply enlarging the gap to the
			// entire buffer size

			Array.Clear(array, 0, array.Length);
			gapStart = 0;
			gapEnd = array.Length;
			version++;
		}

		#endregion

		public override T this[int index]
        {
            get
            {
                ThrowIfOutOfRange(index);

                // Find the correct span and get the item
                if (index >= gapStart)
                    index += GapCount;

                return array[index];
            }

            set
            {
                ThrowIfOutOfRange(index);

                // Find the correct span and set the item
                if (index >= gapStart)
                    index += GapCount;

                array[index] = value;
                version++;
            }
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first 
        /// occurrence within the <see cref="GapBuffer{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="GapBuffer{T}"/>. The value 
        /// can be null for reference types.</param>
        /// <returns>The zero-based index of the first occurrence of <paramref name="item"/> within 
        /// the <see cref="GapBuffer{T}"/>, if found; otherwise, �1.</returns>
        public override int IndexOf(T item, int start)
        {
            int index = Array.IndexOf(array, item, start, gapStart);

            if (index < 0)
            {
                index = Array.IndexOf(array, item, gapEnd, array.Length - gapEnd);

                // Translate the internal index to the index in the collection
                if (index != -1)
                    return index - GapCount;
            }

            return index;
        }

        public override int LastIndexOf(T item, int start)
        {
            int index = Array.LastIndexOf(array, item, start - 1, start - gapStart);

            if (index < 0)
            {
                index = Array.LastIndexOf(array, item, gapStart, gapStart);

                // Translate the internal index to the index in the collection
                if (index != -1)
                    return index - GapCount;
            }

            return index;
        }

        /// <summary>
        /// Inserts an element into the <see cref="GapBuffer{T}"/> at the specified index. Consecutive operations
        /// at or near previous inserts are optimized.
        /// </summary>
        /// <param name="index">The object to insert. The value can be null for reference types.</param>
        /// <param name="item">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0.
        /// <para>-or-</para>
        /// <paramref name="index"/> is greater than <see cref="Count"/>.
        /// </exception>
        public override void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // Prepare the buffer
            EnsureGap(index, 1);

            array[index] = item;
            gapStart++;
            version++;
        }

        /// <summary>
        /// Removes the element at the specified index of the <see cref="GapBuffer{T}"/>. 
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0.
        /// <para>-or-</para>
        /// <paramref name="index"/> is equal to or greater than <see cref="Count"/>.
        /// </exception>
        public override void RemoveAt(int index)
        {
            ThrowIfOutOfRange(index);

            // This is just an optimization
            // The code in else block does the same thing
            if (index == gapStart - 1)
            {
                gapStart = index; // gapStart--;
                array[index] = default!;
            }
            else
            {
                // Place the gap at the index and increase the gap size by 1
                PlaceGapStart(index);
                array[gapEnd] = default!;
                gapEnd++;
            }

            version++;
        }

        /// <summary>
        /// Inserts the elements of a collection into the <see cref="GapBuffer{T}"/> at the specified index. 
        /// Consecutive operations at or near previous inserts are optimized.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">The collection whose elements should be inserted into the <see cref="GapBuffer{T}"/>. 
        /// The collection itself cannot be null, but it can contain elements that are null, if 
        /// type <typeparamref name="T"/> is a reference type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is a null reference.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0.
        /// <para>-or-</para>
        /// <paramref name="index"/> is greater than <see cref="Count"/>.
        /// </exception>
        public override void InsertRange(int index, IEnumerable<T> collection)
        {
            ThrowHelper.ThrowIfNull(collection);
            ThrowIfOutOfRange(index);

            if (collection is ICollection<T> col)
            {
                int count = col.Count;
                if (count > 0)
                {
                    EnsureGap(index, count);

                    // Copy the collection directly into the buffer
                    col.CopyTo(array, gapStart);
                    gapStart += count;
                }
            }
            else
            {
                // Add the items to the buffer one-at-a-time :(
                using (IEnumerator<T> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Insert(index, enumerator.Current);
                        index++;
                    }
                }
            }

            version++;
        }

        /// <summary>
        /// Removes a range of elements from the <see cref="GapBuffer{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or is equal to or greater than <see cref="Count"/>.
        /// <para>-or-</para>
        /// <paramref name="count"/> is less than 0.
        /// <para>-or-</para>
        /// <paramref name="index"/> and <paramref name="count"/> do not denote a valid range of elements in  
        /// the <see cref="GapBuffer{T}"/>. 
        /// </exception>
        public override void RemoveRange(int index, int count)
        {
            ThrowIfOutOfRange(index, count);

            // Move the gap over the index and increase the gap size
            // by the number of elements removed. Easy as pie!

            if (count > 0)
            {
                PlaceGapStart(index);
                Array.Clear(array, gapEnd, count);
                gapEnd += count;
                version++;
            }
        }

        public override void InsertSpan(int index, ReadOnlySpan<T> source)
        {
            int count = source.Length;

            if (count > 0)
            {
                EnsureGap(index, count);

                // Get reference to the gap
                Span<T> gapSpan = array.AsSpan(gapStart, GapCount);

                // Copy elements to the gap span
                source.CopyTo(gapSpan);

                gapStart += count;
            }
        }

        #endregion

		#endregion

		#region ToString

		public override string ToString()
        {
            if (this is GapBuffer<char> charBuffer)
            {
                return new string(charBuffer.array);
            }

            return GetType().ToString();
        }

        #endregion
    }
}
