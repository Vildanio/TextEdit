namespace TextEdit.Text
{
	public class CharRemovedEventArgs : ICharRemovedEventArgs
	{
		public int Index { get; }

		public char Char { get; }

		public CharRemovedEventArgs(int index, char @char)
		{
			Index = index;
			Char = @char;
		}
	}
}