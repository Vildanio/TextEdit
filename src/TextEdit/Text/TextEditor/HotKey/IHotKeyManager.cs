namespace TextEdit.Text
{
	/// <summary>
	/// Provides methods for managing hotkeys
	/// </summary>
	public interface IHotKeyManager
	{
		public void Add(HotKeyBinding hotKey);

		public void Remove(HotKeyBinding hotKey);

		public bool Contains(HotKeyBinding hotKey);
	}
}
