using Microsoft.Extensions.DependencyInjection;

namespace CardBuilder.Core; 

public static class ExtensionMethods {

	public static IServiceCollection AddCoreServices(
		this IServiceCollection services
	) {
		return services
			.AddSingleton<ISolutionService, SolutionService>();
	}
}
