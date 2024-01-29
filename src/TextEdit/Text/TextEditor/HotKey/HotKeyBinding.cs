namespace TextEdit.Text
{
	public class HotkeyBinding
	{
		// This class can have only two states:
		// 1. OnPressCommand is not null && OnReleaseCommand is null
		// 1. OnPressCommand is not null && OnReleaseCommand is not null
		// The OnPressCommand cannot be null, because there should not be commands, which activate in key release

		public HotkeyGesture KeyGesture { get; }

		public ITextEditorCommand OnPressCommand { get; }

		public ITextEditorCommand? OnReleaseCommand { get; }

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HotkeyBinding"/> class
		/// </summary>
		/// <param name="keyGesture"></param>
		/// <param name="onPress"></param>
		/// <param name="onRelease"></param>
		public HotkeyBinding(HotkeyGesture keyGesture, ITextEditorCommand onPress, ITextEditorCommand onRelease)
		{
			ThrowHelper.ThrowIfNull(keyGesture);
			ThrowHelper.ThrowIfNull(onPress);
			ThrowHelper.ThrowIfNull(onRelease);

			KeyGesture = keyGesture;
			OnPressCommand = onPress;
			OnReleaseCommand = onRelease;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HotkeyBinding"/> class
		/// </summary>
		/// <param name="keyGesture"></param>
		/// <param name="onPress"></param>
		public HotkeyBinding(HotkeyGesture keyGesture, ITextEditorCommand onPress)
		{
			ThrowHelper.ThrowIfNull(keyGesture);
			ThrowHelper.ThrowIfNull(onPress);

			KeyGesture = keyGesture;
			OnPressCommand = onPress;
		}

		#endregion
	}
}
