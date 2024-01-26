namespace TextEdit.Line
{
	/// <summary>
	/// Represents a range of characters within a <see cref="ILineDocument"/>
	/// </summary>
	public record LineRange
	{
		/// <summary>
		/// Gets <see cref="LineRange"/> instance which <see cref="Start"/> and <see cref="End"/> positions set default value
		/// </summary>
		public static LineRange Default { get; } = new LineRange();

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
		public LinePosition Start { get; init; }

		/// <summary>
		/// Gets end position of the range
		/// </summary>
		/// <remarks>
		/// Note that value of this property can be less than <see cref="Start"/>
		/// </remarks>
		public LinePosition End { get; init; }

		#region Methods

		/// <summary>
		/// Gets value indicating whether the given <paramref name="position"/> within <see cref="Start"/> and <see cref="End"/>
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool Contains(LinePosition position)
		{
			return Start <= position && End >= position;
		}

		/// <summary>
		/// Gets <see cref="LineRange"/> which <see cref="Start"/> is less than <see cref="End"/>
		/// </summary>
		/// <returns></returns>
		public LineRange Normalized()
		{
			if (Start > End)
			{
				return new LineRange(End, Start);
			}

			return this;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initalizes a new instance of the <see cref="LineRange"/> class
		/// </summary>
		public LineRange()
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="LineRange"/> class
		/// </summary>
		/// <param name="position"></param>
		public LineRange(LinePosition position)
			: this(position, position)
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="LineRange"/> class
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public LineRange(LinePosition start, LinePosition end)
		{
			Start = start;
			End = end;
		}

		#endregion
	}
}