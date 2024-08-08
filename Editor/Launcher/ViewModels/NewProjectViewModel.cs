using System.Windows.Input;
using Editor.Common;
using Editor.Launcher.Commands;
using Editor.Launcher.Stores;

namespace Editor.Launcher.ViewModels;

public class NewProjectViewModel : BaseViewModel
{
    public ICommand NavBackCommand { get; }
    public ICommand NavCheckCommand { get; }
    
    public NewProjectViewModel(NavStore navStore)
    {
        NavBackCommand = new NavCommand<HomeViewModel>(navStore, () => new HomeViewModel(navStore));
        NavCheckCommand = new NavCommand<OpenProjectViewModel>(navStore, () => new OpenProjectViewModel(navStore));
    }
}