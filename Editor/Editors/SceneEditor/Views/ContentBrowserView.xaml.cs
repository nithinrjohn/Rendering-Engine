using System.IO;
using System.Windows;
using System.Windows.Controls;
using Editor.Editors.SceneEditor.ViewModels;
using Microsoft.Win32;

namespace Editor.Editors.SceneEditor.Views;

public partial class ContentBrowserView : UserControl
{
    public ContentBrowserView()
    {
        InitializeComponent();
    }

    private void OnAdd(object sender, RoutedEventArgs e)
    {
        // Create file by opening file dialog
        var dialog = new SaveFileDialog();
        dialog.Filter = "All files (*.*)|*.*";
        dialog.RestoreDirectory = true;
        
        if (dialog.ShowDialog() == true)
        {
            var filePath = dialog.FileName;
            File.Create(filePath).Close();
        }
    }
}