using CardBuilder.Model;

namespace CardBuilder.Client.Messages;

public sealed record ProjectRemovedMessage(
	ProjectId ProjectId
);
