using System.Windows;
using System.Windows.Controls;

namespace Editor.Launcher.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }
    
    private void OnQuit(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void OnMin(object sender, RoutedEventArgs e)
    {
        Window? window = Window.GetWindow(this);
        if(window != null)
            window.WindowState = WindowState.Minimized;
    }
}