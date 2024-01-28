using TextEdit.Collections;

namespace TextEdit.Text
{
	/// <summary>
	/// Implementation of the <see cref="IUndoManager"/> through the document events listening
	/// </summary>
	public class TextDocumentChangeManager : IUndoManager
	{
		private readonly ITextDocument document;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TextDocumentChangeManager"/> class
		/// </summary>
		/// <param name="document"></param>
		public TextDocumentChangeManager(ITextDocument document)
			: this(document, int.MaxValue)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextDocumentChangeManager"/> class
		/// </summary>
		/// <param name="document"></param>
		/// <param name="maxItemCount"></param>
		public TextDocumentChangeManager(ITextDocument document, int maxItemCount)
		{
			this.document = document;
			redoActions = new LimitedStack<IAction>(maxItemCount);
			undoActions = new LimitedStack<IAction>(maxItemCount);

			// Subscribe to the events
			document.CharRemoved += Document_CharRemoved;
			document.CharInserted += Document_CharInserted;
			document.CharReplaced += Document_CharReplaced;
			document.CharRangeRemoved += Document_CharRangeRemoved;
			document.CharRangeInserted += Document_CharRangeInserted;
		}

		#endregion

		#region Handlers

		private void Document_CharRemoved(object? sender, ICharRemovedEventArgs e)
		{
			if (!doUndo)
			{
				CharRemovedAction action = new CharRemovedAction(document, e);

				PushAction(action);
			}
		}

		private void Document_CharReplaced(object? sender, ICharReplacedEventArgs e)
		{
			if (!doUndo)
			{
				CharReplacedAction action = new CharReplacedAction(document, e);

				PushAction(action);
			}
		}

		private void Document_CharInserted(object? sender, ICharInsertedEventArgs e)
		{
			if (!doUndo)
			{
				CharInsertedAction action = new CharInsertedAction(document, e);

				PushAction(action);
			}
		}

		private void Document_CharRangeRemoved(object? sender, ICharRangeRemovedEventArgs e)
		{
			if (!doUndo)
			{
				TextRemovedAction action = new TextRemovedAction(document, e);

				PushAction(action);
			}
		}

		private void Document_CharRangeInserted(object? sender, ICharRangeInsertedEventArgs e)
		{
			if (!doUndo)
			{
				TextInsertedAction action = new TextInsertedAction(document, e);

				PushAction(action);
			}
		}

		#endregion

		#region Undo

		private bool doUndo;
		private LimitedStack<IAction> undoActions;
		private LimitedStack<IAction> redoActions;

		public int Count => undoActions.Count;

		public int MaxCount => undoActions.MaxItemCount;

		private void PushAction(IAction action)
		{
			undoActions.Push(action);

			// The redo operations gets invalid
			// So we clear them to do not cause unexcpected behaviour
			redoActions.Clear();
		}

		public void Clear()
		{
			undoActions.Clear();
			redoActions.Clear();
		}

		public void Undo()
		{
			if (undoActions.TryPop(out var action))
			{
				doUndo = true;
				action.Undo();
				doUndo = false;

				redoActions.Push(action);
			}
			else
			{
				throw new InvalidOperationException("No actions to undo");
			}
		}

		public void Redo()
		{
			if (redoActions.TryPop(out var action))
			{
				doUndo = true;
				action.Redo();
				doUndo = false;

				undoActions.Push(action);
			}
			else
			{
				throw new InvalidOperationException("No actions to redo");
			}
		}

		public bool CanRedo()
		{
			return redoActions.Count > 0;
		}

		public bool CanUndo()
		{
			return undoActions.Count > 0;
		}

		#endregion
	}
}
