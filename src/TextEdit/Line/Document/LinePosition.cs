namespace TextEdit.Line
{
	/// <summary>
	/// Represents a position between characters or the start and the end positions in <see cref="ILineDocument"/>
	/// </summary>
	public readonly record struct LinePosition
	{
		/// <summary>
		/// Gets index of line which character got hit
		/// </summary>
		public readonly int LineIndex { get; init; }

		/// <summary>
		/// Gets information about character that got hit in line at <see cref="LineIndex"/>
		/// </summary>
		public readonly int CharacterIndex { get; init; }

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LinePosition"/> struct
		/// </summary>
		/// <param name="lineIndex"></param>
		public LinePosition(int lineIndex)
			: this(lineIndex, default)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LinePosition"/> record
		/// </summary>
		/// <param name="lineIndex"></param>
		/// <param name="characterIndex"></param>
		public LinePosition(int lineIndex, int characterIndex)
		{
			ThrowHelper.ThrowIfNegative(lineIndex);

			LineIndex = lineIndex;
			CharacterIndex = characterIndex;
		}

		#endregion

		#region Operators

		public static bool operator <(LinePosition left, LinePosition right)
		{
			if (left.LineIndex < right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.CharacterIndex < right.CharacterIndex;
			}

			return false;
		}

		public static bool operator >(LinePosition left, LinePosition right)
		{
			if (left.LineIndex > right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.CharacterIndex > right.CharacterIndex;
			}

			return false;
		}

		public static bool operator <=(LinePosition left, LinePosition right)
		{
			if (left.LineIndex < right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.CharacterIndex <= right.CharacterIndex;
			}

			return false;
		}

		public static bool operator >=(LinePosition left, LinePosition right)
		{
			if (left.LineIndex > right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.CharacterIndex >= right.CharacterIndex;
			}

			return false;
		}

		#endregion
	}
}