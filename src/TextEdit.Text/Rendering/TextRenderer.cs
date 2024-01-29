using Avalonia.Input;
using TextEdit.Line;

namespace TextEdit.Text.Rendering
{
	public abstract class AbstractTextRenderer : InputElement
	{
		private readonly AbstractLineRenderer lineRenderer;
		private readonly LineSelectionManagerAdapter selectionManager;

		protected AbstractTextRenderer(AbstractLineRenderer lineRenderer, ITextDocument textDocument)
		{
			this.lineRenderer = lineRenderer;
			this.textDocument = textDocument;
			this.selectionManager = new LineSelectionManagerAdapter(LineMetrics, lineRenderer.SelectionManager);
		}

		private ITextDocument textDocument;

		public ITextDocument TextDocument
		{
			get => textDocument;
			set
			{
				if (textDocument != value)
				{
					textDocument = value;

					throw new NotImplementedException();

					//lineRenderer.LineDocument = new VirtualLineDocument(value);
				}
			}
		}

		public ILineMetrics LineMetrics => throw new NotImplementedException();

		public ITextSelectionManager SelectionManager => selectionManager;

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
