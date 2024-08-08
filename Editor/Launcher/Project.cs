using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Documents;
using Editor.Common;

namespace Editor.Launcher;

public class Project : BaseViewModel
{
    public class ProjectData
    {
        public string? Name {  get; set; }
        public string? Type {  get; set; }
        public string? Path {  get; set; }

        public override string ToString()
        {
            return Name + " " + Type + " " + Path;
        }
    }

    private readonly ProjectData _data = new ProjectData();
    
    private readonly List<string> _folders = new List<string>();
    
    private const string Extension = ".scene";

    public Project(string name, string type, string path)
    {
        _data.Name = name;
        _data.Type = type;
        _data.Path = path;

        _folders.Add(".Eng");
        _folders.Add("Scenes");
        _folders.Add("Resources");
    }
    
    public bool CheckIsNameValid()
    {
        if (string.IsNullOrWhiteSpace(_data.Name)) return false;
        
        if(_data.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) return false;
        
        List<ProjectData> projects = ProjectsJsonSerializer.LoadProjectsJson();

        if (projects.Count != 0)
        {
            foreach (ProjectData project in projects)
            {
                if (project.Name != null && project.Name.Equals(_data.Name))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool CheckIsPathValid()
    {
        if (string.IsNullOrWhiteSpace(_data.Name)) return false;

        if (!Directory.Exists(_data.Path)) return false;
        
        _data.Path = Path.GetFullPath(_data.Path);

        if (!_data.Path.EndsWith("\\")) _data.Path += "\\";
        
        //Add name as last folder in path
        _data.Path += _data.Name;
        
        List<ProjectData> projects = ProjectsJsonSerializer.LoadProjectsJson();

        if (projects.Count != 0)
        {
            foreach (ProjectData project in projects)
            {
                if(project.Path != null)
                {
                    if (project.Path.Equals(_data.Path))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public void CreateProject()
    {
        // Create folders
        foreach (string folder in _folders)
        {
            Directory.CreateDirectory(_data.Path + "\\" + folder);

            if (folder.StartsWith('.'))
            {
                DirectoryInfo di = new DirectoryInfo(_data.Path + "\\" + folder);
                di.Attributes |= FileAttributes.Hidden;
            }
        }
        
        // Create project file
        string scenePath = _data.Path + "\\" + _folders[1] + "\\" + _data.Name + Extension;
        var sceneFile = File.Create(scenePath);
        
        // Write project details to project file
        // using (StreamWriter writer = new StreamWriter(sceneFile))
        // {
        //     writer.WriteLine(_data.Name);
        //     writer.WriteLine(_data.Type);
        //     writer.WriteLine(_data.Path);
        // }
        
        sceneFile.Close();
        
        ProjectsJsonSerializer.AddToProjectsJson(_data);
    }
    
}
