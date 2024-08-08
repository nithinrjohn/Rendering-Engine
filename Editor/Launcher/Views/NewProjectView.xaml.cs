using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Editor.Editors.SceneEditor;
using Microsoft.Win32;

namespace Editor.Launcher.Views;

public partial class NewProjectView : UserControl
{
    public NewProjectView()
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

    private void OnCheck(object sender, RoutedEventArgs e)
    {
        string type = TwoButton.IsChecked == true ? "2D" : "3D";

        Project newProject = new Project(NameBox.Text, type, PathBox.Text);
        
        bool validName = newProject.CheckIsNameValid();
        bool validPath = newProject.CheckIsPathValid();
        
        if (!validName)
        {
            NameBox.Foreground = Brushes.Red;
        }
        else
        {
            NameBox.Foreground = Brushes.Black;
        }
        
        
        if (!validPath)
        {
            PathBox.Foreground = Brushes.Red;
        }
        else
        {
            PathBox.Foreground = Brushes.Black;
        }
        
        if (validName && validPath)
        {
            newProject.CreateProject();;
            
            // var editor = new EditorWindow(newProject.);
            // editor.Show();
            //
            // Window.GetWindow(this)?.Close();
        }
    }

    private void OnNameBoxGotFocus(object sender, RoutedEventArgs e)
    {
        NameBox.Foreground = Brushes.Black;
    }

    private void OnPathBoxGotFocus(object sender, RoutedEventArgs e)
    {
        PathBox.Foreground = Brushes.Black;
    }

    private void OnDem(object sender, RoutedEventArgs e)
    {
        if (sender.Equals(TwoButton))
        {
            if (ThreeButton.IsChecked == true)
            {
                ThreeBorder.BorderBrush = Brushes.Transparent;
                ThreeButton.IsChecked = false;
            }
            TwoBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#feead3"));
        }
        if (sender.Equals(ThreeButton))
        {
            if (TwoButton.IsChecked == true)
            {
                TwoBorder.BorderBrush = Brushes.Transparent;
                TwoButton.IsChecked = false;
            }
            ThreeBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#feead3"));
        }
    }

    private void OnBrowse(object sender, RoutedEventArgs e)
    {
        // Create OpenFileDialog
        var dlg = new OpenFolderDialog();

        // Set filter for file extension and default file extension
        // dlg.DefaultExt = ".txt";
        // dlg.Filter = "Text documents (.txt)|*.txt";

        // Display OpenFileDialog by calling ShowDialog method
        bool? result = dlg.ShowDialog();

        // Get the selected file name and display in a TextBox
        if (result == true)
        {
            // Open document
            string folderName = dlg.FolderName;
            PathBox.Text = "";
            PathBox.Text = folderName;
        }
    }
}