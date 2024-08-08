using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using Editor.Common;

using ProjectData = Editor.Launcher.Project.ProjectData;

namespace Editor.Editors.SceneEditor.ViewModels;

public class FileSystemItem : INotifyPropertyChanged
{
    protected string? _name;
    public string? Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
    
    public Brush Color { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b18e6e"));
    public string Icon { get; set; } = "\xe9aa";
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class DirectoryNode : FileSystemItem
{
    public ObservableCollection<FileSystemItem> Items { get; private set; }
    
    private FileSystemWatcher? _watcher;

    public DirectoryNode(string? path)
    {
        _name = Path.GetFileName(path);
        Items = new ObservableCollection<FileSystemItem>();
        
        InitializeFileSystemWatcher(path);

        try
        {
            if (path != null)
            {
                foreach (var directoryPath in Directory.GetDirectories(path))
                {
                    if ((File.GetAttributes(directoryPath) & FileAttributes.Hidden) != FileAttributes.Hidden)
                        Items.Add(new DirectoryNode(directoryPath));
                }

                foreach (var filePath in Directory.GetFiles(path))
                {
                    Items.Add(new FileNode(Path.GetFileName(filePath)));
                }
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Error accessing {path}: {ex.Message}");
        }
    }

    private void InitializeFileSystemWatcher(string? path)
    {
        if (path != null)
        {
            _watcher = new FileSystemWatcher(path)
            {
                NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName,
                EnableRaisingEvents = true,
                IncludeSubdirectories = false
            };
        }

        if (_watcher != null)
        {
            _watcher.Created += OnFileSystemChanged;
            _watcher.Deleted += OnFileSystemChanged;
            _watcher.Renamed += OnFileSystemRenamed;
        }
    }
    
    private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    if (Directory.Exists(e.FullPath))
                    {
                        var attributes = File.GetAttributes(e.FullPath);
                        if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                        {
                            Items.Add(new DirectoryNode(e.FullPath));
                        }
                    }
                    else if (File.Exists(e.FullPath))
                    {
                        Items.Add(new FileNode(Path.GetFileName(e.FullPath)));
                    }
                    break;
                case WatcherChangeTypes.Deleted:
                    var itemToRemove = Items.FirstOrDefault(item => item.Name == Path.GetFileName(e.FullPath));
                    if (itemToRemove != null)
                    {
                        Items.Remove(itemToRemove);
                    }
                    break;
            }
        });
    }
    
    private void OnFileSystemRenamed(object sender, RenamedEventArgs e)
    {
        App.Current.Dispatcher.Invoke(() =>
        {
            var itemToRename = Items.FirstOrDefault(item => item.Name == Path.GetFileName(e.OldFullPath));
            if (itemToRename != null)
            {
                Items.Remove(itemToRename);

                if (Directory.Exists(e.FullPath))
                {
                    var attributes = File.GetAttributes(e.FullPath);
                    if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        Items.Add(new DirectoryNode(e.FullPath));
                    }
                }
                else if (File.Exists(e.FullPath))
                {
                    Items.Add(new FileNode(Path.GetFileName(e.FullPath)));
                }
            }
        });
    }
}

public class FileNode : FileSystemItem
{
    public FileNode(string? name)
    {
        _name = name;
        
        //if name is a .scene file use #8087c2
        //if name is a code file use #73c8e8
        //if name is a image file use #f177a5
        //if name is a 3d model file use #ac78bf
        //if name is a audio file use #def089
        //if name is a unknown file use white
        
        if (name != null)
        {
            if (name.EndsWith(".scene"))
            {
                Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8087c2"));
                Icon = "\xe905";
            }
            else if (name.EndsWith(".cs"))
            {
                Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#73c8e8"));
                Icon = "\xe978";
            }
            else if (name.EndsWith(".png") || name.EndsWith(".jpg") || name.EndsWith(".jpeg") || name.EndsWith(".bmp") || name.EndsWith(".gif"))
            {
                Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f177a5"));
                Icon = "\xe9e2";
            }
            else if (name.EndsWith(".fbx") || name.EndsWith(".obj") || name.EndsWith(".blend"))
            {
                Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ac78bf"));
                Icon = "\xe981";
            }
            else if (name.EndsWith(".wav") || name.EndsWith(".mp3") || name.EndsWith(".ogg"))
            {
                Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#def089"));
                Icon = "\xe9d4";
            }
            else
            {
                Color = Brushes.White;
                Icon = "\xe995";
            }
        }
    }
}

public class ContentBrowserViewModel : BaseViewModel
{
    public DirectoryNode RootDirectory { get; set; }
    
    public List<DirectoryNode> RootDirectoryAsList
    {
        get { return new List<DirectoryNode> { RootDirectory }; }
    }
    
    public ContentBrowserViewModel(ProjectData selectedProject)
    {
        string? path = selectedProject.Path;
        
        RootDirectory = new DirectoryNode(path);
    }
}