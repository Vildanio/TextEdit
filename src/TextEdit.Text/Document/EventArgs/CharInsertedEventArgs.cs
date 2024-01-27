namespace TextEdit.Text
{
	public class CharInsertedEventArgs : ICharInsertedEventArgs
	{
		public int Index { get; }

		public char Char { get; }

		public CharInsertedEventArgs(int index, char @char)
		{
			Index = index;
			Char = @char;
		}
	}
}