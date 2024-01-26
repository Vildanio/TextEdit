namespace TextEdit.Text
{
	public interface ITextEditorCommand
	{
		public bool CanExecute(TextEditor editor);

		public void Execute(TextEditor editor);
	}
}
