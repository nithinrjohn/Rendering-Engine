using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interop;
using Editor.Common;
using Editor.Editors.SceneEditor.Views;
using ProjectData = Editor.Launcher.Project.ProjectData;

namespace Editor.Editors.SceneEditor.ViewModels;

public class SceneEditorViewModel : BaseViewModel
{
    public ContentBrowserView ContentBrowserView { get; set; }
    public LogView LogView { get; set; }
    public SceneGraphView SceneGraphView { get; set; }
    
    public ObservableCollection<GridLength> ColumnWidths { get; set; } = new ObservableCollection<GridLength>
    {
        new GridLength(300, GridUnitType.Pixel),
        new GridLength(1, GridUnitType.Star),
        new GridLength(300, GridUnitType.Pixel),
    };
    
    public ObservableCollection<GridLength> RowHeights { get; set; } = new ObservableCollection<GridLength>
    {
        new GridLength(300, GridUnitType.Pixel),
        new GridLength(1, GridUnitType.Star),
        
        new GridLength(1, GridUnitType.Star),
        new GridLength(200, GridUnitType.Pixel)
    };
    
    public SceneEditorViewModel(ProjectData selectedProject)
    {
        ContentBrowserView = new ContentBrowserView()
        {
            DataContext = new ContentBrowserViewModel(selectedProject)
        };

        LogView = new LogView()
        {
            DataContext = new LogViewModel()
        };
        
        SceneGraphView = new SceneGraphView()
        {
            DataContext = new SceneGraphViewModel()
        };
    }
}