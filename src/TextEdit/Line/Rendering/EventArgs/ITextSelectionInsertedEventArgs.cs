using TextEdit.Line;

namespace TextEdit.Line
{
	public interface ILineSelectionInsertedEventArgs
	{
		public int Index { get; }

		public LineHitRange Selection { get; }
	}
}
