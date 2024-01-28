namespace TextEdit.Text
{
	public interface ITextEditorCommand
	{
		public bool CanExecute(ITextEditor textEditor);

		public void Execute(ITextEditor textEditor);
	}
}
