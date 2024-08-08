using Editor.Common;

using ProjectData = Editor.Launcher.Project.ProjectData;

namespace Editor.Editors.SceneEditor.ViewModels;

public class EditorViewModel : BaseViewModel
{

    private BaseViewModel? _currentViewModel;
    public BaseViewModel? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
        
    public EditorViewModel(ProjectData? selectedProject)
    {
        if (selectedProject != null) CurrentViewModel = new SceneEditorViewModel(selectedProject);
    }
}