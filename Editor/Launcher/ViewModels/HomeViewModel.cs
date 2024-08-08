using System.Windows.Controls;
using System.Windows.Input;
using Editor.Common;
using Editor.Launcher.Commands;
using Editor.Launcher.Stores;
using Editor.Launcher.Views;

namespace Editor.Launcher.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ICommand NavOpenCommand { get; }
    public ICommand NavNewCommand { get; }
    
    public HomeViewModel(NavStore navStore)
    {
        NavOpenCommand = new NavCommand<OpenProjectViewModel>(navStore, () => new OpenProjectViewModel(navStore));
        NavNewCommand = new NavCommand<NewProjectViewModel>(navStore, () => new NewProjectViewModel(navStore));
    }
}