using CardBuilder.Model;

namespace CardBuilder.Client.Services;

public interface ISolutionManager {

	Solution Solution { get; }

	void Load(
		Solution solution
	);

	void Create(
		string name
	);

	Project AddProject(
		string name,
		string duplicateNameFormat
	);

	void RemoveProject(
		ProjectId projectId
	);
}
