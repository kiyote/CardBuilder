using CardBuilder.Model;

namespace CardBuilder.Core;

public interface ISolutionService {

	Solution Create(
		string name
	);

	Solution AddProject(
		Solution solution,
		string name,
		string duplicateNameFormat,
		out Project project
	);

	Solution RemoveProject(
		Solution solution,
		ProjectId projectId
	);
}
