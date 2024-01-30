using TextEdit.Collections;

namespace TextEdit.Line
{
	/// <summary>
	/// <see cref="ILineMetrics"/> implemented through <see cref="GapBuffer{T}"/> which stores lengths of lines
	/// </summary>
	public class LineLengthsMetrics : GapBuffer<int>, ILineMetrics
    {
        public int characterCount;
        private readonly ILineEndingProvider lineEndingProvider;

		#region ILineMetrics

		public int GetLineStartFromOffset(int offset)
        {
			if (offset < 0 || offset >= characterCount)
				throw new ArgumentOutOfRangeException(nameof(offset));

			int lineStart = 0;

			for (int i = 0; i < Count; i++)
			{
				int lineEnd = lineStart + this[i] + GetLineTerminatorLength(i);

				if (lineStart < offset && offset > lineEnd)
				{
					return lineStart;
				}

				lineStart = lineEnd;
			}

			throw new ArgumentOutOfRangeException(nameof(offset));
		}

		public int GetLineEndFromIndex(int index)
		{
			int lineStart = GetLineStartFromIndex(index);

			return lineStart + GetLineLength(lineStart);
		}

		public int GetLineEndFromOffset(int offset)
		{
			if (offset < 0 || offset >= characterCount)
				throw new ArgumentOutOfRangeException(nameof(offset));

			int lineStart = 0;

			for (int i = 0; i < Count; i++)
			{
				int lineEnd = lineStart + this[i] + GetLineTerminatorLength(i);

				if (lineStart < offset && offset > lineEnd)
				{
					return lineEnd;
				}

				lineStart = lineEnd;
			}

			throw new ArgumentOutOfRangeException(nameof(offset));
		}

		public int GetLineAtOffset(int offset)
		{
			if (offset < 0 || offset >= characterCount)
				throw new ArgumentOutOfRangeException(nameof(offset));

			int lineStart = 0;

            for (int i = 0; i < Count; i++)
            {
                int lineEnd = lineStart + this[i] + GetLineTerminatorLength(i);

                if (lineStart < offset && offset > lineEnd)
                {
                    return i;
                }

                lineStart = lineEnd;
            }

            throw new ArgumentOutOfRangeException(nameof(offset));
		}

		public int GetLineLength(int index)
        {
			return this[index];
        }

        public int GetLineStartFromIndex(int index)
        {
			int offset = 0;

			for (int i = 0; i < index; i++)
			{
				offset += this[i];
			}

			return offset + lineEndingProvider.GetLineTerminatorsLength(0, index);
		}

        public (int Start, int End) GetLineBounds(int index)
        {
            int start = GetLineStartFromIndex(index);
            int end = start + GetLineLength(index);

			return new(start, end);
        }

		public byte GetLineTerminatorLength(int index)
		{
			return lineEndingProvider.GetLineTerminatorLength(index);
		}

		public LinePosition GetLinePositionFromOffset(int offset)
        {
            if (offset < 0 || offset >= characterCount)
                throw new ArgumentOutOfRangeException(nameof(offset));

            int passed = 0;

            for (int i = 0; i < Count; i++)
            {
                int length = this[i] + lineEndingProvider.GetLineTerminatorLength(i);

                if (passed <= offset && offset <= passed + length)
                {
                    int lineOffset = offset - passed;

                    return new LinePosition(i, lineOffset);
                }

                passed += length;
            }

            throw new ArgumentException(null, nameof(offset));
        }

        public int GetOffsetFromPosition(LinePosition position)
        {
			if (position.LineIndex < 0 || position.LineIndex >= Count)
				throw new ArgumentOutOfRangeException(nameof(position));

			int lineOffset = GetLineStartFromIndex(position.LineIndex);

            return lineOffset + position.CharacterIndex;
        }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LineLengthsMetrics"/> class
		/// </summary>
		internal LineLengthsMetrics(ILineEndingProvider lineEndingProvider)
            : this(0, lineEndingProvider)
        {

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="LineLengthsMetrics"/> class
		/// </summary>
		internal LineLengthsMetrics(int characterCount, ILineEndingProvider lineEndingProvider)
            : base()
        {
            this.characterCount = characterCount;
            this.lineEndingProvider = lineEndingProvider;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="LineLengthsMetrics"/> class
		/// </summary>
		/// <param name="capacity"></param>
		internal LineLengthsMetrics(int capacity, int characterCount, ILineEndingProvider lineEndingProvider)
            : base(capacity)
        {
            this.characterCount = characterCount;
            this.lineEndingProvider = lineEndingProvider;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="LineLengthsMetrics"/> class
		/// </summary>
		/// <param name="enumerable"></param>
		internal LineLengthsMetrics(IEnumerable<int> enumerable, int characterEnd, ILineEndingProvider lineEndingProvider)
            : base(enumerable)
        {
            this.characterCount = characterEnd;
            this.lineEndingProvider = lineEndingProvider;
        }

        #endregion
    }
}