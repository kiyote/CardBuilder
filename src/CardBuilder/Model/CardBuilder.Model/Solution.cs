namespace CardBuilder.Model;

public sealed record Solution(
	string Name,
	IReadOnlyList<Project> Projects
) {
	public static readonly Solution None = new Solution( "", [] );
}
