using Avalonia.Input;
using TextEdit.Visual;

namespace TextEdit.VisualRendering
{
	internal sealed class InputVisualConstructionContext : IVisualConstructionContext
	{
		public IDrawable? Drawable { get; private set; }
		
		public EventHandler<PointerEventArgs>? VisualPointerMoved { get; private set; }
		
		public  EventHandler<PointerEventArgs>? VisualPointerExited { get; private set; }
		
		public  EventHandler<PointerEventArgs>? VisualPointerEntered { get; private set; }
		
		public  EventHandler<PointerPressedEventArgs>? VisualPointerPressed { get; private set; }

		public  EventHandler<PointerReleasedEventArgs>? VisualPointerReleased { get; private set; }


		public void RegisterDrawer(IDrawable drawable)
		{
			Drawable = drawable;
		}

		public void RegisterPointerEntered(EventHandler<PointerEventArgs> handler)
		{
			VisualPointerEntered = handler;
		}

		public void RegisterPointerExited(EventHandler<PointerEventArgs> handler)
		{
			VisualPointerExited = handler;
		}

		public void RegisterPointerMoved(EventHandler<PointerEventArgs> handler)
		{
			VisualPointerMoved = handler;
		}

		public void RegisterPointerPressed(EventHandler<PointerPressedEventArgs> handler)
		{
			VisualPointerPressed = handler;
		}

		public void RegisterPointerReleased(EventHandler<PointerReleasedEventArgs> handler)
		{
			VisualPointerReleased = handler;
		}
	}
}
