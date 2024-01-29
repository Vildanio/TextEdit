namespace TextEdit.Text
{
	public class TextSelectionRemovedEventArgs : ITextSelectionRemovedEventArgs
	{
		public int Index { get; }

		public TextHitRange Selection { get; }

		public TextSelectionRemovedEventArgs(int index, TextHitRange textSelection)
		{
			Index = index;
			Selection = textSelection;
		}
	}
}
