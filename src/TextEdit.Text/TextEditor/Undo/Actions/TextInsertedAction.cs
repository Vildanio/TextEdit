namespace TextEdit.Text
{
	internal sealed class TextInsertedAction : IAction
	{
		public ITextDocument Document { get; }

		public ICharRangeInsertedEventArgs EventArgs { get; }

		public TextInsertedAction(ITextDocument document, ICharRangeInsertedEventArgs eventArgs)
		{
			Document = document;
			EventArgs = eventArgs;
		}

		public void Undo()
		{
			Document.RemoveRange(EventArgs.Index, EventArgs.Chars.Length);
		}

		public void Redo()
		{
			Document.InsertSpan(EventArgs.Index, EventArgs.Chars);
		}
	}
}
