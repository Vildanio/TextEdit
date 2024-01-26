namespace TextEdit.Line
{
	public interface ILineSelection
	{
		/// <summary>
		/// Gets caret and end position of the selection
		/// </summary>
		public ILineCaret Caret { get; }

		/// <summary>
		/// Gets position where selection started
		/// </summary>
		public LineHit StartPosition { get; set; }

		/// <summary>
		/// Sets <see cref="StartPosition"/> and <see cref="ILineCaret.Position"/>
		/// </summary>
		/// <param name="range"></param>
		public void SetSelectedRange(LineHitRange range);
	}
}