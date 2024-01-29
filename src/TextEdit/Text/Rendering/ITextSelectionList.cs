namespace TextEdit.Text
{
	/// <summary>
	/// Observable <see cref="IList{T}"/> of <see cref="TextHitRange"/>
	/// </summary>
	public interface ITextSelectionList : IList<TextHitRange>
	{
		/// <summary>
		/// Occurs when selection was removed
		/// </summary>
		public event EventHandler<ITextSelectionRemovedEventArgs>? SelectionRemoved;

		/// <summary>
		/// Occurs when new selection was added
		/// </summary>
		public event EventHandler<ITextSelectionInsertedEventArgs>? SelectionInserted;

		/// <summary>
		/// Occurs when selection was replaced by another
		/// </summary>
		public event EventHandler<ITextSelectionReplacedEventArgs>? SelectionReplaced;
	}
}
