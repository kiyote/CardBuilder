using CardBuilder.Client.Messages;
using CardBuilder.Core;
using CardBuilder.Model;
using CommunityToolkit.Mvvm.Messaging;

namespace CardBuilder.Client.Services;

public sealed class SolutionManager : ISolutionManager, IRecipient<RemoveProjectMessage> {

	private readonly ISolutionService _solutionService;
	private readonly IMessenger _messenger;
	private Solution _solution = Solution.None;

	public SolutionManager(
		ISolutionService solutionService,
		IMessenger messenger
	) {
		_solutionService = solutionService;
		_messenger = messenger;
		_messenger.Register( this );
	}

	Solution ISolutionManager.Solution => _solution;

	void ISolutionManager.Load(
		Solution solution
	) {
		_solution = solution;
		_ = _messenger.Send( new SolutionChangedMessage() );
	}

	void ISolutionManager.Create(
		string name
	) {
		_solution = _solutionService.Create( name );
		_ = _messenger.Send( new SolutionChangedMessage() );
	}

	Project ISolutionManager.AddProject(
		string name,
		string duplicateNameFormat
	) {
		_solution = _solutionService.AddProject( _solution, name, duplicateNameFormat, out Project project );
		_ = _messenger.Send( new ProjectAddedMessage( project ) );
		return project;
	}

	void ISolutionManager.RemoveProject(
		ProjectId projectId
	) {
		DoRemoveProject( projectId );
	}

	void IRecipient<RemoveProjectMessage>.Receive(
		RemoveProjectMessage message
	) {
		DoRemoveProject( message.ProjectId );
	}

	private void DoRemoveProject(
		ProjectId projectId
	) {
		if( !_solution.Projects.Any( p => p.Id == projectId ) ) {
			return;
		}

		_solution = _solutionService.RemoveProject( _solution, projectId );
		_ = _messenger.Send( new ProjectRemovedMessage( projectId ) );
	}
}
