namespace TextEdit.Text
{
	public interface ICharReplacedEventArgs
	{
		public int Index { get; }

		public char OldChar { get; }

		public char NewChar { get; }
	}
}
