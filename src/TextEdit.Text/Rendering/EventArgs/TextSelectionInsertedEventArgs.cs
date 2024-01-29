namespace TextEdit.Text
{
	public class TextSelectionInsertedEventArgs : ITextSelectionInsertedEventArgs
	{
		public int Index { get; }

		public TextHitRange TextSelection { get; }

		public TextSelectionInsertedEventArgs(int index, TextHitRange textSelection)
		{
			Index = index;
			TextSelection = textSelection;
		}
	}
}
