using Avalonia.Input;

namespace TextEdit.Text
{
	internal sealed class KeyDownHotKeyManager : IHotKeyManager
	{
		private const int tooMuchTime = 100;

		private readonly AbstractTextEditor textEditor;
		private readonly List<HotKeyBinding> hotKeys;

		public KeyDownHotKeyManager(AbstractTextEditor textEditor)
		{
			this.textEditor = textEditor;
			this.hotKeys = new List<HotKeyBinding>();

			textEditor.KeyDown += TextEditor_KeyDown;
		}

		private DateTime lastKeyPressTime;
		private KeyGesture? lastKeyGesture;

		private void TextEditor_KeyDown(object? sender, KeyEventArgs e)
		{
			// NEEDS_CHECK

			var ellapsed = DateTime.Now - lastKeyPressTime;

			// If too much time has passed since the last click.
			if (ellapsed.TotalMilliseconds >= tooMuchTime)
			{
				lastKeyGesture = null;
			}

			KeyGesture gesture = new KeyGesture(e.Key, e.KeyModifiers);

			var firstKeyGesture = lastKeyGesture is null ? gesture : lastKeyGesture;
			var secondKeyGesture = lastKeyGesture is null ? null : gesture;

			foreach (var hotKey in hotKeys)
			{
				var hotKeyGesture = hotKey.KeyGesture;
				
				if (hotKeyGesture.FirstGesture == firstKeyGesture && 
					hotKeyGesture.SecondGesture == secondKeyGesture)
				{
					if (hotKey.Command.CanExecute(textEditor))
					{
						hotKey.Command.Execute(textEditor);
					}

					// Stop processing even if the command didn't execute.
					// Otherwise bugs may occur when some hotkey with the same
					// key gesture works only when "interrupting" hotkey can't execute.
					break;
				}
			}

			lastKeyGesture = gesture;
			lastKeyPressTime = DateTime.Now;
		}

		#region IHotKeyManager

		public void Remove(HotKeyBinding binding)
		{
			hotKeys.Remove(binding);
		}

		public void Add(HotKeyBinding binding)
		{
			hotKeys.Add(binding);
		}

		public bool Contains(HotKeyBinding binding)
		{
			return hotKeys.Contains(binding);
		}

		#endregion
	}
}
