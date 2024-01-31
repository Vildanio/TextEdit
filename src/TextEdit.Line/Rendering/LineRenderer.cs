using System.ComponentModel;
using System.Diagnostics.Metrics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using TextEdit.Text;
using TextEdit.Visual;
using SelectionMode = TextEdit.Text.SelectionMode;

namespace TextEdit.Line
{
	/// <summary>
	/// Implements <see cref="AbstractLineRenderer"/> through <see cref="AbstractVisualRenderer"/>
	/// </summary>
	public abstract class LineRenderer : AbstractLineRenderer, ILogicalScrollable
	{
		private readonly AbstractVisualRenderer visualRenderer;
		private readonly ILineDocumentVisualizerProvider visualizerProvider;

		protected LineRenderer(AbstractVisualRenderer visualRenderer, ILineDocumentVisualizerProvider visualizerProvider, ILineDocument lineDocument)
		{
			this.lineDocument = lineDocument;
			this.visualRenderer = visualRenderer;
			this.visualizerProvider = visualizerProvider;
			this.visualizer = visualizerProvider.GetVisualizer(lineDocument);

			visualRenderer.RenderTransform = scrollTransform = new TranslateTransform();
			VisualChildren.Add(visualRenderer);
		}

		#region LineDocument

		private ILineDocument lineDocument;

		public override ILineDocument LineDocument
		{
			get => lineDocument;
			set
			{
				if (lineDocument != value)
				{
					// Unsubscribe from the new document events
					{
						lineDocument.LineRemoved -= LineDocument_LineRemoved;
						lineDocument.LineInserted -= LineDocument_LineInserted;
						lineDocument.LineReplaced -= LineDocument_LineReplaced;
						lineDocument.LineRangeRemoved -= LineDocument_LineRangeRemoved;
						lineDocument.LineRangeInserted -= LineDocument_LineRangeInserted;
					}

					lineDocument = value;

					// Subscribe to the new document events
					{
						lineDocument.LineRemoved += LineDocument_LineRemoved;
						lineDocument.LineInserted += LineDocument_LineInserted;
						lineDocument.LineReplaced += LineDocument_LineReplaced;
						lineDocument.LineRangeRemoved += LineDocument_LineRangeRemoved;
						lineDocument.LineRangeInserted += LineDocument_LineRangeInserted;
					}

					visualizer = visualizerProvider.GetVisualizer(lineDocument);
					documentExtent = visualizer.GetExtent(0, lineDocument.Count);

					InvalidateArrange();
					InvalidateMeasure();
				}
			}
		}

		#region EventHandlers

		private void LineDocument_LineRemoved(object? sender, ILineRemovedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void LineDocument_LineInserted(object? sender, ILineInsertedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void LineDocument_LineReplaced(object? sender, ILineReplacedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void LineDocument_LineRangeRemoved(object? sender, ILineRangeRemovedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void LineDocument_LineRangeInserted(object? sender, ILineRangeInsertedEventArgs e)
		{
			throw new NotImplementedException();
		}

		#endregion

		#endregion

		#region Rendering

		// Count of visuals preloaded when scroll
		private const int preloadedCount = 3;
		private (int Start, int End) renderedRange;

		private ILineDocumentVisualizer visualizer;

		/// <summary>
		/// Range of the LineDocument which is visible on screen
		/// </summary>
		private (int Start, int End) VisibleRange
		{
			get
			{
				return new(
					Math.Min(renderedRange.Start + preloadedCount, LineDocument.Count),
					Math.Max(renderedRange.End - preloadedCount, 0));
			}
			set
			{
				// ### NEEDS_OPTIMIZATION
				var old = VisibleRange;

				if (old != value)
				{
					var visualDocument = visualRenderer.VisualDocument;

					int renderedStart = Math.Max(value.Start - preloadedCount, 0);
					int renderedEnd = Math.Min(value.End + preloadedCount, LineDocument.Count);

					renderedRange = new(renderedStart, renderedEnd);
					renderOffset = visualizer.GetHeight(0, renderedStart);

					// When visible count changed
					{
						int newCount = value.End - value.Start;

						// Get difference betwen the old and new ranges
						int sizeDifference = newCount - (old.End - old.Start);

						// If new range is less than old range
						if (sizeDifference < 0)
						{
							visualDocument.RemoveRange(value.End, sizeDifference);
						}
						// If new range is greater than old range
						else if (sizeDifference > 0)
						{
							var oldVisualDocument = visualDocument;

							// Resize the document
							visualDocument = visualRenderer.VisualDocument = new VisualDocument(newCount);

							// Copy content
							visualDocument.InsertRange(0, oldVisualDocument);
						}
					}

					for (int i = renderedRange.Start; i < renderedRange.End; i++)
					{
						IVisual visual = visualizer.GetVisualLine(i);

						visualDocument[i - renderedRange.Start] = visual;
					}
				}
			}
		}

		private int VisibleCount
		{
			get
			{
				var visibleRange = VisibleRange;

				return visibleRange.End - visibleRange.Start;
			}
		}

		private bool IsRendered(int index)
		{
			return renderedRange.Start <= index && index <= renderedRange.End;
		}

		private (int Start, int End) GetVisibleRange(Size size, out Size filledSize)
		{
			if (LineDocument.Count <= 0)
			{
				filledSize = default;
				return new();
			}

			int start = VisibleRange.Start;
			int end = start;

			double y = 0;
			double width = 0;

			while (y < size.Height && end < LineDocument.Count)
			{
				IVisual line = visualizer.GetVisualLine(end);

				end++;
				y += line.Height;
				width = Math.Max(width, line.Width);
			}

			// If control resized when its VisibleEnd == LineDocument.Count
			// The visible range must be determined relative to the end
			// Otherwise there will be "empty space", because
			// the last page cannot fill the viewport
			while (y < size.Height && start > 0)
			{
				IVisual line = visualizer.GetVisualLine(start);

				start--;
				y += line.Height;
				width = Math.Max(width, line.Width);
			}

			filledSize = new Size(width, y);

			return new(start, end);
		}

		#endregion

		#region Layout

		protected override Size ArrangeOverride(Size finalSize)
		{
			visualRenderer.Arrange(new Rect(finalSize));

			VisibleRange = GetVisibleRange(finalSize, out Size filledSize);

			return filledSize;
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			// Call the method to do not cause bugs
			visualRenderer.Measure(availableSize);

			return new Size(
				Math.Min(documentExtent.Width, availableSize.Width),
				Math.Min(documentExtent.Height, availableSize.Height));
		}

		#endregion

		#region ILogicalScrollable

		#region IScrollable

		private Vector offset;
		private Size documentExtent;

		// Offset of the first rendered visual
		private double renderOffset;
		private readonly TranslateTransform scrollTransform;

		Vector IScrollable.Offset
		{
			get => offset;
			set
			{
				if (!offset.NearlyEquals(value))
				{
					int lineIndex = visualizer.GetVisual(value.Y, out double lineTop);

					if (!IsRendered(lineIndex))
					{
						VisibleRange = new(lineIndex, lineIndex + VisibleCount);
					}

					scrollTransform.Y = value.Y - renderOffset - lineTop;
					scrollTransform.X = value.X;
				}
			}
		}

		Size IScrollable.Viewport => Bounds.Size;

		Size IScrollable.Extent => documentExtent;

		#endregion

		private bool canVerticallyScroll;
		private bool canHorizontallyScroll;
		private static readonly Size scrollSize = new Size(50, 5);

		public bool CanVerticallyScroll
		{
			get
			{
				return canVerticallyScroll;
			}
			set
			{
				if (canVerticallyScroll != value)
				{
					canVerticallyScroll = value;

					RaiseScrollInvalidated(EventArgs.Empty);
				}
			}
		}

		public bool CanHorizontallyScroll
		{
			get
			{
				return canHorizontallyScroll;
			}
			set
			{
				if (canHorizontallyScroll != value)
				{
					canHorizontallyScroll = value;

					RaiseScrollInvalidated(EventArgs.Empty);
				}
			}
		}

		bool ILogicalScrollable.IsLogicalScrollEnabled => true;

		Size ILogicalScrollable.ScrollSize => scrollSize;

		Size ILogicalScrollable.PageScrollSize => Bounds.Size;

		bool ILogicalScrollable.BringIntoView(Control target, Rect targetRect)
		{
			throw new NotImplementedException();
		}

		Control? ILogicalScrollable.GetControlInDirection(NavigationDirection direction, Control? from)
		{
			throw new NotImplementedException();
		}

		#region ScrollInvalidated

		private EventHandler? scrollInvalidated;

		event EventHandler? ILogicalScrollable.ScrollInvalidated
		{
			add => scrollInvalidated += value;
			remove => scrollInvalidated -= value;
		}

		void ILogicalScrollable.RaiseScrollInvalidated(EventArgs e)
		{
			RaiseScrollInvalidated(e);
		}

		private void RaiseScrollInvalidated(EventArgs e)
		{
			scrollInvalidated?.Invoke(this, e);
		}

		#endregion

		#endregion

		#region SelectionManager

		public override ILineSelectionManager SelectionManager => throw new NotImplementedException();

		#endregion

		#region Options

		#region SelectionMode

		public override SelectionMode SelectionMode
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		#endregion

		#region WordWrap

		public override bool WordWrap
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		#endregion

		#region TextDragDrop

		public override bool TextDragDrop
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		#endregion

		#region ScrollBelowDocument

		public override bool ScrollBelowDocument
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		#endregion

		#region VirtualSpace

		public override bool VirtualSpace
		{
			get => throw new NotImplementedException();
			set => throw new NotImplementedException();
		}

		#endregion

		#endregion
	}
}
