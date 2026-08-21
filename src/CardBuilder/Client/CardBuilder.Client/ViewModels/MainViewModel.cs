using CardBuilder.Client.Assets;
using CardBuilder.Client.Services;
using CardBuilder.Model;
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
		SolutionViewModel = new SolutionViewModel( Solution.None );
	}

	public MainViewModel(
		IShutdownService shutdownService,
		IStorageDialogService storageService
	) {
		_shutdownService = shutdownService;
		_storageService = storageService;

		SolutionViewModel = new SolutionViewModel( Solution.None );
	}

	public string Title {
		get {
			if( SolutionViewModel.Solution != Solution.None ) {
				return $"{Strings.ApplicationName} - {SolutionViewModel.Solution.Name}";
			} else {
				return Strings.ApplicationName;
			}
		}
	}

	public SolutionViewModel SolutionViewModel { get; }

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

	[RelayCommand]
	public void CreateNewSolution() {
		SolutionViewModel.Solution = new Solution(
			"New Solution",
			[
				new Project( "New Project")
			]
		);
	}
}
