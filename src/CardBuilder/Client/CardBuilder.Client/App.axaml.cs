using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CardBuilder.Client.ViewModels;
using CardBuilder.Client.Views;

namespace CardBuilder.Client;

public partial class App : Application {
	public override void Initialize() {
		AvaloniaXamlLoader.Load( this );
	}

	public override void OnFrameworkInitializationCompleted() {
		if( ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ) {
			desktop.MainWindow = new MainWindow {
				DataContext = new MainViewModel(),
			};
		}

		base.OnFrameworkInitializationCompleted();
	}
}
