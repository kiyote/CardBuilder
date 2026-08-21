namespace CardBuilder.Model;

public sealed record Project(
	ProjectId Id,
	string Name
) {
	public static readonly Project None = new Project( ProjectId.None, "" );
}
