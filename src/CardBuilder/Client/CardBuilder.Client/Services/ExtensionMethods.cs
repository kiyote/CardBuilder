using Microsoft.Extensions.DependencyInjection;

namespace CardBuilder.Client.Services;

public static class ExtensionMethods {

	public static IServiceCollection AddServices(
		this IServiceCollection services )
	{
		return services
			.AddSingleton<IShutdownService, ClassicDesktopShutdownService>()
			.AddSingleton<IStorageDialogService, AvaloniaStorageDialogService>();
	}
}
