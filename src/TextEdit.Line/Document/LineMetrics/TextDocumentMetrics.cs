using TextEdit.Text;
using TextEdit.Utils;

namespace TextEdit.Line
{
	public class TextDocumentMetrics : ILineMetrics
	{
		private readonly ITextDocument document;
		private readonly LineLengthsMetrics lineMetrics;
		private readonly ListLineEndingProvider lineEndings;

		#region Static

		/// <summary>
		/// Creates <see cref="TextDocumentMetrics"/> from <paramref name="document"/> and also handles its changes
		/// </summary>
		/// <param name="document"></param>
		/// <returns></returns>
		public static TextDocumentMetrics GetMetrics(ITextDocument document)
		{
			ListLineEndingProvider lineEndings = new ListLineEndingProvider(document.Count);
			LineLengthsMetrics lineOffsets = new LineLengthsMetrics(document.Count, lineEndings);

			var textLineEnumerator = document.AsBuffer().GetTextLineEnumerator(0, document.Count);

			while (textLineEnumerator.MoveNext())
			{
				lineOffsets.Add(textLineEnumerator.LineLength);
				lineEndings.Add(textLineEnumerator.LineEndingLength);
			}

			return new TextDocumentMetrics(document, lineOffsets, lineEndings);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TextDocumentMetrics"/> class
		/// </summary>
		/// <param name="document"></param>
		internal TextDocumentMetrics(ITextDocument document, LineLengthsMetrics lineMetrics, ListLineEndingProvider lineEndings)
		{
			this.document = document;
			this.lineMetrics = lineMetrics;
			this.lineEndings = lineEndings;

			// Subscribe to the events
			document.CharRemoved += Document_CharRemoved;
			document.CharInserted += Document_CharInserted;
			document.CharReplaced += Document_CharReplaced;
			document.CharRangeRemoved += Document_CharRangeRemoved;
			document.CharRangeInserted += Document_CharRangeInserted;
		}

		#endregion

		#region Event handlers

		private void Document_CharRemoved(object? sender, ICharRemovedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void Document_CharReplaced(object? sender, ICharReplacedEventArgs e)
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

		#endregion

		#region ILineMetrics

		public int Count => lineMetrics.Count;

		public LinePosition GetLinePositionFromOffset(int offset)
		{
			return lineMetrics.GetLinePositionFromOffset(offset);
		}

		public int GetLineLength(int index)
		{
			return lineMetrics.GetLineLength(index);
		}

		public (int Start, int End) GetLineBounds(int index)
		{
			return lineMetrics.GetLineBounds(index);
		}

		public int GetOffsetFromPosition(LinePosition position)
		{
			return lineMetrics.GetOffsetFromPosition(position);
		}

		public int GetLineEndFromIndex(int index)
		{
			return lineMetrics.GetLineEndFromIndex(index);
		}

		public int GetLineEndFromOffset(int offset)
		{
			return lineMetrics.GetLineEndFromOffset(offset);
		}

		public int GetLineStartFromIndex(int index)
		{
			return lineMetrics.GetLineStartFromIndex(index);
		}

		public int GetLineStartFromOffset(int offset)
		{
			return lineMetrics.GetLineStartFromOffset(offset);
		}

		public int GetLineAtOffset(int offset)
		{
			return lineMetrics.GetLineAtOffset(offset);
		}

		public byte GetLineTerminatorLength(int index)
		{
			return lineMetrics.GetLineTerminatorLength(index);
		}

		#endregion
	}
}
