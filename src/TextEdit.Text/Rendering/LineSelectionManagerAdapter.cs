using System.Collections;
using TextEdit.Line;

namespace TextEdit.Text.Rendering
{
	/// <summary>
	/// Implements <see cref="ITextSelectionManager"/> through <see cref="ILineSelectionManager"/>
	/// </summary>
	internal sealed class LineSelectionManagerAdapter : ITextSelectionManager
	{
		private readonly ILineMetrics lineMetrics;
		private readonly ILineSelectionManager lineSelectionManager;

		public LineSelectionManagerAdapter(ILineMetrics lineMetrics, ILineSelectionManager lineSelectionManager)
		{
			this.lineMetrics = lineMetrics;
			this.lineSelectionManager = lineSelectionManager;
			this.lineSelectionManager.SelectionRemoved += LineSelectionManager_SelectionRemoved;
			this.lineSelectionManager.SelectionInserted += LineSelectionManager_SelectionInserted;
			this.lineSelectionManager.SelectionReplaced += LineSelectionManager_SelectionReplaced;
		}

		#region Event handlers

		private void LineSelectionManager_SelectionRemoved(object? sender, ILineSelectionRemovedEventArgs e)
		{
			if (SelectionRemoved is not null)
			{
				var textHitRange = HitConverter.FromLineHitRange(e.Selection, lineMetrics);

				SelectionRemoved.Invoke(this, new TextSelectionRemovedEventArgs(e.Index, textHitRange));
			}
		}

		private void LineSelectionManager_SelectionInserted(object? sender, ILineSelectionInsertedEventArgs e)
		{
			if (SelectionInserted is not null)
			{
				var textHitRange = HitConverter.FromLineHitRange(e.Selection, lineMetrics);

				SelectionInserted.Invoke(this, new TextSelectionInsertedEventArgs(e.Index, textHitRange));
			}
		}

		private void LineSelectionManager_SelectionReplaced(object? sender, ILineSelectionReplacedEventArgs e)
		{
			if (SelectionReplaced is not null)
			{
				var oldTextHitRange = HitConverter.FromLineHitRange(e.OldSelection, lineMetrics);
				var newTextHitRange = HitConverter.FromLineHitRange(e.NewSelection, lineMetrics);

				SelectionReplaced.Invoke(this, new TextSelectionReplacedEventArgs(e.Index, oldTextHitRange, newTextHitRange));
			}
		}

		#endregion

		#region Selections

		public IReadOnlyList<TextHitRange> Selections => throw new NotImplementedException();

		public event EventHandler<ITextSelectionRemovedEventArgs>? SelectionRemoved;
		public event EventHandler<ITextSelectionInsertedEventArgs>? SelectionInserted;
		public event EventHandler<ITextSelectionReplacedEventArgs>? SelectionReplaced;

		#region Editing

		public int Add(TextHitRange textHitRange)
		{
			var lineHitRange = HitConverter.FromTextHitRange(textHitRange, lineMetrics);

			return lineSelectionManager.Add(lineHitRange);
		}

		public void RemoveAt(int index)
		{
			lineSelectionManager.RemoveAt(index);
		}

		public void Clear()
		{
			lineSelectionManager.Clear();
		}

		#endregion

		#endregion

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
