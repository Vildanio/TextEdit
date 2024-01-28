using Avalonia.Input;

namespace TextEdit.Text
{
	/// <summary>
	/// Represents a combination of <see cref="KeyGesture"/> for executing some command
	/// </summary>
	public class HotkeyGesture
	{
		public KeyGesture FirstGesture { get; }

		public KeyGesture? SecondGesture { get; }

        public HotkeyGesture(Key key, KeyModifiers modifiers = KeyModifiers.None)
			: this(new KeyGesture(key))
        {
            
        }

        public HotkeyGesture(KeyGesture firstGesture, KeyGesture? secondGesture = null)
		{
			ThrowHelper.ThrowIfNull(firstGesture);

			FirstGesture = firstGesture;
			SecondGesture = secondGesture;
		}
    }
}
