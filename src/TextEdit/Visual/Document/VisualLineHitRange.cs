namespace TextEdit.Visual
{
	/// <summary>
	/// Represents a range of columns within a <see cref="IVisualDocument"/>
	/// </summary>
	public record VisualLineHitRange
	{
		/// <summary>
		/// Gets <see cref="VisualLineHitRange"/> instance which <see cref="Start"/> and <see cref="End"/> positions set default value
		/// </summary>
		public static VisualLineHitRange Default { get; } = new VisualLineHitRange();

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
		public VisualLineHit Start { get; init; }

		/// <summary>
		/// Gets end position of the range
		/// </summary>
		/// <remarks>
		/// Note that value of this property can be less than <see cref="Start"/>
		/// </remarks>
		public VisualLineHit End { get; init; }

		#region Constructors

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualLineHitRange"/> class
		/// </summary>
		public VisualLineHitRange()
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualLineHitRange"/> class
		/// </summary>
		/// <param name="position"></param>
		public VisualLineHitRange(VisualLineHit position)
			: this(position, position)
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualLineHitRange"/> class
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public VisualLineHitRange(VisualLineHit start, VisualLineHit end)
		{
			Start = start;
			End = end;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets value indicating whether the given <paramref name="position"/> within <see cref="Start"/> and <see cref="End"/>
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool Contains(VisualLineHit position)
		{
			return Start <= position && End >= position;
		}

		/// <summary>
		/// Gets <see cref="VisualLineHitRange"/> which <see cref="Start"/> is less than <see cref="End"/>
		/// </summary>
		/// <returns></returns>
		public VisualLineHitRange Normalized()
		{
			if (Start > End)
			{
				return new VisualLineHitRange(End, Start);
			}

			return this;
		}

		#endregion
	}
}