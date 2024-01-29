namespace TextEdit.Text
{
	public class TextSelectionReplacedEventArgs : ITextSelectionReplacedEventArgs
	{
		public int Index { get; }

		public TextHitRange OldTextSelection { get; }

		public TextHitRange NewTextSelection { get; }

		public TextSelectionReplacedEventArgs(int index, TextHitRange oldTextSelection, TextHitRange newTextSelection)
		{
			Index = index;
			OldTextSelection = oldTextSelection;
			NewTextSelection = newTextSelection;
		}
	}
}
