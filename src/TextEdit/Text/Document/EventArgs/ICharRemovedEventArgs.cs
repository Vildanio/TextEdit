namespace TextEdit.Text
{
	public interface ICharRemovedEventArgs
	{
		public int Index { get; }

		public char Char { get; }
	}
}
