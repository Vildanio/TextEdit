namespace TextEdit.Text
{
	public interface ITextSelectionRemovedEventArgs
	{
		public int Index { get; }

		public TextHitRange Selection { get; }
	}
}
