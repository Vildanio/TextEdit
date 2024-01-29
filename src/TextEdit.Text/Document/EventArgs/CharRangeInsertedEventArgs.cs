namespace TextEdit.Text
{
	public class CharRangeInsertedEventArgs : ICharRangeInsertedEventArgs
	{
		public int Index { get; }

		public string Chars { get; }

		public CharRangeInsertedEventArgs(int index, string chars)
		{
			Index = index;
			Chars = chars;
		}
	}
}