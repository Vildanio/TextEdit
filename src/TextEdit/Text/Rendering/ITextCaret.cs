namespace TextEdit.Text
{
	public interface ITextCaret
	{
		public TextHit Position { get; set; }

		#region Editing

		// This method defined excplicitly to support multiple-carets

		/// <summary>
		/// Replaces text in selected range with the given <paramref name="text"/>
		/// </summary>
		/// <param name="text"></param>
		public void Paste(string text);

		#endregion

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

		#region Document

		/// <summary>
		/// Moves caret to the document start
		/// </summary>
		public void DocumentStart();

		/// <summary>
		/// Moves caret to the document end
		/// </summary>
		public void DocumentEnd();

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

		/// <summary>
		/// Moves caret to start of logical line.
		/// </summary>
		public void LogicalLineStart();

		/// <summary>
		/// Moves caret to end of logical line.
		/// </summary>
		public void LogicalLineEnd();

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

		/// <summary>
		/// Moves caret to start of visual line.
		/// </summary>
		public void VisualLineStart();

		/// <summary>
		/// Moves caret to end of visual line.
		/// </summary>
		public void VisualLineEnd();

		#endregion

		#endregion
	}
}
