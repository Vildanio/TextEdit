using TextEdit.Collections;

namespace TextEdit.Utils
{
	public class TextLineEnumerator
	{
		private const char CR = '\r';
		private const char LF = '\n';

		private int lineEnd;
		private int lineStart;
		private int lineLength;
		private byte lineEndingLength;
		private readonly int initOffset;
		private readonly int maxOffset;
		private readonly IReadOnlyBuffer<char> text;

		public TextLineEnumerator(IReadOnlyBuffer<char> text, int offset, int count)
		{
			ThrowHelper.ThrowIfNull(text);
			ThrowHelper.ThrowIfOutOfRange(offset, count, text.Count, maxCountParamName: null);

			this.text = text;
			this.lineEnd = offset;
			this.initOffset = offset;
			this.maxOffset = offset + count;
		}

		public int LineStart => lineStart;

		public int LineLength => lineLength;

		public int LineEnd => lineEnd;

		public int LineEndAfterEnding => lineEnd + lineEndingLength;

		public byte LineEndingLength => lineEndingLength;

		public void Reset()
		{
			lineEnd = initOffset;
			lineStart = initOffset;
			lineLength = 0;
			lineEndingLength = 0;
		}

		public bool MoveNext()
		{
			if (LineEndAfterEnding < maxOffset)
			{
				int beforeNewLine = text.IndexOfAny(CR, LF, LineEndAfterEnding);

				if (beforeNewLine >= 0)
				{
					// CRLF
					if (text[beforeNewLine] is CR && beforeNewLine + 1 < text.Count && text[beforeNewLine + 1] is LF)
					{
						lineEndingLength = 2;
					}

					// CR/LF or any other unicode whitespace
					else
					{
						lineEndingLength = 1;
					}

					if (lineEnd == initOffset)
					{
						lineStart = 0;
						lineEnd = beforeNewLine;
						lineLength = beforeNewLine;
					}
					else
					{
						lineLength = beforeNewLine - lineEnd - lineEndingLength;
						lineEnd = beforeNewLine;
						lineStart = lineEnd - lineLength;
					}

					return true;
				}
			}

			return false;
		}
	}
}
