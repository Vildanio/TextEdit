using TextEdit.Text;

namespace TextEdit.Line
{
	/// <summary>
	/// Represents a position between characters or the start and the end positions in <see cref="ILineDocument"/>
	/// </summary>
	public readonly record struct LineHit
	{
		/// <summary>
		/// Gets index of line
		/// </summary>
		public readonly int LineIndex { get; init; }

		/// <summary>
		/// Gets <see cref="Text.TextHit"/> in the line at the <see cref="LineIndex"/>
		/// </summary>
		public readonly TextHit TextHit { get; init; }

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LineHit"/> struct
		/// </summary>
		/// <param name="lineIndex"></param>
		public LineHit(int lineIndex)
			: this(lineIndex, default)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LineHit"/> record
		/// </summary>
		/// <param name="lineIndex"></param>
		/// <param name="textHit"></param>
		public LineHit(int lineIndex, TextHit textHit)
		{
			ThrowHelper.ThrowIfNegative(lineIndex);

			LineIndex = lineIndex;
			TextHit = textHit;
		}

		#endregion

		#region Operators

		public static bool operator <(LineHit left, LineHit right)
		{
			if (left.LineIndex < right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.TextHit < right.TextHit;
			}

			return false;
		}

		public static bool operator >(LineHit left, LineHit right)
		{
			if (left.LineIndex > right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.TextHit > right.TextHit;
			}

			return false;
		}

		public static bool operator <=(LineHit left, LineHit right)
		{
			if (left.LineIndex < right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.TextHit <= right.TextHit;
			}

			return false;
		}

		public static bool operator >=(LineHit left, LineHit right)
		{
			if (left.LineIndex > right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.TextHit >= right.TextHit;
			}

			return false;
		}

		#endregion
	}
}