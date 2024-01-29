using Avalonia.Input;
using TextEdit.Line;

namespace TextEdit.Text.Rendering
{
	public abstract class AbstractTextRenderer : InputElement
	{
		private readonly AbstractLineRenderer lineRenderer;

		protected AbstractTextRenderer(AbstractLineRenderer lineRenderer)
		{
			this.lineRenderer = lineRenderer;
		}

		public ITextDocument TextDocument
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		public ILineMetrics LineMetrics => throw new NotImplementedException();

		public ITextSelectionManager SelectionManager => new LineSelectionManagerAdapter(LineMetrics, lineRenderer.SelectionManager);

		#region Options

		public SelectionMode SelectionMode
		{
			get => lineRenderer.SelectionMode;
			set => lineRenderer.SelectionMode = value;
		}

		public bool WordWrap
		{
			get => lineRenderer.WordWrap;
			set => lineRenderer.WordWrap = value;
		}

		public bool TextDragDrop
		{
			get => lineRenderer.TextDragDrop;
			set => lineRenderer.TextDragDrop = value;
		}

		public bool VirtualSpace
		{
			get => lineRenderer.VirtualSpace;
			set => lineRenderer.VirtualSpace = value;
		}

		public bool ScrollBelowDocument
		{
			get => lineRenderer.ScrollBelowDocument;
			set => lineRenderer.ScrollBelowDocument = value;
		}

		#endregion
	}
}
