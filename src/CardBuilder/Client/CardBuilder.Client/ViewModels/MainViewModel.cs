using CardBuilder.Client.Assets;
using CardBuilder.Client.Services;
using CardBuilder.Core;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace CardBuilder.Client.ViewModels;

public partial class MainViewModel : ViewModelBase {

	private readonly IShutdownService _shutdownService;
	private readonly IStorageDialogService _storageService;
	private readonly ISolutionManager _solutionManager;

	// We need a public parmaeterless constructor otherwise the Preview will
	// fail to initialize the object.
	public MainViewModel() {
		_shutdownService = default!;
		_storageService = default!;
		_solutionManager = new SolutionManager( new SolutionService(), WeakReferenceMessenger.Default );
		SolutionViewModel = new SolutionTreeViewModel( _solutionManager, WeakReferenceMessenger.Default );
	}

	public MainViewModel(
		IShutdownService shutdownService,
		IStorageDialogService storageService,
		ISolutionManager solutionManager,
		IMessenger messenger
	) {
		_shutdownService = shutdownService;
		_storageService = storageService;
		_solutionManager = solutionManager;

		SolutionViewModel = new SolutionTreeViewModel( solutionManager, messenger );
	}

	public SolutionTreeViewModel SolutionViewModel { get; }

	[RelayCommand]
	public void ExitCommand() {
		_shutdownService.Shutdown();
	}

	[RelayCommand]
	public async Task OpenFileDialogAsync(
		CancellationToken cancellationToken = default
	) {
		_ = await _storageService.OpenFileDialogAsync( Strings.Title_OpenSolution, cancellationToken );
	}

	[RelayCommand]
	public void CreateNewSolution() {
		_solutionManager.Create( Strings.Default_NewSolutionName );
	}
}
