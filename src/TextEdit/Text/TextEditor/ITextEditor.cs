using TextEdit.Line;

namespace TextEdit.Text
{
	// Text editor should be able to support collaborative editing
	// The following intreface represents proxy to "text editing session" of ONE user
	// There are may be many "sessions" in text editor, but commands and other stuff have access to only the current session

	/// <summary>
	/// Provides access to text editor
	/// </summary>
	public interface ITextEditor
	{
		public ITextDocument TextDocument { get; set; }

		public ILineMetrics LineMetrics { get; }

		public ITextSelectionManager SelectionManager { get; }

		public EditMode EditMode { get; set; }

		public SelectionMode SelectionMode { get; set; }

		public IClipboard Clipboard { get; }

		public IUndoManager UndoManager { get; }

		public IHotKeyManager HotKeyManager { get; }

		#region Options

		public bool WordWrap { get; set; }

		public bool TextDragDrop { get; set; }

		public bool VirtualSpace { get; set; }

		public bool ScrollBelowDocument { get; set; }

		#endregion
	}
}
