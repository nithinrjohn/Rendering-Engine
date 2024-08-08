using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Editor.Common;
using Editor.Launcher.Commands;
using Editor.Launcher.Stores;

using ProjectData = Editor.Launcher.Project.ProjectData;

namespace Editor.Launcher.ViewModels;

public class OpenProjectViewModel : BaseViewModel
{
    public ICommand NavBackCommand { get; }
    public ICommand OpenProjectCommand { get; }
    private ProjectData? _selectedProject;
    public ProjectData? SelectedProject
    {
        get => _selectedProject;
        set
        {
            if (_selectedProject != value)
            {
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }
    }

    public ReadOnlyObservableCollection<ProjectData> ProjectsCollection { get; }
    
    public OpenProjectViewModel(NavStore navStore)
    {
        NavBackCommand = new NavCommand<HomeViewModel>(navStore, () => new HomeViewModel(navStore));
        OpenProjectCommand = new OpenProjectCommand();
        
        List<ProjectData> projects = ProjectsJsonSerializer.LoadProjectsJson();
        ProjectsCollection = new ReadOnlyObservableCollection<ProjectData>(new ObservableCollection<ProjectData>(projects));
    }
}