using System.Collections.Immutable;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using TextEdit.Line;

namespace TextEdit.Text
{
	/// <summary>
	/// <see cref="Control"/> for rendering and editing <see cref="ITextDocument"/>
	/// </summary>
	public abstract class AbstractTextEditor : Control, ITextEditor
	{
		public static readonly ImmutableArray<HotkeyBinding> BasicHotkeyBindings
			= new HotkeyBinding[]
			{
				// Caret
				new HotkeyBinding(new HotkeyGesture(Key.Left), CaretCommands.CaretCharLeft),
				new HotkeyBinding(new HotkeyGesture(Key.Right), CaretCommands.CaretCharRight),
				new HotkeyBinding(new HotkeyGesture(Key.Left, KeyModifiers.Control), CaretCommands.CaretWordLeft),
				new HotkeyBinding(new HotkeyGesture(Key.Right, KeyModifiers.Control), CaretCommands.CaretWordRight),
				new HotkeyBinding(new HotkeyGesture(Key.Up), CaretCommands.CaretLogicalLineUp),
				new HotkeyBinding(new HotkeyGesture(Key.Down), CaretCommands.CaretLogicalLineDown),
				//new HotKeyBinding(new HotKeyGesture(Key.Up), CaretCommands.CaretVisualLineUp),
				//new HotKeyBinding(new HotKeyGesture(Key.Down), CaretCommands.CaretVisualLineDown),
				new HotkeyBinding(new HotkeyGesture(Key.Home), CaretCommands.CaretToLogicalLineStart),
				new HotkeyBinding(new HotkeyGesture(Key.End), CaretCommands.CaretToLogicalLineEnd),
				//new HotKeyBinding(new HotKeyGesture(Key.Home), CaretCommands.CaretToLogicalLineStart),
				//new HotKeyBinding(new HotKeyGesture(Key.End), CaretCommands.CaretToLogicalLineEnd),
				new HotkeyBinding(new HotkeyGesture(Key.Home, KeyModifiers.Control), CaretCommands.CaretToDocumentStart),
				new HotkeyBinding(new HotkeyGesture(Key.End, KeyModifiers.Control), CaretCommands.CaretToDocumentEnd),

				// Select
				new HotkeyBinding(new HotkeyGesture(Key.Left, KeyModifiers.Shift), SelectionCommands.SelectCharLeft),
				new HotkeyBinding(new HotkeyGesture(Key.Right, KeyModifiers.Shift), SelectionCommands.SelectCharRight),
				new HotkeyBinding(new HotkeyGesture(Key.Left, KeyModifiers.Shift | KeyModifiers.Control), SelectionCommands.SelectWordLeft),
				new HotkeyBinding(new HotkeyGesture(Key.Right, KeyModifiers.Shift | KeyModifiers.Control), SelectionCommands.SelectWordRight),
				new HotkeyBinding(new HotkeyGesture(Key.Up, KeyModifiers.Shift), SelectionCommands.SelectLogicalLineUp),
				new HotkeyBinding(new HotkeyGesture(Key.Down, KeyModifiers.Shift), SelectionCommands.SelectLogicalLineDown),
				//new HotKeyBinding(new HotKeyGesture(Key.Up, KeyModifiers.Shift), SelectionCommands.SelectVisualLineUp),
				//new HotKeyBinding(new HotKeyGesture(Key.Down, KeyModifiers.Shift), SelectionCommands.SelectVisualLineDown),
				new HotkeyBinding(new HotkeyGesture(Key.Home, KeyModifiers.Shift), SelectionCommands.SelectToLogicalLineStart),
				new HotkeyBinding(new HotkeyGesture(Key.End, KeyModifiers.Shift), SelectionCommands.SelectToLogicalLineEnd),
				//new HotKeyBinding(new HotKeyGesture(Key.Home, KeyModifiers.Shift), SelectionCommands.SelectToVisualLineStart),
				//new HotKeyBinding(new HotKeyGesture(Key.End, KeyModifiers.Shift), SelectionCommands.SelectToVisualLineEnd),
				new HotkeyBinding(new HotkeyGesture(Key.Home, KeyModifiers.Shift | KeyModifiers.Control), SelectionCommands.SelectToDocumentStart),
				new HotkeyBinding(new HotkeyGesture(Key.End, KeyModifiers.Shift | KeyModifiers.Control), SelectionCommands.SelectToDocumentEnd),
				new HotkeyBinding(new HotkeyGesture(Key.V, KeyModifiers.Shift), SelectionCommands.SetColumnSelectionMode, SelectionCommands.SetPlainSelectionMode),

				// Clipboard
				new HotkeyBinding(new HotkeyGesture(Key.C, KeyModifiers.Control), ClipboardCommands.Copy),
				new HotkeyBinding(new HotkeyGesture(Key.X, KeyModifiers.Control), ClipboardCommands.Cut),
				new HotkeyBinding(new HotkeyGesture(Key.V, KeyModifiers.Control), ClipboardCommands.Paste),

				// Edit
				new HotkeyBinding(new HotkeyGesture(Key.Delete), EditCommands.CharDelete),
				new HotkeyBinding(new HotkeyGesture(Key.Back), EditCommands.CharBackspace),
				new HotkeyBinding(new HotkeyGesture(Key.Delete, KeyModifiers.Control), EditCommands.WordDelete),
				new HotkeyBinding(new HotkeyGesture(Key.Back, KeyModifiers.Control), EditCommands.WordBackspace),
				new HotkeyBinding(new HotkeyGesture(Key.Insert), EditCommands.SwitchEditMode),

				// Undo
				new HotkeyBinding(new HotkeyGesture(Key.Z, KeyModifiers.Control), UndoCommands.Undo),
				new HotkeyBinding(new HotkeyGesture(Key.Y, KeyModifiers.Control), UndoCommands.Redo),
			}.ToImmutableArray();

		private readonly IHotKeyManager hotKeyManager;
		private readonly AbstractTextRenderer textRenderer;
		private readonly TextDocumentChangeManager undoManager;

		protected AbstractTextEditor(AbstractTextRenderer textRenderer)
		{
			this.textRenderer = textRenderer;
			this.hotKeyManager = new InputHotKeyManager(this, BasicHotkeyBindings);
			this.undoManager = new TextDocumentChangeManager(textRenderer.TextDocument);

			VisualChildren.Add(textRenderer);
		}

		#region TextInput

		protected sealed override void OnTextInput(TextInputEventArgs e)
		{
			// ### NEEDS_TEST
			string? text = e.Text;

			// Preprocessing
			{
				// If input text is not valid
				if (string.IsNullOrEmpty(text) || text == "\x1b" || text == "\b")
				{
					// ASCII 0x1b = ESC.
					// Some shortcuts like Alt+Space produce an empty TextInput event in WPF
					// We have to ignore those
					return;
				}
			}

			// ### NEEDS_TEST

			// If there are selections
			if (!SelectionManager.IsEmpty())
			{
				// Replace selection with text
				SelectionManager.Paste(text);
			}

			// If there are only carets
			else
			{
				// ### NEEDS_TEST

				var textDocument = TextDocument;

				// Insert mode implementation
				if (EditMode == EditMode.Insert)
				{
					foreach (var caretPosition in SelectionManager.EnumerateCarets())
					{
						textDocument.InsertString(caretPosition.CharacterIndex, text);

						// Move caret to the end of inserted text
						SelectionManager.CharRight(text.Length); // ### NEEDS_TEST
					}
				}

				// Overstrike mode implementation
				else if (EditMode == EditMode.Overstrike)
				{
					foreach (var caretPosition in SelectionManager.EnumerateCarets())
					{
						int offset = caretPosition.CharacterIndex;

						// When caret before line ending or at the end of text
						if (offset == textDocument.Count || textDocument[offset].IsLineTerminator())
						{
							// Simply insert characters
							textDocument.InsertString(offset, text);
						}
						else
						{
							int lineEnd = LineMetrics.GetLineEndFromOffset(offset);

							// When text to insert is greater than characters that can be overwrited,
							// then we should simply insert remaining characters
							int charsToOverwrite = text.Length - (lineEnd - offset);

							textDocument.RemoveRange(offset, charsToOverwrite);
							textDocument.InsertString(offset, text);

							// Move caret to the end of inserted text
							SelectionManager.CharRight(text.Length); // ### NEEDS_TEST
						}
					}
				}
			}
		}

		#endregion

		#region ITextEditor

		public ITextDocument TextDocument
		{
			get
			{
				return textRenderer.TextDocument;
			}

			set
			{
				if (TextDocument != value)
				{
					textRenderer.TextDocument = value;
					undoManager.Document = value;
				}
			}
		}

		public ILineMetrics LineMetrics => textRenderer.LineMetrics;

		public ITextSelectionManager SelectionManager => textRenderer.SelectionManager;

		public IUndoManager UndoManager => undoManager;

		public IHotKeyManager HotKeyManager => hotKeyManager;

		#region Clipboard

		public IClipboard Clipboard
		{
			get
			{
				var avaloniaClipboard = TopLevel.GetTopLevel(this)?.Clipboard;

				if (avaloniaClipboard is not null)
				{
					return new AvaloniaClipboard(avaloniaClipboard);
				}

				// This must not happen
				return FalseClipboard.Instance;
			}
		}

		#endregion

		#region Options

		#region EditMode

		public EditMode EditMode
		{
			get => GetValue(EditModeProperty);
			set => SetValue(EditModeProperty, value);
		}

		public static readonly StyledProperty<EditMode> EditModeProperty
			= AvaloniaProperty.Register<AbstractTextEditor, EditMode>(nameof(EditMode), EditMode.Insert);

		#endregion

		public SelectionMode SelectionMode
		{
			get => textRenderer.SelectionMode;
			set => textRenderer.SelectionMode = value;
		}

		public bool WordWrap
		{
			get => textRenderer.WordWrap;
			set => textRenderer.WordWrap = value;
		}

		public bool TextDragDrop
		{
			get => textRenderer.TextDragDrop;
			set => textRenderer.TextDragDrop = value;
		}

		public bool ScrollBelowDocument
		{
			get => textRenderer.ScrollBelowDocument;
			set => textRenderer.ScrollBelowDocument = value;
		}

		public bool VirtualSpace
		{
			get => textRenderer.VirtualSpace;
			set => textRenderer.VirtualSpace = value;
		}

		#endregion

		#endregion
	}
}
