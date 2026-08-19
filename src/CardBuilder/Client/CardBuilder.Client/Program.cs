using Avalonia;
using System;

namespace CardBuilder.Client;

public sealed class Program {
	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(
		string[] args
	) {
		_ = AppBuilder.Configure<App>()
			.UsePlatformDetect()
#if DEBUG
			.WithDeveloperTools()
#endif
			.WithInterFont()
			.LogToTrace()
			.StartWithClassicDesktopLifetime( args );
	}
}
