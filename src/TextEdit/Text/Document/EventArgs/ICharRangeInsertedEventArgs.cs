namespace TextEdit.Text
{
	public interface ICharRangeInsertedEventArgs
	{
		public int Index { get; }

		public string Chars { get; }
	}
}
