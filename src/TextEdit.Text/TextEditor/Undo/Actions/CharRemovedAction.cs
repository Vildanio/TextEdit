namespace TextEdit.Text
{
	internal sealed class CharRemovedAction : IAction
	{
		public ITextDocument Document { get; }

		public ICharRemovedEventArgs EventArgs { get; }

		public CharRemovedAction(ITextDocument document, ICharRemovedEventArgs eventArgs)
		{
			Document = document;
			EventArgs = eventArgs;
		}

		public void Undo()
		{
			Document.Insert(EventArgs.Index, EventArgs.Char);
		}

		public void Redo()
		{
			Document.RemoveAt(EventArgs.Index);
		}
	}
}
