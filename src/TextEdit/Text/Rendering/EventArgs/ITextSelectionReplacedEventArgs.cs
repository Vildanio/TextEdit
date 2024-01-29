namespace TextEdit.Text
{
	public interface ITextSelectionReplacedEventArgs
	{
		public int Index { get; }

		public TextHitRange OldTextSelection { get; }

		public TextHitRange NewTextSelection { get; }
	}
}
