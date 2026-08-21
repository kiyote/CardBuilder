using CardBuilder.Client.Services;
using CardBuilder.Client.ViewModels;
using CardBuilder.Core;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CardBuilder.Client;

internal static class ExtensionMethods {

	public static IServiceCollection ConfigureServices(
		this ServiceCollection services
	) {
		return services
			.AddSingleton<IMessenger>( WeakReferenceMessenger.Default )
			.AddSingleton<MainViewModel>()
			.AddServices()
			.AddCoreServices();
	}
}
