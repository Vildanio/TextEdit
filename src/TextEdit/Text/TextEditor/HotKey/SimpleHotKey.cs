using Avalonia.Input;

namespace TextEdit.Text
{
	internal sealed class SimpleHotKey : HotKey
	{
		// This class allows to don't allocate memory in heap for simple hotkeys
		private readonly KeyGesture keyGesture;

		public override IEnumerable<KeyGesture> KeyGestures
		{
			get
			{
				yield return keyGesture;
			}
		}

		public SimpleHotKey(KeyGesture keyGesture)
		{
			ThrowHelper.ThrowIfNull(keyGesture);

			this.keyGesture = keyGesture;
		}
	}
}
