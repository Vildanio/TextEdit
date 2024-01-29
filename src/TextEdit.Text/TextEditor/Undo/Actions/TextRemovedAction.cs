namespace TextEdit.Text
{
	internal sealed class TextRemovedAction : IAction
	{
		public ITextDocument Document { get; }

		public ICharRangeRemovedEventArgs EventArgs { get; }

		public TextRemovedAction(ITextDocument document, ICharRangeRemovedEventArgs eventArgs)
		{
			Document = document;
			EventArgs = eventArgs;
		}

		public void Undo()
		{
			Document.InsertSpan(EventArgs.Index, EventArgs.Chars);
		}

		public void Redo()
		{
			Document.RemoveRange(EventArgs.Index, EventArgs.Chars.Length);
		}
	}
}
