namespace TextEdit.Text
{
	/// <summary>
	/// Provides methods for managing hotkeys
	/// </summary>
	public interface IHotKeyManager
	{
		public void RemoveHotKey(HotKey hotKey);

		public HotKey? GetHotKey(ITextEditorCommand command);

		public ITextEditorCommand? GetCommand(HotKey hotKey);

		public void SetHotKey(HotKey hotKey, ITextEditorCommand command);
	}
}
