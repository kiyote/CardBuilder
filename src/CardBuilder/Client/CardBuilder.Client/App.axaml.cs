using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CardBuilder.Client.ViewModels;
using CardBuilder.Client.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CardBuilder.Client;

public partial class App : Application {
	public override void Initialize() {
		AvaloniaXamlLoader.Load( this );
	}

	public override void OnFrameworkInitializationCompleted() {

		ServiceProvider services = new ServiceCollection()
			.ConfigureServices()
			.BuildServiceProvider();

		if( ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ) {
			desktop.MainWindow = new MainWindow {
				DataContext = services.GetRequiredService<MainViewModel>()
			};
		}

		base.OnFrameworkInitializationCompleted();
	}
}
