namespace CardBuilder.Client.Services;

public interface IStorageDialogService {

	Task<string?> OpenFileDialogAsync(
		string title,
		CancellationToken cancellationToken
	);

}
