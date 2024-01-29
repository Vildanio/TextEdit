namespace TextEdit.Text
{
	public class TextSelectionInsertedEventArgs : ITextSelectionInsertedEventArgs
	{
		public int Index { get; }

		public TextHitRange Selection { get; }

		public TextSelectionInsertedEventArgs(int index, TextHitRange textSelection)
		{
			Index = index;
			Selection = textSelection;
		}
	}
}
