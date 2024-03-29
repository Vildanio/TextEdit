﻿using TextEdit.Line;

namespace TextEdit.Text
{
	/// <summary>
	/// Manages text selecting
	/// </summary>
	public interface ILineSelectionManager
	{
		/// <summary>
		/// Gets collection of <see cref="LineHitRange"/> elements where each of them represents selection
		/// and the <see cref="LineHitRange.End"/> represents caret position. The primary selection is the first in the collection if collection is not empty.
		/// </summary>
		/// <remarks>
		/// <para>Note that the <see cref="LineHitRange"/> values can be invalid within <see cref="ILineDocument"/> if virtual space is enabled.</para>
		/// <para>It expected that this property is readonly and never changes</para>
		/// </remarks>
		public ILineSelectionList Selections { get; }

		#region Editing

		/// <summary>
		/// Replaces text in selected range with the given <paramref name="text"/>
		/// </summary>
		/// <param name="text"></param>
		public void Paste(string text);

		/// <summary>
		/// Copies text in the selected range.
		/// </summary>
		/// <returns></returns>
		public string Copy();

		/// <summary>
		/// Copies and then removes text in the selected range.
		/// </summary>
		/// <returns></returns>
		public string Cut();

		#endregion

		// These methods needed to prevent carets and selections from moving independently of others

		#region Navigation			

		#region Char

		/// <summary>
		/// Empties selection and moves carets to the previous character
		/// </summary>
		public void CharLeft();

		/// <summary>
		/// Empties selection and moves carets to the next character
		/// </summary>
		public void CharRight();

		/// <summary>
		/// Empties selection and moves carets to <paramref name="count"/> characters backward
		/// </summary>
		public void CharLeft(int count);

		/// <summary>
		/// Empties selection and moves carets to <paramref name="count"/> characters forward
		/// </summary>
		public void CharRight(int count);

		/// <summary>
		/// Empties selection and moves carets to the previous word boundary
		/// </summary>
		public void WordLeft();

		/// <summary>
		/// Empties selection and moves carets to the next word boundary
		/// </summary>
		public void WordRight();

		#endregion

		#region Document

		/// <summary>
		/// Empties selection and moves carets to the document start
		/// </summary>
		public void DocumentStart();

		/// <summary>
		/// Empties selection and moves carets to the document end
		/// </summary>
		public void DocumentEnd();

		#endregion

		#region Logical line

		/// <summary>
		/// Empties selection and moves carets to previous logical line.
		/// </summary>
		public void LogicalLineUp();

		/// <summary>
		/// Empties selection and moves carets to next logical line.
		/// </summary>
		public void LogicalLineDown();

		/// <summary>
		/// Empties selection and moves carets to start of logical line.
		/// </summary>
		public void LogicalLineStart();

		/// <summary>
		/// Empties selection and moves carets to end of logical line.
		/// </summary>
		public void LogicalLineEnd();

		#endregion

		#region Visual line

		/// <summary>
		/// Empties selection and moves carets to previous visual line.
		/// </summary>
		public void VisualLineUp();

		/// <summary>
		/// Empties selection and moves carets to previous visual line.
		/// </summary>
		public void VisualLineDown();

		/// <summary>
		/// Empties selection and moves carets to start of visual line.
		/// </summary>
		public void VisualLineStart();

		/// <summary>
		/// Empties selection and moves carets to end of visual line.
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
		/// Moves selection end to <paramref name="count"/> characters backward
		/// </summary>
		public void SelectCharLeft(int count);

		/// <summary>
		/// Moves selection end to <paramref name="count"/> characters forward
		/// </summary>
		public void SelectCharRight(int count);

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
