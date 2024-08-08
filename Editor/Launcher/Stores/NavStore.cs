using Editor.Common;
using Editor.Launcher.ViewModels;

namespace Editor.Launcher.Stores;

public class NavStore
{
    public event Action? CurentViewModelChanged;
    
    private BaseViewModel? _currentViewModel;
    public BaseViewModel? CurrentViewModel
    {  
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            CurentViewModelChanged?.Invoke();
        }
    }
    
    public NavStore()
    {
        CurrentViewModel = new HomeViewModel(this);
    }
}