using System.Collections.Immutable;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using TextEdit.Line.Document;
using TextEdit.Text.TextEditor.Clipboard;

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

		public IEnumerable<ITextCaret> Carets => textRenderer.Carets;

		public ITextSelection? Selection => textRenderer.Selection;

		#region EditMode

		public EditMode EditMode
		{
			get => GetValue(EditModeProperty);
			set => SetValue(EditModeProperty, value);
		}

		public static readonly StyledProperty<EditMode> EditModeProperty
			= AvaloniaProperty.Register<AbstractTextEditor, EditMode>(nameof(EditMode), EditMode.Insert);

		#endregion

		#region SelectionMode

		public SelectionMode SelectionMode
		{
			get => GetValue(SelectionModeProperty);
			set => SetValue(SelectionModeProperty, value);
		}

		public static readonly StyledProperty<SelectionMode> SelectionModeProperty
			= AvaloniaProperty.Register<AbstractTextEditor, SelectionMode>(nameof(SelectionMode), SelectionMode.Plain);

		#endregion

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

		#region WordWrap

		public bool WordWrap
		{
			get => GetValue(WordWrapProperty);
			set => SetValue(WordWrapProperty, value);
		}

		public static readonly StyledProperty<bool> WordWrapProperty
			= AvaloniaProperty.Register<AbstractTextEditor, bool>(nameof(WordWrap), false);

		#endregion

		#region TextDragDrop

		public bool TextDragDrop
		{
			get => GetValue(TextDragDropProperty);
			set => SetValue(TextDragDropProperty, value);
		}

		public static readonly StyledProperty<bool> TextDragDropProperty
			= AvaloniaProperty.Register<AbstractTextEditor, bool>(nameof(TextDragDrop), false);

		#endregion

		#region ScrollBelowDocument

		public bool ScrollBelowDocument
		{
			get => GetValue(ScrollBelowDocumentProperty);
			set => SetValue(ScrollBelowDocumentProperty, value);
		}

		public static readonly StyledProperty<bool> ScrollBelowDocumentProperty
			= AvaloniaProperty.Register<AbstractTextEditor, bool>(nameof(ScrollBelowDocument), false);

		#endregion

		#region VirtualSpace

		public bool VirtualSpace
		{
			get => GetValue(VirtualSpaceProperty);
			set => SetValue(VirtualSpaceProperty, value);
		}

		public static readonly StyledProperty<bool> VirtualSpaceProperty
			= AvaloniaProperty.Register<AbstractTextEditor, bool>(nameof(VirtualSpace), false);

		#endregion

		#endregion
	}
}
