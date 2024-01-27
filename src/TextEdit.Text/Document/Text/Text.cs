using System.Collections;

namespace TextEdit.Text
{
	public abstract class Text : IText
	{
		#region IText

		#region IReadOnlyText

		#region IReadOnlyList

		public abstract int Count { get; }

		public abstract char this[int index] { get; set; }

		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public abstract IEnumerator<char> GetEnumerator();

		#endregion

		#endregion

		public abstract bool IsReadOnly { get; }

		public abstract ReadOnlyMemory<char> AsMemory(int start, int count);
		public abstract ReadOnlySpan<char> AsSpan(int start, int count);
		public abstract string AsString(int start, int count);
		public abstract void CopyTo(int index, int count, Span<char> span);
		public abstract void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);
		public abstract int IndexOf(char value, int startIndex);
		public abstract int IndexOf(ReadOnlySpan<char> value, int startIndex);
		public abstract int IndexOfAny(char value0, char value1, int startIndex);
		public abstract int IndexOfAny(char value0, char value1, char value2, int startIndex);
		public abstract int IndexOfAny(IEnumerable<char> items, int startIndex);
		public abstract int IndexOfAny(ReadOnlySpan<char> items, int startIndex);
		public abstract int LastIndexOf(char value, int startIndex);
		public abstract int LastIndexOf(ReadOnlySpan<char> value, int startIndex);
		public abstract int LastIndexOfAny(char value0, char value1, int startIndex);
		public abstract int LastIndexOfAny(char value0, char value1, char value2, int startIndex);
		public abstract int LastIndexOfAny(IEnumerable<char> items, int startIndex);
		public abstract int LastIndexOfAny(ReadOnlySpan<char> items, int startIndex);

		#endregion

		#region IList

		#region ICollection

		public virtual void Add(char item)
		{
			Insert(Count, item);
		}

		public virtual bool Remove(char item)
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

		public virtual bool Contains(char value)
		{
			return IndexOf(value, 0) >= 0;
		}

		public virtual bool Contains(ReadOnlySpan<char> value)
		{
			return IndexOf(value, 0) >= 0;
		}

		public virtual void CopyTo(char[] array, int arrayIndex)
		{
			ThrowHelper.ThrowIfNull(array);
			ThrowHelper.ThrowIfOutOfRange(arrayIndex, array.Length, maxValueParamName: null);

			if (array.Rank is not 1)
				throw new ArgumentException(null, nameof(array));

			CopyTo(0, array, arrayIndex, array.Length - arrayIndex);
		}

		#endregion

		public virtual int IndexOf(char item)
		{
			return IndexOf(item, 0);
		}

		public abstract void RemoveAt(int index);

		public abstract void Insert(int index, char item);

		#endregion

		public abstract IText Clone();
		public abstract void RemoveRange(int index, int count);
		public abstract void InsertRange(int index, IEnumerable<char> enumerable);
		public abstract void InsertSpan(int index, ReadOnlySpan<char> span);
		public abstract void InsertString(int index, string text);

		#endregion

		#region Object

		public override string ToString()
		{
			return AsString(0, Count);
		}

		#endregion
	}
}