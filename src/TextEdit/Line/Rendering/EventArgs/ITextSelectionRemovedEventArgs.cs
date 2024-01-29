using TextEdit.Line;

namespace TextEdit.Line
{
	public interface ILineSelectionRemovedEventArgs
	{
		public int Index { get; }

		public LineHitRange Selection { get; }
	}
}
