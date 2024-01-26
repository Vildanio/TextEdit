using Avalonia.Input;

namespace TextEdit.Visual
{
	/// <summary>
	/// Context of generating <see cref="Visual"/> or <see cref="InputElement"/> from <see cref="IVisual"/>
	/// </summary>
	public interface IVisualConstructionContext
	{
		#region Draw

		/// <summary>
		/// Registers object that draws content of created <see cref="Visual"/>
		/// </summary>
		/// <param name="drawable"></param>
		public void RegisterDrawer(IDrawable drawable);

		#endregion

		#region Input

		/// <summary>
		/// Registers <see cref="InputElement.PointerEntered"/> event handler to the created <see cref="InputElement"/>
		/// </summary>
		/// <param name="handler"></param>
		public void RegisterPointerEntered(EventHandler<PointerEventArgs> handler);

		/// <summary>
		/// Registers <see cref="InputElement.PointerMoved"/> event handler to the created <see cref="InputElement"/>
		/// </summary>
		/// <param name="handler"></param>
		public void RegisterPointerMoved(EventHandler<PointerEventArgs> handler);

		/// <summary>
		/// Registers <see cref="InputElement.PointerPressed"/> event handler to the created <see cref="InputElement"/>
		/// </summary>
		/// <param name="handler"></param>
		public void RegisterPointerPressed(EventHandler<PointerPressedEventArgs> handler);

		/// <summary>
		/// Registers <see cref="InputElement.PointerReleased"/> event handler to the created <see cref="InputElement"/>
		/// </summary>
		/// <param name="handler"></param>
		public void RegisterPointerReleased(EventHandler<PointerReleasedEventArgs> handler);

		/// <summary>
		/// Registers <see cref="InputElement.PointerExited"/> event handler to the created <see cref="InputElement"/>
		/// </summary>
		/// <param name="handler"></param>
		public void RegisterPointerExited(EventHandler<PointerEventArgs> handler);

		#endregion
	}
}
