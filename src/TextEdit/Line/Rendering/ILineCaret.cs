namespace TextEdit.Line
{
	public interface ILineCaret
	{
		public LinePosition Position { get; set; }

		#region Logical navigation

		// Visual navigation is needed for setting multi-caret work.
		// It will be able to create new caret at the next visual line but not logical.
		// Maybe this help someone. Also VS Code can do it.

		/// <summary>
		/// Moves caret to previous logical line.
		/// </summary>
		public void LogicalLineUp();

		/// <summary>
		/// Moves caret to next logical line.
		/// </summary>
		public void LogicalLineDown();

		#endregion

		#region Visual navigation

		/// <summary>
		/// Moves caret to previous visual line.
		/// </summary>
		public void VisualLineUp();

		/// <summary>
		/// Moves caret to next visual line.
		/// </summary>
		public void VisualLineDown();

		#endregion
	}
}