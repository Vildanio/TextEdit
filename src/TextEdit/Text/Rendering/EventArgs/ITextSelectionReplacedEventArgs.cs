namespace TextEdit.Text
{
	public interface ITextSelectionReplacedEventArgs
	{
		public int Index { get; }

		public TextHitRange OldSelection { get; }

		public TextHitRange NewSelection { get; }
	}
}
