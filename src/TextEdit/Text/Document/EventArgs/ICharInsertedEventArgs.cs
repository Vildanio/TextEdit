namespace TextEdit.Text
{
	public interface ICharInsertedEventArgs
	{
		public int Index { get; }

		public char Char { get; }
	}
}
