namespace TextEdit.Text
{
	public class TextSelectionRemovedEventArgs : ITextSelectionRemovedEventArgs
	{
		public int Index { get; }

		public TextHitRange TextSelection { get; }

		public TextSelectionRemovedEventArgs(int index, TextHitRange textSelection)
		{
			Index = index;
			TextSelection = textSelection;
		}
	}
}
