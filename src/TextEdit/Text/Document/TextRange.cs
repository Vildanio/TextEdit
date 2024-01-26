namespace TextEdit.Text
{
	/// <summary>
	/// Represents a range of characters within a <see cref="ITextDocument"/>
	/// </summary>
	public record TextRange
	{
		/// <summary>
		/// Gets <see cref="TextRange"/> instance which <see cref="Start"/> and <see cref="End"/> positions set default value
		/// </summary>
		public static TextRange Default { get; } = new TextRange();

		/// <summary>
		/// Gets value indicating whether the <see cref="Start"/> and <see cref="End"/> are equal or not
		/// </summary>
		public bool IsEmpty => Start == End;

		/// <summary>
		/// Gets start position of the range
		/// </summary>
		/// <remarks>
		/// Note that value of this property can be greater than <see cref="End"/>
		/// </remarks>
		public int Start { get; init; }

		/// <summary>
		/// Gets end position of the range
		/// </summary>
		/// <remarks>
		/// Note that value of this property can be less than <see cref="Start"/>
		/// </remarks>
		public int End { get; init; }

		#region Methods

		/// <summary>
		/// Gets value indicating whether the given <paramref name="position"/> within <see cref="Start"/> and <see cref="End"/>
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool Contains(int position)
		{
			return Start <= position && End >= position;
		}

		/// <summary>
		/// Gets <see cref="TextRange"/> which <see cref="Start"/> is less than <see cref="End"/>
		/// </summary>
		/// <returns></returns>
		public TextRange Normalized()
		{
			if (Start > End)
			{
				return new TextRange(End, Start);
			}

			return this;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initalizes a new instance of the <see cref="TextRange"/> class
		/// </summary>
		public TextRange()
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="TextRange"/> class
		/// </summary>
		/// <param name="position"></param>
		public TextRange(int position)
			: this(position, position)
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="TextRange"/> class
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public TextRange(int start, int end)
		{
			Start = start;
			End = end;
		}

		#endregion
	}
}
