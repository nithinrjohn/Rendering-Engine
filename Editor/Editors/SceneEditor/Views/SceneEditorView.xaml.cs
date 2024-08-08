using System.Windows.Controls;

namespace Editor.Editors.SceneEditor.Views;

public partial class SceneEditorView : UserControl
{
    public SceneEditorView()
    {
        InitializeComponent();
        var glHost = new OpenGLHost(BorderHost);
        BorderHost.Child = glHost;
    }
}