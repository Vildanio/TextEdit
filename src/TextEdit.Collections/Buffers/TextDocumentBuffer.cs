using System.Collections;
using TextEdit.Text;

namespace TextEdit.Collections
{
	/// <summary>
	/// Implements <see cref="IBuffer{T}"/> through <see cref="ITextDocument"/>
	/// </summary>
	internal sealed class TextDocumentBuffer : IBuffer<char>
	{
		private readonly ITextDocument textDocument;

		public TextDocumentBuffer(ITextDocument textDocument)
		{
			this.textDocument = textDocument;
		}

		public char this[int index]
		{
			get
			{
				return textDocument[index];
			}

			set
			{
				textDocument[index] = value;
			}
		}

		public int Count => textDocument.Count;

		public bool IsReadOnly => textDocument.IsReadOnly;

		public void Add(char item)
		{
			textDocument.Add(item);
		}

		public ReadOnlyMemory<char> AsMemory(int start, int count)
		{
			return textDocument.AsMemory(start, count);
		}

		public ReadOnlySpan<char> AsSpan(int start, int count)
		{
			return textDocument.AsSpan(start, count);
		}

		public void Clear()
		{
			textDocument.Clear();
		}

		public bool Contains(char item)
		{
			return textDocument.Contains(item);
		}

		public void CopyTo(int index, int count, Span<char> span)
		{
			textDocument.CopyTo(index, count, span);
		}

		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			textDocument.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		public void CopyTo(char[] array, int arrayIndex)
		{
			textDocument.CopyTo(array, arrayIndex);
		}

		public IEnumerator<char> GetEnumerator()
		{
			return textDocument.GetEnumerator();
		}

		public int IndexOf(char value, int startIndex)
		{
			return textDocument.IndexOf(value, startIndex);
		}

		public int IndexOf(ReadOnlySpan<char> value, int startIndex)
		{
			return textDocument.IndexOf(value, startIndex);
		}

		public int IndexOf(char item)
		{
			return textDocument.IndexOf(item);
		}

		public int IndexOfAny(char value0, char value1, int startIndex)
		{
			return textDocument.IndexOfAny(value0, value1, startIndex);
		}

		public int IndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return textDocument.IndexOfAny(value0, value1, value2, startIndex);
		}

		public int IndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return textDocument.IndexOfAny(items, startIndex);
		}

		public int IndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return textDocument.IndexOfAny(items, startIndex);
		}

		public void Insert(int index, char item)
		{
			textDocument.Insert(index, item);
		}

		public void InsertRange(int index, IEnumerable<char> enumerable)
		{
			textDocument.InsertRange(index, enumerable);
		}

		public void InsertSpan(int index, ReadOnlySpan<char> span)
		{
			textDocument.InsertSpan(index, span);
		}

		public int LastIndexOf(char value, int startIndex)
		{
			return textDocument.LastIndexOf(value, startIndex);
		}

		public int LastIndexOf(ReadOnlySpan<char> value, int startIndex)
		{
			return textDocument.LastIndexOf(value, startIndex);
		}

		public int LastIndexOfAny(char value0, char value1, int startIndex)
		{
			return textDocument.LastIndexOfAny(value0, value1, startIndex);
		}

		public int LastIndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return textDocument.LastIndexOfAny(value0, value1, value2, startIndex);
		}

		public int LastIndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return textDocument.LastIndexOfAny(items, startIndex);
		}

		public int LastIndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return textDocument.LastIndexOfAny(items, startIndex);
		}

		public bool Remove(char item)
		{
			return textDocument.Remove(item);
		}

		public void RemoveAt(int index)
		{
			textDocument.RemoveAt(index);
		}

		public void RemoveRange(int index, int count)
		{
			textDocument.RemoveRange(index, count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return textDocument.GetEnumerator();
		}
	}
}
