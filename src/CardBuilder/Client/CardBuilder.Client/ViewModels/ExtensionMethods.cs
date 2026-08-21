using CardBuilder.Model;

namespace CardBuilder.Client.ViewModels;

public static class ExtensionMethods {

	public static SolutionItemViewModel ToViewModel(
		this Solution solution
	) {
		return new SolutionItemViewModel {
			ItemId = solution.Name,
			ItemType = SolutionItemType.Solution,
			Name = solution.Name,
			Children = [ .. solution.Projects.Select( p => p.ToViewModel() ) ]
		};
	}

	public static SolutionItemViewModel ToViewModel(
		this Project project
	) {
		return new SolutionItemViewModel {
			ItemId = project.Name,
			ItemType = SolutionItemType.Project,
			Name = project.Name,
			Children = [  ]
		};
	}
}
