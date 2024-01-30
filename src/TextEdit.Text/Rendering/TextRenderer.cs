using TextEdit.Line;

namespace TextEdit.Text
{
	/// <summary>
	/// Implemens <see cref="AbstractTextRenderer"/> through <see cref="AbstractLineRenderer"/>
	/// </summary>
	public abstract class TextRenderer : AbstractTextRenderer
	{
		private readonly AbstractLineRenderer lineRenderer;
		private readonly LineSelectionManagerAdapter selectionManager;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		protected TextRenderer(AbstractLineRenderer lineRenderer, ITextDocument textDocument)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
			this.lineRenderer = lineRenderer;
			this.TextDocument = textDocument;
			this.selectionManager = new LineSelectionManagerAdapter(LineMetrics, lineRenderer.SelectionManager);
		}

		private ITextDocument textDocument;
		private TextDocumentMetrics lineMetrics;

		public override ITextDocument TextDocument
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

		public override ILineMetrics LineMetrics => lineMetrics;

		public override ITextSelectionManager SelectionManager => selectionManager;

		#region Options

		public override SelectionMode SelectionMode
		{
			get => lineRenderer.SelectionMode;
			set => lineRenderer.SelectionMode = value;
		}

		public override bool WordWrap
		{
			get => lineRenderer.WordWrap;
			set => lineRenderer.WordWrap = value;
		}

		public override bool TextDragDrop
		{
			get => lineRenderer.TextDragDrop;
			set => lineRenderer.TextDragDrop = value;
		}

		public override bool VirtualSpace
		{
			get => lineRenderer.VirtualSpace;
			set => lineRenderer.VirtualSpace = value;
		}

		public override bool ScrollBelowDocument
		{
			get => lineRenderer.ScrollBelowDocument;
			set => lineRenderer.ScrollBelowDocument = value;
		}

		#endregion
	}
}
