using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace TextEdit.Collections
{
	/// <summary>
	/// Limited stack.
	/// </summary>
	public class LimitedStack<T> : IReadOnlyCollection<T>, ICollection
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LimitedStack{T}"/> class
		/// </summary>
		/// <param name="maxItemCount">Maximum length of stack</param>
		public LimitedStack(int maxItemCount)
		{
			items = new T[maxItemCount];
			count = 0;
			start = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LimitedStack{T}"/> class
		/// </summary>
		/// <param name="items">The array to copy elements from</param>
		/// <param name="maxItemCount">Maximum length of stack</param>
		/// <exception cref="ArgumentException"></exception>
		public LimitedStack(T[] items, int maxItemCount)
		{
			if (items.Length > maxItemCount)
				throw new ArgumentException(null, nameof(items));

			this.items = new T[items.Length];
			items.CopyTo(this.items, 0);

			start = 0;
			count = items.Length;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LimitedStack{T}"/> class
		/// </summary>
		/// <param name="collection">The collection to copy elements from</param>
		/// <param name="maxItemCount">Maximum length of stack</param>
		/// <exception cref="ArgumentException"></exception>
		public LimitedStack(IEnumerable<T> collection, int maxItemCount)
		{
			if (collection.TryGetNonEnumeratedCount(out int collectionCount) && collectionCount > maxItemCount)
				throw new ArgumentException(null, nameof(collection));

			T[] items = collection.ToArray();

			if (items.Length > maxItemCount)
				throw new ArgumentException(null, nameof(collection));

			this.start = 0;
			this.items = items;
			this.count = items.Length;
		}

		#endregion

		#region Stack

		private T[] items;
		private int count;
		private int start;
		private int version;

		/// <summary>
		/// Max stack length
		/// </summary>
		public int MaxItemCount => items.Length;

		/// <summary>
		/// Current length of stack
		/// </summary>
		public int Count => count;

		private int LastIndex => (start + count - 1) % items.Length;

		/// <summary>
		/// Pop item
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T Pop()
		{
			if (count == 0)
			{
				throw new InvalidOperationException("Stack is empty");
			}

			int i = LastIndex;
			T item = items[i];
			items[i] = default!;

			count--;
			version++;

			return item;
		}

		/// <summary>
		/// Peek item
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T Peek()
		{
			if (count == 0)
			{
				throw new InvalidOperationException("Stack is empty");
			}

			return items[LastIndex];
		}

		/// <summary>
		/// Push item
		/// </summary>
		public void Push(T item)
		{
			if (count == items.Length)
			{
				start = (start + 1) % items.Length;
			}
			else
			{
				count++;
			}

			items[LastIndex] = item;
			version++;
		}

		/// <summary>
		/// <see cref="Pop"/> if stack is not empty
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryPop([MaybeNullWhen(false)] out T value)
		{
			if (count > 0)
			{
				value = Pop();
				return true;
			}

			value = default!;
			return false;
		}

		/// <summary>
		/// <see cref="Peek"/> if stack is not empty
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryPeek([MaybeNullWhen(false)] out T value)
		{
			if (count > 0)
			{
				value = Peek();
				return true;
			}

			value = default!;
			return false;
		}

		/// <summary>
		/// Clear stack
		/// </summary>
		public void Clear()
		{
			items = new T[items.Length];
			count = 0;
			start = 0;
			version++;
		}

		#endregion

		#region ICollection

		public void CopyTo(Array array, int index)
		{
			items.CopyTo(array, index);
		}

		public bool IsSynchronized => false;

		public object SyncRoot => this;

		#endregion

		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			int enumeratingVersion = version;

			for (int i = count - 1; i >= 0; i--)
			{
				if (enumeratingVersion != version)
					throw new InvalidOperationException("LimitedStack was changed");

				yield return items[i];
			}
		}

		#endregion
	}
}