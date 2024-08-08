using System.Windows;
using Editor.Editors.SceneEditor;
using Editor.Editors.SceneEditor.ViewModels;
using Microsoft.VisualBasic.CompilerServices;
using ProjectData = Editor.Launcher.Project.ProjectData;

namespace Editor.Launcher.Commands;

public class OpenProjectCommand : BaseCommand
{
    public override void Execute(object? parameter)
    {
        ProjectData? selectedProject = parameter as ProjectData;
        EditorWindow editorWindow = new EditorWindow()
        {
            DataContext = new EditorViewModel(selectedProject)
        };
        editorWindow.Show();
        
        Window? window = Application.Current.Windows[0];
        window?.Close();
    }
}