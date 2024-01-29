namespace TextEdit.Collections
{
	public class ImmutableArrayBuffer<T> : ImmutableBuffer<T>, IEquatable<ImmutableArrayBuffer<T>>
	{
		public static ImmutableArrayBuffer<T> Empty { get; } = new ImmutableArrayBuffer<T>(Array.Empty<T>());

		private readonly T[] items;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableArrayBuffer{T}"/>
		/// </summary>
		/// <param name="enumerable"></param>
		public ImmutableArrayBuffer(IEnumerable<T> enumerable)
			: this(enumerable.ToArray())
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImmutableArrayBuffer{T}"/>
		/// </summary>
		/// <param name="array"></param>
		private ImmutableArrayBuffer(T[] array)
		{
			this.items = array;
		}

		#endregion

		#region IReadOnlyBuffer

		#region IReadOnlyList

		#region IEnumerable

		public override IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)items).GetEnumerator();
		}

		#endregion

		protected override T GetItem(int index) => items[index];

		public override int Count => items.Length;

		#endregion

		public override ReadOnlyMemory<T> AsMemory(int start, int count)
		{
			return items.AsMemory(start, count);
		}

		public override ReadOnlySpan<T> AsSpan(int start, int count)
		{
			return items.AsSpan(start, count);
		}

		public override void CopyTo(int index, int count, Span<T> span)
		{
			AsSpan(index, count).CopyTo(span);
		}

		public override void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int count)
		{
			Array.Copy(items, sourceIndex, destination, destinationIndex, count);
		}

		public override int IndexOf(T value, int startIndex)
		{
			return Array.IndexOf(items, value, startIndex);
		}

		public override int LastIndexOf(T value, int startIndex)
		{
			return Array.LastIndexOf(items, value, startIndex);
		}

		// TODO: Implement these methods
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

		#endregion

		#region Object

		public bool Equals(ImmutableArrayBuffer<T>? other)
		{
			return other is not null && this.items == other.items;
		}

		public override bool Equals(object? obj)
		{
			return obj is ImmutableArrayBuffer<T> stringBuilderBuffer && this.Equals(stringBuilderBuffer);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(items);
		}

		public override string ToString()
		{
			if (this is ImmutableArrayBuffer<char>)
			{
				return items.AsSpan().ToString();
			}

			return GetType().ToString();
		}

		#endregion
	}
}
