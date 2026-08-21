using CardBuilder.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CardBuilder.Client.ViewModels;

public partial class SolutionItemViewModel : ViewModelBase {

	[ObservableProperty]
	public partial string ItemId { get; set; } = "<Id>";

	[ObservableProperty]
	public partial string Name { get; set; } = "<Name>";

	[ObservableProperty]
	[NotifyPropertyChangedFor( nameof( IsSolution ) )]
	[NotifyPropertyChangedFor( nameof( IsProject ) )]
	public partial SolutionItemType ItemType { get; set; } = SolutionItemType.Unknown;

	public bool IsSolution => ItemType == SolutionItemType.Solution;

	public bool IsProject => ItemType == SolutionItemType.Project;

	[ObservableProperty]
	public partial ObservableCollection<SolutionItemViewModel> Children { get; set; } = [];
}
