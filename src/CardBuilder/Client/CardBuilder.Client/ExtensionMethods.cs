using CardBuilder.Client.Services;
using CardBuilder.Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CardBuilder.Client;

internal static class ExtensionMethods {

	public static IServiceCollection ConfigureServices(
		this ServiceCollection services
	) {
		return services
			.AddSingleton<MainViewModel>()
			.AddServices();
	}
}
