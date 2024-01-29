namespace TextEdit.Text
{
	public interface ITextSelectionInsertedEventArgs
	{
		public int Index { get; }

		public TextHitRange TextSelection { get; }
	}
}
