using TextEdit.Line;

namespace TextEdit.Text.Rendering
{
	/// <summary>
	/// Implements <see cref="ITextSelectionManager"/> through <see cref="ILineSelectionManager"/>
	/// </summary>
	internal sealed class LineSelectionManagerAdapter : ITextSelectionManager
	{
		private readonly LineSelectionListAdapter textSelectionList;
		private readonly ILineSelectionManager lineSelectionManager;

		public LineSelectionManagerAdapter(ILineMetrics lineMetrics, ILineSelectionManager lineSelectionManager)
		{
			this.lineSelectionManager = lineSelectionManager;
			this.textSelectionList = new LineSelectionListAdapter(lineMetrics, lineSelectionManager.Selections);
		}

		public ITextSelectionList Selections => textSelectionList;

		public void Paste(string text)
		{
			lineSelectionManager.Paste(text);
		}

		public void CharLeft()
		{
			lineSelectionManager.CharLeft();
		}

		public void CharLeft(int count)
		{
			lineSelectionManager.CharLeft(count);
		}

		public void CharRight()
		{
			lineSelectionManager.CharRight();
		}

		public void CharRight(int count)
		{
			lineSelectionManager.CharRight(count);
		}

		public string Copy()
		{
			return lineSelectionManager.Copy();
		}

		public string Cut()
		{
			return lineSelectionManager.Cut();
		}

		public void DocumentEnd()
		{
			lineSelectionManager.DocumentEnd();
		}

		public void DocumentStart()
		{
			lineSelectionManager.DocumentStart();
		}

		public void LogicalLineDown()
		{
			lineSelectionManager.LogicalLineDown();
		}

		public void LogicalLineEnd()
		{
			lineSelectionManager.LogicalLineEnd();
		}

		public void LogicalLineStart()
		{
			lineSelectionManager.LogicalLineStart();
		}

		public void LogicalLineUp()
		{
			lineSelectionManager.LogicalLineUp();
		}

		public void SelectCharLeft()
		{
			lineSelectionManager.SelectCharLeft();
		}

		public void SelectCharLeft(int count)
		{
			lineSelectionManager.SelectCharLeft(count);
		}

		public void SelectCharRight()
		{
			lineSelectionManager.SelectCharRight();
		}

		public void SelectCharRight(int count)
		{
			lineSelectionManager.SelectCharRight(count);
		}

		public void SelectDocumentEnd()
		{
			lineSelectionManager.SelectDocumentEnd();
		}

		public void SelectDocumentStart()
		{
			lineSelectionManager.SelectDocumentStart();
		}

		public void SelectLogicalLineDown()
		{
			lineSelectionManager.SelectLogicalLineDown();
		}

		public void SelectLogicalLineEnd()
		{
			lineSelectionManager.SelectLogicalLineEnd();
		}

		public void SelectLogicalLineStart()
		{
			lineSelectionManager.SelectLogicalLineStart();
		}

		public void SelectLogicalLineUp()
		{
			lineSelectionManager.SelectLogicalLineUp();
		}

		public void SelectVisualLineDown()
		{
			lineSelectionManager.SelectVisualLineDown();
		}

		public void SelectVisualLineEnd()
		{
			lineSelectionManager.SelectVisualLineEnd();
		}

		public void SelectVisualLineStart()
		{
			lineSelectionManager.SelectVisualLineStart();
		}

		public void SelectVisualLineUp()
		{
			lineSelectionManager.SelectVisualLineUp();
		}

		public void SelectWordLeft()
		{
			lineSelectionManager.SelectWordLeft();
		}

		public void SelectWordRight()
		{
			lineSelectionManager.SelectWordRight();
		}

		public void VisualLineDown()
		{
			lineSelectionManager.VisualLineDown();
		}

		public void VisualLineEnd()
		{
			lineSelectionManager.VisualLineEnd();
		}

		public void VisualLineStart()
		{
			lineSelectionManager.VisualLineStart();
		}

		public void VisualLineUp()
		{
			lineSelectionManager.VisualLineUp();
		}

		public void WordLeft()
		{
			lineSelectionManager.WordLeft();
		}

		public void WordRight()
		{
			lineSelectionManager.WordRight();
		}
	}
}
