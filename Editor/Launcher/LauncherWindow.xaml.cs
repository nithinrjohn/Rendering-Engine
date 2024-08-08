using System.Windows;
using System.Windows.Input;
using Editor.Launcher.ViewModels;
using Editor.Launcher.Views;

namespace Editor.Launcher;

public partial class LauncherWindow : Window
{
    public LauncherWindow()
    {
        InitializeComponent();
    }   

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        this.DragMove();
    }
}