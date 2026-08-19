using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace CardBuilder.Client.Services;

internal sealed class ClassicDesktopShutdownService : IShutdownService {
	void IShutdownService.Shutdown() {
		if( Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ) {
			// Gracefully stops the event loop and closes all windows
			desktop.Shutdown();
		}
	}
}
