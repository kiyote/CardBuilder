using CardBuilder.Model;

namespace CardBuilder.Client.Messages;

public sealed record ProjectAddedMessage(
	Project Project
);
