using CardBuilder.Core;
using Microsoft.Extensions.DependencyInjection;

namespace CardBuilder.Client.Services;

public static class ExtensionMethods {

	public static IServiceCollection AddServices(
		this IServiceCollection services )
	{
		return services
			.AddSingleton<ISolutionManager, SolutionManager>()
			.AddSingleton<IShutdownService, ClassicDesktopShutdownService>()
			.AddSingleton<IStorageDialogService, AvaloniaStorageDialogService>();
	}
}
