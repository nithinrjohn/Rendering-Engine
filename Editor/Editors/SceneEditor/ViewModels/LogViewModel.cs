using System.Collections.ObjectModel;
using System.Windows.Media;
using Editor.Common;

namespace Editor.Editors.SceneEditor.ViewModels;

public class LogViewModel : BaseViewModel
{
    public static ObservableCollection<Log.Message> Messages { get; set; }

    public LogViewModel()
    {
        Messages = new ObservableCollection<Log.Message>();
        Log.Add(Log.Message.MsgType.Info, "Log Info Test.");
        Log.Add(Log.Message.MsgType.Debug, "Log Debug Test.");
        Log.Add(Log.Message.MsgType.Warning, "Log Warning Test.");
        Log.Add(Log.Message.MsgType.Error, "Log Error Test.");
    }
}