using CardBuilder.Client.Messages;
using CardBuilder.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace CardBuilder.Client.ViewModels;

public partial class ProjectItemViewModel : TreeItemViewModel {

	private readonly IMessenger _messenger;

	public ProjectItemViewModel(
		Project project,
		IMessenger messenger
	) {
		Project = project;
		_messenger = messenger;
	}

	public Project Project { get; }

	[RelayCommand]
	public void Remove() {
		_ = _messenger.Send( new RemoveProjectMessage( Project.Id ) );
	}
}
