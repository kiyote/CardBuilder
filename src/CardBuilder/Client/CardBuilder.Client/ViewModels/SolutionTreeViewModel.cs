using CardBuilder.Client.Messages;
using CardBuilder.Client.Services;
using CardBuilder.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace CardBuilder.Client.ViewModels;

public partial class SolutionTreeViewModel : ViewModelBase, IRecipient<SolutionChangedMessage> {

	private readonly ISolutionManager _solutionManager;
	private readonly IMessenger _messenger;

	public SolutionTreeViewModel(
		ISolutionManager solutionManager,
		IMessenger messenger
	) {
		_solutionManager = solutionManager;
		_messenger = messenger;
		_messenger.Register( this );
		Refresh();
	}

	[ObservableProperty]
	public partial IEnumerable<SolutionItemViewModel> SolutionItems { get; set; } = [];

	void IRecipient<SolutionChangedMessage>.Receive(
		SolutionChangedMessage message
	) {
		Refresh();
	}

	private void Refresh() {
		if( _solutionManager.Solution == Solution.None ) {
			SolutionItems = [];
			return;
		}

		SolutionItems = [ new SolutionItemViewModel( _solutionManager, _messenger ) ];
	}
}
