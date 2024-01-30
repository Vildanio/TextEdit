using TextEdit.Collections;
using TextEdit.Text;

namespace TextEdit.Line
{
	/// <summary>
	/// Implements <see cref="ILineDocument"/> through <see cref="ITextDocument"/>
	/// </summary>
	public class VirtualLineDocument : Buffer<string>, ILineDocument
	{
		private readonly ITextDocument document;
		private readonly ILineMetrics lineMetrics;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="VirtualLineDocument"/> class
		/// </summary>
		/// <param name="document"></param>
		public VirtualLineDocument(ITextDocument document, ILineMetrics lineMetrics)
		{
			this.document = document;
			this.lineMetrics = lineMetrics;

			this.document.CharInserted += Document_CharInserted;
			this.document.CharRemoved += Document_CharRemoved;
			this.document.CharReplaced += Document_CharReplaced;
			this.document.CharRangeRemoved += Document_CharRangeRemoved;
			this.document.CharRangeInserted += Document_CharRangeInserted;
		}

		private void Document_CharRemoved(object? sender, ICharRemovedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Document_CharInserted(object? sender, ICharInsertedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Document_CharRangeRemoved(object? sender, ICharRangeRemovedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Document_CharRangeInserted(object? sender, ICharRangeInsertedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Document_CharReplaced(object? sender, ICharReplacedEventArgs e)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ILineDocument

		#region Enumerable

		public override IEnumerator<string> GetEnumerator()
		{
			return EnumerateLines(0, lineMetrics.Count).GetEnumerator();
		}

		#endregion

		public override int IndexOf(string value, int startIndex)
		{
			return ToArray(0, startIndex).AsSpan().IndexOf(value);
		}

		public override int IndexOf(ReadOnlySpan<string> value, int startIndex)
		{
			return ToArray(0, startIndex).AsSpan().IndexOf(value);
		}

		public override int LastIndexOf(string value, int startIndex)
		{
			return ToArray(0, startIndex).AsSpan().LastIndexOf(value);
		}

		public override int LastIndexOf(ReadOnlySpan<string> value, int startIndex)
		{
			return ToArray(0, startIndex).AsSpan().LastIndexOf(value);
		}

		public override int IndexOfAny(string value0, string value1, int startIndex)
		{
			throw new NotImplementedException();
		}

		public override int IndexOfAny(string value0, string value1, string value2, int startIndex)
		{
			throw new NotImplementedException();
		}

		public override int IndexOfAny(IEnumerable<string> items, int startIndex)
		{
			throw new NotImplementedException();
		}

		public override int IndexOfAny(ReadOnlySpan<string> items, int startIndex)
		{
			throw new NotImplementedException();
		}

		public override int LastIndexOfAny(string value0, string value1, int startIndex)
		{
			throw new NotImplementedException();
		}

		public override int LastIndexOfAny(string value0, string value1, string value2, int startIndex)
		{
			throw new NotImplementedException();
		}

		public override int LastIndexOfAny(IEnumerable<string> items, int startIndex)
		{
			throw new NotImplementedException();
		}

		public override int LastIndexOfAny(ReadOnlySpan<string> items, int startIndex)
		{
			throw new NotImplementedException();
		}

		public override void CopyTo(int index, int count, Span<string> span)
		{
			int i = 0;
			foreach (var line in EnumerateLines(index, count))
			{
				span[i] = line;

				i++;
			}
		}

		public override void CopyTo(int sourceIndex, string[] destination, int destinationIndex, int count)
		{
			int i = 0;
			foreach (var line in EnumerateLines(sourceIndex, count))
			{
				destination[i + destinationIndex] = line;

				i++;
			}
		}

		public override ReadOnlySpan<string> AsSpan(int start, int count)
		{
			return ToArray(start, count).AsSpan();
		}

		public override ReadOnlyMemory<string> AsMemory(int start, int count)
		{
			return ToArray(start, count).AsMemory();
		}

		public string[] ToArray(int start, int count)
		{
			return EnumerateLines(start, count).AsEnumerable().ToArray();
		}

		public IEnumerable<string> EnumerateLines(int index, int count)
		{
			// ### NEEDS_OPTIMIZATION
			// Add line start offsets enumerating method to the ILineMetrics

			int start = 0;
			for (int i = index; i < index + count; i++)
			{
				int end = lineMetrics.GetLineStartFromIndex(i);

				yield return document.AsString(start, end - start);

				start = end;
			}
		}

		public override int Count => lineMetrics.Count;

		public override bool IsReadOnly => document.IsReadOnly;

		public override string this[int index]
		{
			get
			{
				var bounds = lineMetrics.GetLineBounds(index);

				return document.AsString(bounds.Start, bounds.End - bounds.Start);
			}
			set
			{
				var bounds = lineMetrics.GetLineBounds(index);

				document.RemoveRange(bounds.Start, bounds.End - bounds.Start);
			}
		}

		public override void RemoveRange(int index, int count)
		{
			int startOffset = lineMetrics.GetLineStartFromIndex(index);
			int endOffset = lineMetrics.GetLineEndFromIndex(index + count);

			document.RemoveRange(startOffset, endOffset - startOffset);
		}

		public override void InsertSpan(int index, ReadOnlySpan<string> span)
		{
			// ### NEEDS_OPTIMIZATION

			int offset = lineMetrics.GetLineStartFromIndex(index);

			int length = 0;

			// Determine character count
			{
				for (int i = 0; i < span.Length; i++)
				{
					length += span[i].Length;
				}
			}

			// Allocate new memory
			Span<char> chars = new char[length].AsSpan();

			// Copy the strings to char array
			{
				int copyStart = 0;

				for (int i = 0; i < span.Length; i++)
				{
					span[i].CopyTo(chars.Slice(copyStart));
				}
			}

			document.InsertSpan(offset, chars);
		}

		public override void InsertRange(int index, IEnumerable<string> enumerable)
		{
			// ### NEEDS_OPTIMIZATION
			// Unneseccary memory allocation

			int offset = lineMetrics.GetLineStartFromIndex(index);

			string str = string.Concat(enumerable);

			document.InsertRange(offset, str);
		}

		public override void Insert(int index, string item)
		{
			int offset = lineMetrics.GetLineStartFromIndex(index);

			document.InsertString(offset, item);
		}

		public override void RemoveAt(int index)
		{
			var bounds = lineMetrics.GetLineBounds(index);

			document.RemoveRange(bounds.Start, bounds.End - bounds.Start);
		}

		public event EventHandler<ILineRemovedEventArgs>? LineRemoved;
		public event EventHandler<ILineReplacedEventArgs>? LineReplaced;
		public event EventHandler<ILineInsertedEventArgs>? LineInserted;
		public event EventHandler<ILineRangeRemovedEventArgs>? LineRangeRemoved;
		public event EventHandler<ILineRangeInsertedEventArgs>? LineRangeInserted;

		#endregion
	}
}