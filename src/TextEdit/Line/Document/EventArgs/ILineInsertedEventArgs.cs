namespace TextEdit.Line
{
	public interface ILineInsertedEventArgs
	{
		public int Index { get; }

		public string Text { get; }
	}
}