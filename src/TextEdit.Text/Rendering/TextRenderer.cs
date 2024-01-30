using Avalonia.Input;
using TextEdit.Line;

namespace TextEdit.Text
{
	public abstract class AbstractTextRenderer : InputElement
	{
		private readonly AbstractLineRenderer lineRenderer;
		private readonly LineSelectionManagerAdapter selectionManager;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		protected AbstractTextRenderer(AbstractLineRenderer lineRenderer, ITextDocument textDocument)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
			this.lineRenderer = lineRenderer;
			this.TextDocument = textDocument;
			this.selectionManager = new LineSelectionManagerAdapter(LineMetrics, lineRenderer.SelectionManager);
		}

		private ITextDocument textDocument;
		private TextDocumentMetrics lineMetrics;

		public ITextDocument TextDocument
		{
			get => textDocument;
			set
			{
				if (textDocument != value)
				{
					textDocument = value;

					lineMetrics = TextDocumentMetrics.GetMetrics(value);
					lineRenderer.LineDocument = new VirtualLineDocument(value, lineMetrics);
				}
			}
		}

		public ILineMetrics LineMetrics => lineMetrics;

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
