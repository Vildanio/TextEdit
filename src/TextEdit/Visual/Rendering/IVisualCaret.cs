namespace TextEdit.Visual
{
	public interface IVisualCaret
	{
		public VisualLineHit Position { get; set; }

		#region Line navigation

		/// <summary>
		/// Moves caret to the previous visual line
		/// </summary>
		public void LineUp();

		/// <summary>
		/// Moves caret to the next visual line
		/// </summary>
		public void LineDown();

		#endregion

		#region Column navigation

		/// <summary>
		/// Moves caret to <paramref name="count"/> of columns left
		/// </summary>
		/// <param name="count"></param>
		public void ColumnLeft(int count);

		/// <summary>
		/// Moves caret to <paramref name="count"/> of columns right
		/// </summary>
		/// <param name="count"></param>
		public void ColumnRight(int count);

		#endregion
	}
}
