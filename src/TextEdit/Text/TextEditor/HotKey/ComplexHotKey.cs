using Avalonia.Input;

namespace TextEdit.Text
{
	public class ComplexHotKey : HotKey
	{
		private readonly KeyGesture[] keyGestures;

		public override IEnumerable<KeyGesture> KeyGestures => keyGestures;

        public ComplexHotKey(IEnumerable<KeyGesture> keyGestures)
			: this(keyGestures.ToArray())
        {
            
        }

        private ComplexHotKey(KeyGesture[] keyGesture)
        {
			keyGestures = keyGesture;
		}
    }
}
