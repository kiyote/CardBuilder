using CardBuilder.Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardBuilder.Client.ViewModels;

public partial class MainViewModel : ViewModelBase {

	private readonly IShutdownService _shutdownService;
	private readonly IStorageDialogService _storageService;

	// We need a public parmaeterless constructor otherwise the Preview will
	// fail to initialize the object.
	public MainViewModel() {
		_shutdownService = default!;
		_storageService = default!;
	}

	public MainViewModel(
		IShutdownService shutdownService,
		IStorageDialogService storageService
	) {
		_shutdownService = shutdownService;
		_storageService = storageService;
	}
	
	[ObservableProperty]
	public partial string Greeting { get; set; } = "Welcome to Avalonia!";

	[RelayCommand]
	public void ExitCommand() {
		_shutdownService.Shutdown();
	}

	[RelayCommand]
	public async Task OpenFileDialogAsync(
		CancellationToken cancellationToken = default
	) {
		_ = await _storageService.OpenFileDialogAsync( "Open Project", cancellationToken );
	}
}
