namespace TextEdit.Visual
{
	/// <summary>
	/// Represents a range of columns within a <see cref="IVisualDocument"/>
	/// </summary>
	public record VisualColumnRange
	{
		/// <summary>
		/// Gets <see cref="VisualColumnRange"/> instance which <see cref="Start"/> and <see cref="End"/> positions set default value
		/// </summary>
		public static VisualColumnRange Default { get; } = new VisualColumnRange();

		/// <summary>
		/// Gets value indicating whether the <see cref="Start"/> and <see cref="End"/> are equal or not
		/// </summary>
		public bool IsEmpty => Start == End;

		/// <summary>
		/// Gets first column index in the range.
		/// </summary>
		/// <remarks>
		/// Note that value of this property can be greater than <see cref="End"/>
		/// </remarks>
		public int Start { get; init; }

		/// <summary>
		/// Gets last column index in the range.
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
		/// Gets <see cref="VisualColumnRange"/> which <see cref="Start"/> is less than <see cref="End"/>
		/// </summary>
		/// <returns></returns>
		public VisualColumnRange Normalized()
		{
			if (Start > End)
			{
				return new VisualColumnRange(End, Start);
			}

			return this;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualColumnRange"/> class
		/// </summary>
		public VisualColumnRange()
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualColumnRange"/> class
		/// </summary>
		/// <param name="position"></param>
		public VisualColumnRange(int position)
			: this(position, position)
		{

		}

		/// <summary>
		/// Initalizes a new instance of the <see cref="VisualColumnRange"/> class
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public VisualColumnRange(int start, int end)
		{
			Start = start;
			End = end;
		}

		#endregion
	}
}
