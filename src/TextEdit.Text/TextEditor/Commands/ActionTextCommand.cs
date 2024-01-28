namespace TextEdit.Text
{
	internal sealed class ActionTextEditorCommand : ITextEditorCommand
	{
		private readonly Action<ITextEditor> execute;
		private readonly Func<ITextEditor, bool>? canExecute;

		public ActionTextEditorCommand(Action<ITextEditor> execute, Func<ITextEditor, bool>? canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public bool CanExecute(ITextEditor editor)
		{
			if (canExecute is not null)
			{
				return canExecute(editor);
			}

			return true;
		}

		public void Execute(ITextEditor editor)
		{
			execute(editor);
		}
	}
}
