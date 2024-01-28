namespace TextEdit.Text
{
	public class HotkeyBinding
	{
		public HotkeyGesture KeyGesture { get; }

		public ITextEditorCommand Command { get; }

		public HotkeyBinding(HotkeyGesture keyGesture, ITextEditorCommand command)
		{
			ThrowHelper.ThrowIfNull(keyGesture);
			ThrowHelper.ThrowIfNull(command);

			KeyGesture = keyGesture;
			Command = command;
		}
	}
}
