using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using TextEdit.Visual;

namespace TextEdit.VisualRendering
{
	internal sealed class InputVisual : InputElement
	{
		private IVisual? visual;
		private InputVisualConstructionContext? context;

		public InputVisual(IVisual? visual = null)
		{
			ConstructVisual(visual);
		}

		private void Free()
		{
			if (context is not null)
			{
				this.PointerEntered -= context.VisualPointerEntered;
				this.PointerExited -= context.VisualPointerExited;
				this.PointerMoved -= context.VisualPointerMoved;
				this.PointerPressed -= context.VisualPointerPressed;
				this.PointerReleased -= context.VisualPointerReleased;
			}
		}

		public IVisual? Visual
		{
			get
			{
				return visual;
			}
			set
			{
				if (visual != value)
				{
					visual = value;

					ConstructVisual(value);
				}
			}
		}

		private void ConstructVisual(IVisual? visual)
		{
			Free();

			if (visual is not null)
			{
				context = new InputVisualConstructionContext();

				visual.Construct(context);

				this.PointerEntered += context.VisualPointerEntered;
				this.PointerExited += context.VisualPointerExited;
				this.PointerMoved += context.VisualPointerMoved;
				this.PointerPressed += context.VisualPointerPressed;
				this.PointerReleased += context.VisualPointerReleased;
			}

			InvalidateVisual();
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			if (Visual is not null)
			{
				return new Size(Visual.Width, Visual.Height);
			}

			return new Size();
		}

		protected override void ArrangeCore(Rect finalRect)
		{
			double width = 0;
			double height = 0;

			if (Visual is not null)
			{
				width = Visual.Width;
				height = Visual.Height;
			}

			Bounds = new Rect(finalRect.X, finalRect.Y, width, height);
		}

		protected override Size MeasureCore(Size availableSize)
		{
			if (Visual is not null)
			{
				return new Size(
					Math.Min(Visual.Width, availableSize.Width), 
					Math.Min(Visual.Height, availableSize.Height));
			}

			return new Size();
		}

		public override void Render(DrawingContext context)
		{
			this.context?.Drawable?.Draw(context, default);
		}
	}
}
