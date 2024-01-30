using Avalonia.Input;
using TextEdit.Line;

namespace TextEdit.Text
{
	public abstract class AbstractTextRenderer : InputElement
	{
		public abstract ITextDocument TextDocument { get; set; }

		public abstract ILineMetrics LineMetrics { get; }

		public abstract ITextSelectionManager SelectionManager { get; }

		#region Options

		public abstract SelectionMode SelectionMode { get; set; }

		public abstract bool WordWrap { get; set; }

		public abstract bool TextDragDrop { get; set; }

		public abstract bool VirtualSpace { get; set; }

		public abstract bool ScrollBelowDocument { get; set; }

		#endregion
	}
}
