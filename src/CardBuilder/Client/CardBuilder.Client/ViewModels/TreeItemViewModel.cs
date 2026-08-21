using CommunityToolkit.Mvvm.ComponentModel;

namespace CardBuilder.Client.ViewModels;

public abstract partial class TreeItemViewModel : ViewModelBase {

	[ObservableProperty]
	public partial bool IsExpanded { get; set; }

	[ObservableProperty]
	public partial bool IsSelected { get; set; }
}
