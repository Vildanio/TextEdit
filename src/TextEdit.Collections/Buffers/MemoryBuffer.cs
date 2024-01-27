using System.Runtime.InteropServices;

namespace TextEdit.Collections
{
	/// <summary>
	/// <see cref="IReadOnlyBuffer{T}"/> implemented through <see cref="ReadOnlyMemory{T}"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class MemoryBuffer<T> : ImmutableBuffer<T>, IEquatable<MemoryBuffer<T>>
        where T : IEquatable<T>
    {
		#region Static

		public static MemoryBuffer<T> Empty { get; } = new MemoryBuffer<T>(default, default);

		/// <summary>
		/// Creates <see cref="MemoryBuffer{T}"/> and uses the given <paramref name="memory"/> without copy.
		/// </summary>
		/// <param name="memory"></param>
		/// <returns></returns>
		public static MemoryBuffer<T> CreateUnsafe(ReadOnlyMemory<T> memory)
        {
            return new MemoryBuffer<T>(memory, default);
        }

		#endregion

		public ReadOnlyMemory<T> Memory { get; }

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MemoryBuffer{T}"/> class.
		/// </summary>
		/// <param name="memory"></param>
		public MemoryBuffer(ReadOnlyMemory<T> memory)
            : this(memory.ToArray().AsMemory(), default)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryBuffer{T}"/> class
        /// </summary>
        /// <param name="memory"></param>
		private MemoryBuffer(ReadOnlyMemory<T> memory, bool stub)
        {
            this.Memory = memory;
        }

		#endregion

		#region IReadOnlyBuffer

		#region IReadOnlyList

		#region IEnumerable

		public override IEnumerator<T> GetEnumerator()
		{
			return MemoryMarshal.ToEnumerable(Memory).GetEnumerator();
		}

		#endregion

		public override int Count => Memory.Length;

		protected override T GetItem(int index) => Memory.Span[index];

		#endregion

		public override ReadOnlyMemory<T> AsMemory(int start, int count)
        {
            return Memory.Slice(start, count);
        }

		public override ReadOnlySpan<T> AsSpan(int start, int count)
        {
            return Memory.Span.Slice(start, count);
        }

		public override void CopyTo(int index, int count, Span<T> span)
        {
            Memory.Span.Slice(index, count).CopyTo(span);
        }

		public override void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int count)
        {
            Span<T> destinationSpan = destination.AsSpan(destinationIndex, count);

            Memory.Span.Slice(sourceIndex, count).CopyTo(destinationSpan);
        }

		public override int IndexOf(T value, int startIndex)
        {
            return Memory.Span.Slice(startIndex).IndexOf(value);
        }

		public override int IndexOf(ReadOnlySpan<T> value, int startIndex)
        {
            return Memory.Span.Slice(startIndex).IndexOf(value);
        }

		public override int LastIndexOf(T value, int startIndex)
        {
            return Memory.Span.Slice(0, startIndex).LastIndexOf(value);
        }

		public override int LastIndexOf(ReadOnlySpan<T> value, int startIndex)
        {
            return Memory.Span.Slice(0, startIndex).LastIndexOf(value);
        }

		public override int IndexOfAny(T value0, T value1, int startIndex)
        {
            return Memory.Span.Slice(startIndex).IndexOfAny(value0, value1);
        }

		public override int IndexOfAny(T value0, T value1, T value2, int startIndex)
        {
            return Memory.Span.Slice(startIndex).IndexOfAny(value0, value1, value2);
        }

		public override int IndexOfAny(IEnumerable<T> items, int startIndex)
        {
            // TODO: Optimize
            return Memory.Span.Slice(startIndex).IndexOfAny(items.ToArray().AsSpan());
        }

		public override int IndexOfAny(ReadOnlySpan<T> items, int startIndex)
        {
            return Memory.Span.Slice(startIndex).IndexOfAny(items);
        }

		public override int LastIndexOfAny(T value0, T value1, int startIndex)
        {
            return Memory.Span.Slice(startIndex).LastIndexOfAny(value0, value1);
        }

		public override int LastIndexOfAny(T value0, T value1, T value2, int startIndex)
        {
            return Memory.Span.Slice(startIndex).LastIndexOfAny(value0, value1, value2);
        }

		public override int LastIndexOfAny(IEnumerable<T> items, int startIndex)
        {
            // TODO: Optimize
            return Memory.Span.Slice(startIndex).LastIndexOfAny(items.ToArray().AsSpan());
        }

		public override int LastIndexOfAny(ReadOnlySpan<T> items, int startIndex)
        {
            return Memory.Span.Slice(startIndex).LastIndexOfAny(items);
        }

		#endregion

		#region Object

		public bool Equals(MemoryBuffer<T>? other)
		{
			return other is not null && this.Memory.Equals(other.Memory);
		}

		public override bool Equals(object? obj)
		{
			return obj is MemoryBuffer<T> stringBuilderBuffer && this.Equals(stringBuilderBuffer);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Memory);
		}

		public override string ToString()
		{
			if (this is MemoryBuffer<char>)
			{
				return Memory.ToString();
			}

			return GetType().ToString();
		}

		#endregion
	}
}
