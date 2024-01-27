namespace TextEdit.Text
{
	public class CharReplacedEventArgs : ICharReplacedEventArgs
    {
		public int Index { get; }

		public char OldChar { get; }

		public char NewChar { get; }

		public CharReplacedEventArgs(int index, char oldChar, char newChar)
		{
			Index = index;
			OldChar = oldChar;
			NewChar = newChar;
		}
	}
}