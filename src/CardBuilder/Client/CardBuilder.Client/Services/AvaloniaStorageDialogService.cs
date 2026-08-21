using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace CardBuilder.Client.Services;

public class AvaloniaStorageDialogService : IStorageDialogService {

	async Task<string?> IStorageDialogService.OpenFileDialogAsync(
		string title,
		CancellationToken cancellationToken
	) {
		IStorageProvider? provider = GetStorageProvider();
		if( provider is null ) {
			return null;
		}

		IReadOnlyList<IStorageFile> files = await provider.OpenFilePickerAsync(
			new FilePickerOpenOptions {
				Title = title,
				AllowMultiple = false
			}
		);

		return files.Count > 0 ? files[ 0 ].Path.LocalPath : null;
	}

	private static IStorageProvider? GetStorageProvider() {
		if( Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ) {
			return TopLevel.GetTopLevel( desktop.MainWindow )?.StorageProvider;
		} else if( Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleView ) {
			return TopLevel.GetTopLevel( singleView.MainView )?.StorageProvider;
		}
		return null;
	}
}
