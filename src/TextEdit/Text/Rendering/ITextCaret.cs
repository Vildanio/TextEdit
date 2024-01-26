namespace TextEdit.Text
{
	public interface ITextCaret
	{
		public TextHit Position { get; set; }

		#region Navigation

		// These all methods explicitly defined even if they can be implemented
		// through the SetPosition method to support multiple carets navigation.

		#region Char

		/// <summary>
		/// Moves caret to the previous character
		/// </summary>
		public void CharLeft();

		/// <summary>
		/// Moves caret to the next character
		/// </summary>
		public void CharRight();

		/// <summary>
		/// Moves caret to the previous word boundary
		/// </summary>
		public void WordLeft();

		/// <summary>
		/// Moves caret to the next word boundary
		/// </summary>
		public void WordRight();

		#endregion

		#region Logical

		/// <summary>
		/// Moves caret to previous logical line.
		/// </summary>
		public void LogicalLineUp();

		/// <summary>
		/// Moves caret to next logical line.
		/// </summary>
		public void LogicalLineDown();

		#endregion

		#region Visual

		/// <summary>
		/// Moves caret to previous visual line.
		/// </summary>
		public void VisualLineUp();

		/// <summary>
		/// Moves caret to previous visual line.
		/// </summary>
		public void VisualLineDown();

		#endregion

		#endregion
	}
}
