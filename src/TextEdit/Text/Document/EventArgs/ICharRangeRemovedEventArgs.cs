namespace TextEdit.Text
{
	public interface ICharRangeRemovedEventArgs
	{
		public int Index { get; }

		public string Chars { get; }
	}
}
