namespace TextEdit.Visual
{
	public interface IVisualSelection
	{
		/// <summary>
		/// Gets caret and end position of the selection
		/// </summary>
		public IVisualCaret Caret { get; }

		/// <summary>
		/// Gets position where selection started
		/// </summary>
		public VisualLineHit StartPosition { get; set; }

		/// <summary>
		/// Sets <see cref="StartPosition"/> and <see cref="IVisualCaret.Position"/>
		/// </summary>
		/// <param name="range"></param>
		public void SetSelectedRange(VisualLineHitRange range);
	}
}
