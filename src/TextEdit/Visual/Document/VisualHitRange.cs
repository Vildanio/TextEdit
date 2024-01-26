namespace TextEdit.Visual
{
	/// <summary>
	/// Represents a range of columns within a <see cref="IVisualDocument"/>
	/// </summary>
	public record VisualHitRange
	{
		/// <summary>
		/// Gets <see cref="VisualHitRange"/> instance which <see cref="Start"/> and <see cref="End"/> positions set default value
		/// </summary>
		public static VisualHitRange Default { get; } = new VisualHitRange();

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
		public VisualHit Start { get; init; }

		/// <summary>
		/// Gets end position of the range
		/// </summary>
		/// <remarks>
		/// Note that value of this property can be less than <see cref="Start"/>
		/// </remarks>
		public VisualHit End { get; init; }

		#region Methods

		/// <summary>
		/// Gets value indicating whether the given <paramref name="position"/> within <see cref="Start"/> and <see cref="End"/>
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public bool Contains(VisualHit position)
		{
			return Start <= position && End >= position;
		}

		/// <summary>
		/// Gets <see cref="VisualHitRange"/> which <see cref="Start"/> is less than <see cref="End"/>
		/// </summary>
		/// <returns></returns>
		public VisualHitRange Normalized()
		{
			if (Start > End)
			{
				return new VisualHitRange(End, Start);
			}

			return this;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualHitRange"/> class
		/// </summary>
		public VisualHitRange()
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualHitRange"/> class
		/// </summary>
		/// <param name="position"></param>
		public VisualHitRange(VisualHit position)
			: this(position, position)
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualHitRange"/> class
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public VisualHitRange(VisualHit start, VisualHit end)
		{
			Start = start;
			End = end;
		}

		#endregion
	}
}
