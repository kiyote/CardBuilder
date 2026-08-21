using CardBuilder.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardBuilder.Client.ViewModels;

public partial class SolutionViewModel : ViewModelBase {

	public SolutionViewModel(
		Solution solution
	) {
		Solution = solution;
	}

	[ObservableProperty]
	public partial Solution Solution { get; set; }

	[ObservableProperty]
	public partial IEnumerable<SolutionItemViewModel> SolutionItems { get; set; } = [];

	partial void OnSolutionChanged(
		Solution value
	) {
		if( value == Solution.None ) {
			SolutionItems = [];
			return;
		}

		SolutionItems = new[] { value.ToViewModel() };
	}
}
