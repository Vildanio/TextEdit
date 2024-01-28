namespace TextEdit.Text
{
	internal sealed class CharInsertedAction : IAction
	{
		public ITextDocument Document { get; }

		public ICharInsertedEventArgs EventArgs { get; }

		public CharInsertedAction(ITextDocument document, ICharInsertedEventArgs eventArgs)
		{
			Document = document;
			EventArgs = eventArgs;
		}

		public void Undo()
        {
			Document.RemoveAt(EventArgs.Index);
        }

        public void Redo()
        {
			Document.Insert(EventArgs.Index, EventArgs.Char);
        }
    }
}
