using Avalonia.Input;
using TextEdit.Text;

namespace TextEdit.Line
{
	public abstract class AbstractLineRenderer : InputElement
	{
		public abstract ILineDocument LineDocument { get; set; }

		public abstract ILineSelectionManager SelectionManager { get; }

		#region Options

		public abstract SelectionMode SelectionMode { get; set; }

		public abstract bool WordWrap { get; set; }

		public abstract bool TextDragDrop { get; set; }

		public abstract bool VirtualSpace { get; set; }

		public abstract bool ScrollBelowDocument { get; set; }

		#endregion
	}
}
