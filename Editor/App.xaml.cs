using System.Configuration;
using System.Data;
using System.Windows;
using Editor.Launcher;
using Editor.Launcher.Stores;
using Editor.Launcher.ViewModels;

namespace Editor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private LauncherWindow? _launcherWindow;
    private NavStore? _navStore;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        _navStore = new NavStore();
        
        _launcherWindow = new LauncherWindow()
        {
            DataContext = new LauncherViewModel(_navStore)
        };
        _launcherWindow.Show();
    }
}