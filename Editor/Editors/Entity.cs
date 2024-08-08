using Editor.Editors.SceneEditor.Components;

namespace Editor.Editors.SceneEditor;

public class Entity
{
    List<IComponent> Components { get; } = new();
    
    public void AddComponent(IComponent component)
    {
        Components.Add(component);
    }
    
    public void RemoveComponent(IComponent component)
    {
        Components.Remove(component);
    }
}