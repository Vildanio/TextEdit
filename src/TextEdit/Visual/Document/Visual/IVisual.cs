using Avalonia;

namespace TextEdit.Visual
{
	/// <summary>
	/// Represents an object that can be registered in <see cref="VisualRenderer"/>
	/// </summary>
	public interface IVisual
	{
		/// <summary>
		/// Width of the visual in device-independent pixels
		/// </summary>
		public double Width => GetColumnsWidth(0, VisualLength);

		/// <summary>
		/// Height of the visual in device-independent pixels
		/// </summary>
		public double Height => GetColumnsHeight(0, VisualLength);

		/// <summary>
		/// Gets count of visual columns
		/// </summary>
		public int VisualLength { get; }

		/// <summary>
		/// Construct itself using the given <paramref name="constructionContext"/>
		/// </summary>
		/// <param name="constructionContext"></param>
		public void Construct(IVisualConstructionContext constructionContext);

		#region GetColumn

		/// <summary>
		/// Gets the height of the column at the specified index.
		/// </summary>
		/// <param name="index">The index of the column.</param>
		/// <returns>The height of the column.</returns>
		public double GetColumnHeight(int index);

		/// <summary>
		/// Gets the width of the column at the specified index.
		/// </summary>
		/// <param name="index">The index of the column.</param>
		/// <returns>The width of the column.</returns>
		public double GetColumnWidth(int index);

		/// <summary>
		/// Gets the bounding box (size) of the column at the specified index.
		/// </summary>
		/// <param name="index">The index of the column.</param>
		/// <returns>The bounding box (size) of the column.</returns>
		public Size GetColumnBounds(int index);

		#endregion

		#region GetColumns

		/// <summary>
		/// Gets the maximum height of the columns within the given range.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of columns in the range.</param>
		/// <returns>The maximum height of the columns.</returns>
		public double GetColumnsHeight(int start, int count);

		/// <summary>
		/// Gets the sum of width of the columns within the given range.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of columns in the range.</param>
		/// <returns>The sum of width of the columns.</returns>
		public double GetColumnsWidth(int start, int count);

		/// <summary>
		/// Gets the maximum bounding box (size) of the columns within the given range.
		/// </summary>
		/// <param name="start">The starting index of the range.</param>
		/// <param name="count">The number of columns in the range.</param>
		/// <returns>The maximum bounding box (size) of the columns.</returns>
		public Size GetColumnsBounds(int start, int count);

		#endregion

		#region GetSelectableColumns

		/// <summary>
		/// Gets ranges of columns that can be selected.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public IEnumerable<VisualColumnRange> GetSelectableColumns(int start, int count);

		#endregion

		#region GetVisualHit

		/// <summary>
		/// Gets visual hit from the given <paramref name="distance"/>
		/// </summary>
		/// <param name="distance"></param>
		/// <returns></returns>
		public VisualHit GetVisualHitFromDistance(double distance);

		/// <summary>
		/// Gets distance from the given <paramref name="visualColumnHit"/>
		/// </summary>
		/// <param name="visualColumnHit"></param>
		/// <returns></returns>
		public double GetDistanceFromVisualHit(VisualHit visualColumnHit);

		#endregion
	}
}
