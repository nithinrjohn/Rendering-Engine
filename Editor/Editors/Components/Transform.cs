using System.Numerics;

namespace Editor.Editors.SceneEditor.Components;

public class Transform : IComponent
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public Vector3 Scale { get; set; }
}