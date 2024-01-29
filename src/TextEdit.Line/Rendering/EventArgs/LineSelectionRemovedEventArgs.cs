namespace TextEdit.Line
{
	public class LineSelectionRemovedEventArgs : ILineSelectionRemovedEventArgs
	{
		public int Index { get; }

		public LineHitRange Selection { get; }

		public LineSelectionRemovedEventArgs(int index, LineHitRange selection)
		{
			Index = index;
			Selection = selection;
		}
	}
}
