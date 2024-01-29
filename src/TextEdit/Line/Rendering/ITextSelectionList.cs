namespace TextEdit.Line
{
	/// <summary>
	/// Observable <see cref="IList{T}"/> of <see cref="LineHitRange"/>
	/// </summary>
	public interface ILineSelectionList : IList<LineHitRange>
	{
		/// <summary>
		/// Occurs when selection was removed
		/// </summary>
		public event EventHandler<ILineSelectionRemovedEventArgs>? SelectionRemoved;

		/// <summary>
		/// Occurs when new selection was added
		/// </summary>
		public event EventHandler<ILineSelectionInsertedEventArgs>? SelectionInserted;

		/// <summary>
		/// Occurs when selection was replaced by another
		/// </summary>
		public event EventHandler<ILineSelectionReplacedEventArgs>? SelectionReplaced;
	}
}
