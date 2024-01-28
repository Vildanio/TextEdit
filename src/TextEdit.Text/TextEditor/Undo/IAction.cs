namespace TextEdit.Text
{
    public interface IAction
    {
        public void Undo();

        public void Redo();
    }
}
