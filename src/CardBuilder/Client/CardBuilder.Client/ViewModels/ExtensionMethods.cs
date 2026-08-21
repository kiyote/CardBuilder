using CardBuilder.Model;
using CommunityToolkit.Mvvm.Messaging;

namespace CardBuilder.Client.ViewModels;

public static class ExtensionMethods {

	public static ProjectItemViewModel ToViewModel(
		this Project project,
		IMessenger messenger
	) {
		return new ProjectItemViewModel( project, messenger );
	}
}
