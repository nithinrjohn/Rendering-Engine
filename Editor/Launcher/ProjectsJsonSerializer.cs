using System.IO;
using System.Text.Json;

using ProjectData = Editor.Launcher.Project.ProjectData;

namespace Editor.Launcher;

public static class ProjectsJsonSerializer
{
    private const string JsonFilePath = @"Launcher\Projects.json";
    
    public static void AddToProjectsJson(ProjectData data)
    {
        List<ProjectData> projects = LoadProjectsJson();
        
        // Add the current project to the list
        projects.Add(data);

        // Serialize the updated list of projects with indentation
        var options = new JsonSerializerOptions { WriteIndented = true };
        string updatedJson = JsonSerializer.Serialize(projects, options);

        // Write the updated JSON back to the file
        File.WriteAllText(JsonFilePath, updatedJson);
    }
    
    public static List<ProjectData> LoadProjectsJson()
    {
        List<ProjectData> projects;

        // Check if the file exists and is not empty
        if (File.Exists(JsonFilePath) && new FileInfo(JsonFilePath).Length > 0)
        {
            // Read the existing content of the file
            string existingJson = File.ReadAllText(JsonFilePath);
            // Deserialize the JSON to a list of ProjectData
            projects = JsonSerializer.Deserialize<List<ProjectData>>(existingJson) ?? new List<ProjectData>();
        }
        else
        {
            projects = new List<ProjectData>();
        }
        
        return projects;
    }
}