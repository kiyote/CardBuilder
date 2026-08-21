using CardBuilder.Model;

namespace CardBuilder.Client.Messages;

public sealed record RemoveProjectMessage(
	ProjectId ProjectId
);
