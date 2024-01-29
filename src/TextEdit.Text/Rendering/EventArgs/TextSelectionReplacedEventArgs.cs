namespace TextEdit.Text
{
	public class TextSelectionReplacedEventArgs : ITextSelectionReplacedEventArgs
	{
		public int Index { get; }

		public TextHitRange OldSelection { get; }

		public TextHitRange NewSelection { get; }

		public TextSelectionReplacedEventArgs(int index, TextHitRange oldTextSelection, TextHitRange newTextSelection)
		{
			Index = index;
			OldSelection = oldTextSelection;
			NewSelection = newTextSelection;
		}
	}
}
