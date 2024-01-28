namespace TextEdit.Text
{
	/// <summary>
	/// Provides methods for managing hotkeys
	/// </summary>
	public interface IHotKeyManager
	{
		public void Add(HotkeyBinding hotKey);

		public void Remove(HotkeyBinding hotKey);

		public bool Contains(HotkeyBinding hotKey);
	}
}
