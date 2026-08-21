using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace CardBuilder.Client.Views.Controls;

public partial class EditableTextBlock : UserControl {

	private bool _isEditing;

	public EditableTextBlock() {
		InitializeComponent();
	}

	public static readonly StyledProperty<string> TextProperty =
		AvaloniaProperty.Register<EditableTextBlock, string>(
			nameof( Text ),
			defaultValue: "",
			defaultBindingMode: BindingMode.TwoWay
		);

	public string Text {
		get => GetValue( TextProperty );
		set => SetValue( TextProperty, value );
	}

	internal static readonly DirectProperty<EditableTextBlock, bool> IsEditingProperty =
		AvaloniaProperty.RegisterDirect<EditableTextBlock, bool>(
			nameof( IsEditing ),
			o => o.IsEditing,
			( o, v ) => o.IsEditing = v
		);

	internal bool IsEditing {
		get => _isEditing;
		set => SetAndRaise( IsEditingProperty, ref _isEditing, value );
	}

	private void StartEdit() {
		if( IsEditing ) {
			return;
		}
		EditBox.Text = Text;
		IsEditing = true;
		_ = EditBox.Focus();
		EditBox.SelectAll();
	}

	private void CommitEdit() {
		if( !IsEditing ) {
			return;
		}
		IsEditing = false;
		Text = EditBox.Text ?? "";
	}

	private void CancelEdit() {
		if( !IsEditing ) {
			return;
		}
		IsEditing = false;
	}

	private void OnDisplayDoubleTapped(
		object? sender,
		TappedEventArgs e
	) {
		StartEdit();
	}

	private void OnEditKeyDown(
		object? sender,
		KeyEventArgs e
	) {
		if( e.Key == Key.Enter ) {
			CommitEdit();
			e.Handled = true;
		} else if( e.Key == Key.Escape ) {
			CancelEdit();
			e.Handled = true;
		}
	}

	private void OnEditLostFocus(
		object? sender,
		RoutedEventArgs e
	) {
		CommitEdit();
	}
}
