namespace TextEdit.Visual
{
	/// <summary>
	/// Represents a position between columns or the start and the end positions in <see cref="IVisualDocument"/>
	/// </summary>
	public readonly record struct VisualLineHit
	{
		/// <summary>
		/// Gets index of line
		/// </summary>
		public readonly int LineIndex { get; init; }

		/// <summary>
		/// Gets information about character that got hit in line at <see cref="LineIndex"/>
		/// </summary>
		public readonly VisualHit VisualHit { get; init; }

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="VisualLineHit"/> struct
		/// </summary>
		/// <param name="lineIndex"></param>
		public VisualLineHit(int lineIndex)
			: this(lineIndex, default)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VisualLineHit"/> record
		/// </summary>
		/// <param name="lineIndex"></param>
		/// <param name="visualHit"></param>
		public VisualLineHit(int lineIndex, VisualHit visualHit)
		{
			ThrowHelper.ThrowIfNegative(lineIndex);

			LineIndex = lineIndex;
			VisualHit = visualHit;
		}

		#endregion

		#region Operators

		public static bool operator <(VisualLineHit left, VisualLineHit right)
		{
			if (left.LineIndex < right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.VisualHit < right.VisualHit;
			}

			return false;
		}

		public static bool operator >(VisualLineHit left, VisualLineHit right)
		{
			if (left.LineIndex > right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.VisualHit > right.VisualHit;
			}

			return false;
		}

		public static bool operator <=(VisualLineHit left, VisualLineHit right)
		{
			if (left.LineIndex < right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.VisualHit <= right.VisualHit;
			}

			return false;
		}

		public static bool operator >=(VisualLineHit left, VisualLineHit right)
		{
			if (left.LineIndex > right.LineIndex)
			{
				return true;
			}
			else if (left.LineIndex == right.LineIndex)
			{
				return left.VisualHit >= right.VisualHit;
			}

			return false;
		}

		#endregion
	}
}