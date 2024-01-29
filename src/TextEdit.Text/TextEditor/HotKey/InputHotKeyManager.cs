using Avalonia.Input;

namespace TextEdit.Text
{
	internal sealed class InputHotKeyManager : IHotKeyManager
	{
		private const int tooMuchTime = 100;

		private readonly AbstractTextEditor textEditor;
		private readonly List<HotkeyBinding> hotKeys;

		#region Constructors

		public InputHotKeyManager(AbstractTextEditor textEditor)
			: this(textEditor, new List<HotkeyBinding>())
		{

		}

		public InputHotKeyManager(AbstractTextEditor textEditor, IEnumerable<HotkeyBinding> bindings)
			: this(textEditor, new List<HotkeyBinding>(bindings))
		{

		}

		private InputHotKeyManager(AbstractTextEditor textEditor, List<HotkeyBinding> bindings)
		{
			this.textEditor = textEditor;
			this.hotKeys = bindings;

			textEditor.KeyDown += TextEditor_KeyDown;
			textEditor.KeyUp += TextEditor_KeyUp;
		}

		#endregion

		private DateTime lastKeyPressTime;
		private KeyGesture? lastKeyGesture;

		// Gets hotkey which was activated and now waits for key releasing
		private HotkeyBinding? activeHotKey;

		private void TextEditor_KeyUp(object? sender, KeyEventArgs e)
		{
			if (activeHotKey is not null)
			{
				activeHotKey.OnReleaseCommand?.Execute(textEditor);
			}
		}

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
					if (hotKey.OnPressCommand.CanExecute(textEditor))
					{
						hotKey.OnPressCommand.Execute(textEditor);

						if (hotKey.OnReleaseCommand is not null)
						{
							activeHotKey = hotKey;
						}
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

		public void Remove(HotkeyBinding binding)
		{
			hotKeys.Remove(binding);
		}

		public void Add(HotkeyBinding binding)
		{
			hotKeys.Add(binding);
		}

		public bool Contains(HotkeyBinding binding)
		{
			return hotKeys.Contains(binding);
		}

		#endregion
	}
}
