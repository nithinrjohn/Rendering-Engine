using System.Windows;
using System.Windows.Controls;
using Editor.Editors.SceneEditor.ViewModels;

namespace Editor.Editors.SceneEditor.Views;

public partial class LogView : UserControl
{
    public LogView()
    {
        InitializeComponent();
    }

    private void OnClear(object sender, RoutedEventArgs e)
    {
        Log.Clear();
    }
}