using Avalonia.Input;
using TextEdit.Line;

namespace TextEdit.Text
{
	public abstract class AbstractTextRenderer : InputElement
	{
		public abstract ITextDocument TextDocument { get; set; }

		/// <summary>
		/// Gets <see cref="ILineMetrics"/> used for text rendering
		/// </summary>
		public abstract ILineMetrics LineMetrics { get; }

		/// <summary>
		/// Gets selection manager.
		/// </summary>
		public abstract ITextSelectionManager SelectionManager { get; }

		public abstract SelectionMode SelectionMode { get; set; }

		#region Options

		public abstract bool WordWrap { get; set; }

		public abstract bool TextDragDrop { get; set; }

		public abstract bool VirtualSpace { get; set; }

		public abstract bool ScrollBelowDocument { get; set; }

		#endregion
	}
}
