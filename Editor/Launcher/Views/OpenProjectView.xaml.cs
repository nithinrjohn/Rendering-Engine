using System.Windows;
using System.Windows.Controls;
using Editor.Editors.SceneEditor;
using Editor.Editors.SceneEditor.ViewModels;
using Editor.Editors.SceneEditor.Views;
using Microsoft.VisualBasic.CompilerServices;

namespace Editor.Launcher.Views;

public partial class OpenProjectView : UserControl
{

    public OpenProjectView()
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