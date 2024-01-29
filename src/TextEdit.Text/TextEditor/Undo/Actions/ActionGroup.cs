namespace TextEdit.Text
{
	internal sealed class ActionGroup : IAction
	{
		private readonly IEnumerable<IAction> actions;

		public void Undo()
		{
			foreach (var action in actions)
			{
				action.Undo();
			}
		}

		public void Redo()
		{
			foreach (var action in actions.Reverse())
			{
				action.Redo();
			}
		}

		public ActionGroup(IEnumerable<IAction> args)
		{
			this.actions = args;
		}
	}
}
