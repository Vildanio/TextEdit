namespace TextEdit.Text
{
	public interface IUndoManager
	{
		#region Undo

		public bool CanUndo();

		public void Undo();

		#endregion

		#region Redo

		public bool CanRedo();

		public void Redo();

		#endregion
	}
}
