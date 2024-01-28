namespace TextEdit.Text
{
	internal sealed class CharReplacedAction : IAction
	{
		public ITextDocument Document { get; }

		public ICharReplacedEventArgs EventArgs { get; }

		public CharReplacedAction(ITextDocument document, ICharReplacedEventArgs eventArgs)
		{
			Document = document;
			EventArgs = eventArgs;
		}

		public void Undo()
        {
			Document[EventArgs.Index] = EventArgs.OldChar;
        }

        public void Redo()
        {
			Document[EventArgs.Index] = EventArgs.NewChar;
        }
    }
}
