
namespace TextEdit.Text
{
	public interface ITextSelection
	{
		#region Position

		/// <summary>
		/// Gets position where selection started
		/// </summary>
		public TextHit StartPosition { get; set; }

		/// <summary>
		/// Gets position where selection ended
		/// </summary>
		public TextHit EndPosition { get; set; }

		/// <summary>
		/// Gets and sets <see cref="StartPosition"/> and <see cref="EndPosition"/>
		/// </summary>
		public TextHitRange SelectedRange { get; set; }

		#endregion

		#region Editing

		// These methods defined excplicitly to support column selection

		/// <summary>
		/// Replaces text in selected range with the given <paramref name="text"/>
		/// </summary>
		/// <param name="text"></param>
		public void Paste(string text);

		/// <summary>
		/// Copies text in the selected range
		/// </summary>
		/// <returns></returns>
		public string Copy();

		/// <summary>
		/// Copies and then removed from document text in the selected range
		/// </summary>
		/// <returns></returns>
		public string Cut();

		#endregion

		#region Navigation

		// These all methods explicitly defined even if they can be implemented
		// through the SetPosition method to support multiple selection navigation.

		#region Char

		/// <summary>
		/// Empties selection and moves selection end to the previous character
		/// </summary>
		public void CharLeft();

		/// <summary>
		/// Empties selection and moves selection end to the next character
		/// </summary>
		public void CharRight();

		/// <summary>
		/// Empties selection and moves selection end caret to the previous word boundary
		/// </summary>
		public void WordLeft();

		/// <summary>
		/// Empties selection and moves selection end caret to the next word boundary
		/// </summary>
		public void WordRight();

		#endregion

		#region Document

		/// <summary>
		/// Empties selection and moves selection end to the document start
		/// </summary>
		public void DocumentStart();

		/// <summary>
		/// Empties selection and moves selection end to the document end
		/// </summary>
		public void DocumentEnd();

		#endregion

		#region Logical line

		/// <summary>
		/// Empties selection and moves selection end to previous logical line.
		/// </summary>
		public void LogicalLineUp();

		/// <summary>
		/// Empties selection and moves selection end to next logical line.
		/// </summary>
		public void LogicalLineDown();

		/// <summary>
		/// Empties selection and moves selection end to start of logical line.
		/// </summary>
		public void LogicalLineStart();

		/// <summary>
		/// Empties selection and moves selection end to end of logical line.
		/// </summary>
		public void LogicalLineEnd();

		#endregion

		#region Visual line

		/// <summary>
		/// Empties selection and moves selection end to previous visual line.
		/// </summary>
		public void VisualLineUp();

		/// <summary>
		/// Empties selection and moves selection end to previous visual line.
		/// </summary>
		public void VisualLineDown();

		/// <summary>
		/// Empties selection and moves selection end to start of visual line.
		/// </summary>
		public void VisualLineStart();

		/// <summary>
		/// Empties selection and moves selection end to end of visual line.
		/// </summary>
		public void VisualLineEnd();

		#endregion

		#endregion

		#region Selection

		#region Char

		/// <summary>
		/// Moves selection end to the previous character
		/// </summary>
		public void SelectCharLeft();

		/// <summary>
		/// Moves selection end to the next character
		/// </summary>
		public void SelectCharRight();

		/// <summary>
		/// Moves selection end to the previous word boundary
		/// </summary>
		public void SelectWordLeft();

		/// <summary>
		/// Moves selection end to the next word boundary
		/// </summary>
		public void SelectWordRight();

		#endregion

		#region Document

		/// <summary>
		/// Moves selection end to the document start
		/// </summary>
		public void SelectDocumentStart();

		/// <summary>
		/// Moves selection end to the document end
		/// </summary>
		public void SelectDocumentEnd();

		#endregion

		#region Logical line

		/// <summary>
		/// Moves selection end to previous logical line.
		/// </summary>
		public void SelectLogicalLineUp();

		/// <summary>
		/// Moves selection end to next logical line.
		/// </summary>
		public void SelectLogicalLineDown();

		/// <summary>
		/// Moves selection end to start of logical line.
		/// </summary>
		public void SelectLogicalLineStart();

		/// <summary>
		/// Moves selection end to end of logical line.
		/// </summary>
		public void SelectLogicalLineEnd();

		#endregion

		#region Visual line

		/// <summary>
		/// Moves selection end to previous visual line.
		/// </summary>
		public void SelectVisualLineUp();

		/// <summary>
		/// Moves selection end to previous visual line.
		/// </summary>
		public void SelectVisualLineDown();

		/// <summary>
		/// Moves selection end to start of visual line.
		/// </summary>
		public void SelectVisualLineStart();

		/// <summary>
		/// Moves selection end to end of visual line.
		/// </summary>
		public void SelectVisualLineEnd();

		#endregion

		#endregion
	}
}
