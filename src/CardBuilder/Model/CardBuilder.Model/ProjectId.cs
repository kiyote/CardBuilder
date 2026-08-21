namespace CardBuilder.Model;

public readonly record struct ProjectId(
	string Value
) {
	public static readonly ProjectId None = new ProjectId( "" );

	public override string ToString() {
		return Value;
	}
}
