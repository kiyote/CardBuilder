using System.Collections.ObjectModel;
using CardBuilder.Client.Assets;
using CardBuilder.Client.Messages;
using CardBuilder.Client.Services;
using CardBuilder.Model;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace CardBuilder.Client.ViewModels;

public partial class SolutionItemViewModel : TreeItemViewModel,
	IRecipient<ProjectAddedMessage>,
	IRecipient<ProjectRemovedMessage> {

	private readonly ISolutionManager _solutionManager;
	private readonly IMessenger _messenger;

	public SolutionItemViewModel(
		ISolutionManager solutionManager,
		IMessenger messenger
	) {
		_solutionManager = solutionManager;
		_messenger = messenger;
		Projects = [ .. solutionManager.Solution.Projects.Select( p => p.ToViewModel( messenger ) ) ];
		_messenger.Register<ProjectAddedMessage>( this );
		_messenger.Register<ProjectRemovedMessage>( this );
	}

	public Solution Solution => _solutionManager.Solution;

	public ObservableCollection<ProjectItemViewModel> Projects { get; }

	[RelayCommand]
	public void AddProject() {
		_ = _solutionManager.AddProject(
			Strings.Default_NewProjectName,
			Strings.DuplicatedProjectNameFormat
		);
	}

	void IRecipient<ProjectAddedMessage>.Receive(
		ProjectAddedMessage message
	) {
		ProjectItemViewModel project = message.Project.ToViewModel( _messenger );
		Projects.Add( project );

		IsExpanded = true;
		project.IsSelected = true;
	}

	void IRecipient<ProjectRemovedMessage>.Receive(
		ProjectRemovedMessage message
	) {
		ProjectItemViewModel? project = Projects.FirstOrDefault( p => p.Project.Id == message.ProjectId );
		if( project is null ) {
			return;
		}

		_ = Projects.Remove( project );
	}
}
