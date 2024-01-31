using Avalonia;
using Avalonia.Media;
using Avalonia.Input;
using TextEdit.Visual;

namespace TextEdit.VisualRendering
{
	/// <summary>
	/// Implements <see cref="AbstractVisualRenderer"/> through rendering each <see cref="IVisual"/> in its own <see cref="InputElement"/>
	/// </summary>
	public sealed class VisualRenderer : AbstractVisualRenderer
	{
		#region Constructors

		public VisualRenderer(IVisualDocument renderedDocument)
		{
			ThrowHelper.ThrowIfNull(renderedDocument);

			this.renderedDocument = renderedDocument;
		}

		#endregion

		#region RenderedDocument

		private IVisualDocument renderedDocument;

		public override IVisualDocument VisualDocument
		{
			get
			{
				return renderedDocument;
			}

			set
			{
				ThrowHelper.ThrowIfNull(value);

				if (renderedDocument != value)
				{
					// Unsubscribe from thr old document events
					{
						renderedDocument.VisualInserted -= RenderedDocument_VisualInserted;
						renderedDocument.VisualRemoved -= RenderedDocument_VisualRemoved;
						renderedDocument.VisualReplaced -= RenderedDocument_VisualReplaced;
						renderedDocument.VisualRangeRemoved -= RenderedDocument_VisualRangeRemoved;
						renderedDocument.VisualRangeInserted -= RenderedDocument_VisualRangeInserted;
					}

					renderedDocument = value;

					// Subscribe to the new document events
					{
						value.VisualInserted += RenderedDocument_VisualInserted;
						value.VisualRemoved += RenderedDocument_VisualRemoved;
						value.VisualReplaced += RenderedDocument_VisualReplaced;
						value.VisualRangeRemoved += RenderedDocument_VisualRangeRemoved;
						value.VisualRangeInserted += RenderedDocument_VisualRangeInserted;
					}

					// Construct visuals
					ConstructVisualDocument(value);
				}
			}
		}

		private void RenderedDocument_VisualRangeInserted(object? sender, IVisualRangeInsertedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void RenderedDocument_VisualRangeRemoved(object? sender, IVisualRangeRemovedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void RenderedDocument_VisualReplaced(object? sender, IVisualReplacedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void RenderedDocument_VisualRemoved(object? sender, IVisualRemovedEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void RenderedDocument_VisualInserted(object? sender, IVisualInsertedEventArgs e)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Visual construction
		
		private IEnumerable<InputVisual> CreateInputVisuals(int count)
		{
			for (int i = 0; i < count; i++)
			{
				yield return new InputVisual();
			}
		}

		private void ConstructVisualDocument(IVisualDocument visualDocument)
		{
			int childrenCount = VisualChildren.Count;
			int documentCount = visualDocument.Count;

			if (childrenCount < documentCount)
			{
				int countToAdd = documentCount - childrenCount;

				VisualChildren.AddRange(CreateInputVisuals(countToAdd));
			}
			else if(childrenCount > documentCount)
			{
				int countToRemove = childrenCount - documentCount;

				VisualChildren.RemoveRange(0, countToRemove);
			}

			ConstructVisuals(visualDocument);
		}

		private void ConstructVisuals(IEnumerable<IVisual> visuals)
		{
			Point point = default;

			var visualsEnumerator = visuals.GetEnumerator();
			var visualChildrenEnumerator = VisualChildren.GetEnumerator();

			while (visualsEnumerator.MoveNext() && visualChildrenEnumerator.MoveNext())
			{
				var visual = visualsEnumerator.Current;
				var inputVisual = (InputVisual)visualChildrenEnumerator.Current;

				inputVisual.Visual = visual;

				var transform = inputVisual.RenderTransform;

				if (transform is not TranslateTransform translateTranform ||
					translateTranform.X != point.X || translateTranform.Y != point.Y)
				{
					inputVisual.RenderTransform = new TranslateTransform(point.X, point.Y);
				}

				point = point.WithY(point.Y + visual.Height);
			}
		}

		#endregion

		#region Layout

		protected override Size ArrangeOverride(Size finalSize)
		{
			Rect bounds = new Rect(finalSize);

			var enumerator = VisualChildren.GetEnumerator();

			// Enumerate visual until the height or width achieve the constraints
			while (bounds.Width < finalSize.Width &&
				bounds.Height < finalSize.Height &&
				enumerator.MoveNext())
			{
				var visual = (InputVisual)enumerator.Current;

				visual.Arrange(bounds);

				Rect visualBounds = visual.Bounds;

				bounds = new Rect(
					bounds.X,
					bounds.Y + visualBounds.Height,
					Math.Max(bounds.Width, visualBounds.Width),
					bounds.Height - visualBounds.Height);
			}

			return bounds.Size;
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			double width = 0;
			double height = 0;

			var enumerator = VisualChildren.GetEnumerator();

			// Enumerate visual until the height or width achieve the constraints
			while (width < availableSize.Width &&
				height < availableSize.Height &&
				enumerator.MoveNext())
			{
				var visual = (InputVisual)enumerator.Current;

				// There is a bug if you dont measure InputElement
				visual.Measure(availableSize);

				Size desiredSize = visual.DesiredSize;

				height += desiredSize.Height;
				width = Math.Max(width, desiredSize.Width);
			}

			return new Size(width, height);
		}

		#endregion
	}
}
