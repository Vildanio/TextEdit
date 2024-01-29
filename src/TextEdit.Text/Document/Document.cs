using System.Collections;

namespace TextEdit.Text
{
	public class Document : ITextDocument, IText
	{
		#region Static

		public static Document GapDocument()
		{
			return CreateUnsafe(new GapText());
		}

		public static Document GapDocument(string text)
		{
			return CreateUnsafe(new GapText(text));
		}

		public static Document StringBuilderDocument()
		{
			return CreateUnsafe(new StringBuilderText());
		}

		public static Document StringBuilderDocument(string text)
		{
			return CreateUnsafe(new StringBuilderText(text));
		}

		public static Document CreateUnsafe(IText text)
		{
			return new Document(text, default);
		}

		#endregion

		private readonly IText text;

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Document"/> class
		/// </summary>
		/// <param name="text"></param>
		public Document(IText text)
			: this(text.Clone(), default)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Document"/> class
		/// </summary>
		/// <param name="text"></param>
		private Document(IText text, bool stub)
		{
			this.text = text;
		}

		#endregion

		#region ITextDocument

		#region IText

		#region IReadOnlyText

		#region IReadOnlyList

		public char this[int index]
		{
			get
			{
				return text[index];
			}
			set
			{
				if (!IsReadOnly && CharReplaced is not null)
				{
					char oldChar = this[index];

					text[index] = value;

					CharReplaced.Invoke(this, new CharReplacedEventArgs(index, oldChar, value));
				}
				else
				{
					text[index] = value;
				}
			}
		}

		public int Count => text.Count;

		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return text.GetEnumerator();
		}

		public IEnumerator<char> GetEnumerator()
		{
			return text.GetEnumerator();
		}

		#endregion

		#endregion

		public ReadOnlyMemory<char> AsMemory(int start, int count)
		{
			return text.AsMemory(start, count);
		}

		public ReadOnlySpan<char> AsSpan(int start, int count)
		{
			return text.AsSpan(start, count);
		}

		public string AsString(int start, int count)
		{
			return text.AsString(start, count);
		}

		public bool Contains(ReadOnlySpan<char> value)
		{
			return text.Contains(value);
		}

		public bool Contains(char item)
		{
			return text.Contains(item);
		}

		public void CopyTo(int index, int count, Span<char> span)
		{
			text.CopyTo(index, count, span);
		}

		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			text.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		public void CopyTo(char[] array, int arrayIndex)
		{
			text.CopyTo(array, arrayIndex);
		}

		public int IndexOf(char value, int startIndex)
		{
			return text.IndexOf(value, startIndex);
		}

		public int IndexOf(ReadOnlySpan<char> value, int startIndex)
		{
			return text.IndexOf(value, startIndex);
		}

		public int IndexOf(char item)
		{
			return text.IndexOf(item);
		}

		public int IndexOfAny(char value0, char value1, int startIndex)
		{
			return text.IndexOfAny(value0, value1, startIndex);
		}

		public int IndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return text.IndexOfAny(value0, value1, value2, startIndex);
		}

		public int IndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return text.IndexOfAny(items, startIndex);
		}

		public int IndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return text.IndexOfAny(items, startIndex);
		}

		public int LastIndexOf(char value, int startIndex)
		{
			return text.LastIndexOf(value, startIndex);
		}

		public int LastIndexOf(ReadOnlySpan<char> value, int startIndex)
		{
			return text.LastIndexOf(value, startIndex);
		}

		public int LastIndexOfAny(char value0, char value1, int startIndex)
		{
			return text.LastIndexOfAny(value0, value1, startIndex);
		}

		public int LastIndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return text.LastIndexOfAny(value0, value1, value2, startIndex);
		}

		public int LastIndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return text.LastIndexOfAny(items, startIndex);
		}

		public int LastIndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return text.LastIndexOfAny(items, startIndex);
		}

		#endregion

		#region IList

		#region ICollection

		public bool IsReadOnly => text.IsReadOnly;

		public void Add(char item)
		{
			Insert(Count, item);
		}

		public bool Remove(char item)
		{
			int index = IndexOf(item);

			if (index >= 0)
			{
				RemoveAt(index);

				return true;
			}

			return false;
		}

		public void Clear()
		{
			if (!IsReadOnly && CharRangeRemoved is not null)
			{
				// Objects in event args should me immutable
				string chars = this.ToString();

				text.Clear();

				CharRangeRemoved.Invoke(this, new CharRangeRemovedEventArgs(0, chars));
			}
			else
			{
				text.Clear();
			}
		}

		#endregion

		public void Insert(int index, char item)
		{
			text.Insert(index, item);

			if (!IsReadOnly)
			{
				CharInserted?.Invoke(this, new CharInsertedEventArgs(index, item));
			}
		}

		public void RemoveAt(int index)
		{
			if (!IsReadOnly && CharRemoved is not null)
			{
				char character = this[index];

				text.RemoveAt(index);

				CharRemoved.Invoke(this, new CharRemovedEventArgs(index, character));
			}
			else
			{
				text.RemoveAt(index);
			}
		}

		#endregion

		public void RemoveRange(int index, int count)
		{
			if (!IsReadOnly && CharRangeRemoved is not null)
			{
				// Objects in event args should me immutable
				string chars = AsString(index, count);

				text.RemoveRange(index, count);

				CharRangeRemoved.Invoke(this, new CharRangeRemovedEventArgs(index, chars));
			}
			else
			{
				text.RemoveRange(index, count);
			}
		}

		public void InsertRange(int index, IEnumerable<char> enumerable)
		{
			text.InsertRange(index, enumerable);

			if (!IsReadOnly)
			{
				CharRangeInserted?.Invoke(this, new CharRangeInsertedEventArgs(index, string.Concat(enumerable)));
			}
		}

		public void InsertSpan(int index, ReadOnlySpan<char> span)
		{
			text.InsertSpan(index, span);

			if (!IsReadOnly)
			{
				CharRangeInserted?.Invoke(this, new CharRangeInsertedEventArgs(index, span.ToString()));
			}
		}

		public void InsertString(int index, string value)
		{
			InsertSpan(index, value);
		}

		IText IText.Clone()
		{
			return new Document(text);
		}

		#endregion

		public ITextDocument Clone()
		{
			return new Document(text);
		}

		public event EventHandler<ICharRemovedEventArgs>? CharRemoved;
		public event EventHandler<ICharReplacedEventArgs>? CharReplaced;
		public event EventHandler<ICharInsertedEventArgs>? CharInserted;
		public event EventHandler<ICharRangeRemovedEventArgs>? CharRangeRemoved;
		public event EventHandler<ICharRangeInsertedEventArgs>? CharRangeInserted;

		#endregion

		#region Object

		public override string ToString()
		{
			return text.ToString();
		}

		#endregion
	}
}