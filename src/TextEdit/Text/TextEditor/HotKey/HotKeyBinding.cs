namespace TextEdit.Text
{
	public class HotKeyBinding
	{
		public HotKeyGesture KeyGesture { get; }

		public ITextEditorCommand Command { get; }

		public HotKeyBinding(HotKeyGesture keyGesture, ITextEditorCommand command)
		{
			ThrowHelper.ThrowIfNull(keyGesture);
			ThrowHelper.ThrowIfNull(command);

			KeyGesture = keyGesture;
			Command = command;
		}
	}
}
