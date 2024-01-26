namespace TextEdit.Line
{
	/// <summary>
	/// Represents a range of characters within a <see cref="ILineDocument"/>
	/// </summary>
	public record LineHitRange
	{
		/// <summary>
		/// Gets <see cref="LineHitRange"/> instance which <see cref="Start"/> and <see cref="End"/> positions set default value
		/// </summary>
		public static LineHitRange Default { get; } = new LineHitRange();

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
		public LineHit Start { get; init; }

		/// <summary>
		/// Gets end position of the range
		/// </summary>
		/// <remarks>
		/// Note that value of this property can be less than <see cref="Start"/>
		/// </remarks>
		public LineHit End { get; init; }

		#region Methods

		/// <summary>
		/// Gets value indicating whether the given <paramref name="position"/> within <see cref="Start"/> and <see cref="End"/>
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool Contains(LineHit position)
		{
			return Start <= position && End >= position;
		}
		
		/// <summary>
		/// Gets <see cref="LineHitRange"/> which <see cref="Start"/> is less than <see cref="End"/>
		/// </summary>
		/// <returns></returns>
		public LineHitRange Normalized()
		{
			if (Start > End)
			{
				return new LineHitRange(End, Start);
			}

			return this;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initalizes a new instance of the <see cref="LineHitRange"/> class
		/// </summary>
		public LineHitRange()
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="LineHitRange"/> class
		/// </summary>
		/// <param name="position"></param>
		public LineHitRange(LineHit position)
			: this(position, position)
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="LineHitRange"/> class
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public LineHitRange(LineHit start, LineHit end)
		{
			Start = start;
			End = end;
		}

		#endregion
	}
}