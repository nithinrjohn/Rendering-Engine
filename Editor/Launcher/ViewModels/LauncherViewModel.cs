using Editor.Common;
using Editor.Launcher.Stores;

namespace Editor.Launcher.ViewModels;

public class LauncherViewModel : BaseViewModel
{
    private readonly NavStore _navStore;
    public BaseViewModel? CurrentViewModel => _navStore.CurrentViewModel;
    
    public LauncherViewModel(NavStore navStore)
    {
        _navStore = navStore;
        
        _navStore.CurentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
    }
}