using CardBuilder.Model;

namespace CardBuilder.Core;

public sealed class SolutionService : ISolutionService {

	Solution ISolutionService.Create(
		string name
	) {
		return new Solution( name, [] );
	}

	Solution ISolutionService.AddProject(
		Solution solution,
		string name,
		string duplicateNameFormat,
		out Project project
	) {
		ProjectId projectId = GetNewProjectId( solution );
		name = GetProjectName( solution, name, duplicateNameFormat );

		project = new Project( projectId, name );
		return solution with { Projects = [ .. solution.Projects, project ] };
	}

	Solution ISolutionService.RemoveProject(
		Solution solution,
		ProjectId projectId
	) {
		return solution with { Projects = [ .. solution.Projects.Where( p => p.Id != projectId ) ] };
	}

	private static ProjectId GetNewProjectId(
		Solution solution
	) {
		HashSet<ProjectId> existingIds = [ .. solution.Projects.Select( p => p.Id ) ];
		int projectNumber = solution.Projects.Count;
		ProjectId projectId = new ProjectId( $"p{projectNumber}" );
		while( existingIds.Contains( projectId ) ) {
			projectNumber++;
			projectId = new ProjectId( $"p{projectNumber}" );
		}

		return projectId;
	}

	private static string GetProjectName(
		Solution solution,
		string name,
		string duplicatedProjectNameFormat
	) {
		HashSet<string> existingNames = [ .. solution.Projects.Select( p => p.Name ) ];
		if( !existingNames.Contains( name ) ) {
			return name;
		}
		int projectNumber = 1;
		string newName = name;
		while( existingNames.Contains( newName ) ) {
			newName = string.Format( duplicatedProjectNameFormat, name, projectNumber );
			projectNumber++;
		}
		return newName;
	}
}
