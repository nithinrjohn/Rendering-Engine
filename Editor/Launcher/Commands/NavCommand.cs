using Editor.Common;
using Editor.Launcher.Stores;
using Editor.Launcher.ViewModels;

namespace Editor.Launcher.Commands;

public class NavCommand<TViewModel> : BaseCommand 
    where TViewModel : BaseViewModel
{
    private readonly NavStore _navStore;
    private readonly Func<TViewModel> _createViewModel;
    
    public NavCommand(NavStore navStore, Func<TViewModel> createViewModel)
    {
        _navStore = navStore;
        _createViewModel = createViewModel;
    }
    
    public override void Execute(object? parameter)
    {
        _navStore.CurrentViewModel = _createViewModel();
    }
}