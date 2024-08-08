using System.Windows.Media;
using Editor.Editors.SceneEditor.ViewModels;

namespace Editor.Editors.SceneEditor;

public class Log
{
    public class Message
    {
        public enum MsgType
        {
            Info,
            Debug,
            Warning,
            Error
        }
        
        public string Time { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        
        public Brush Color { get; set; }
        
        public Message(MsgType type, string content)
        {
            Content = content;
            
            Time = $"[{DateTime.Now:HH:mm:ss}]";
            
            switch (type)
            {
                case MsgType.Info:
                    Color = Brushes.White;
                    Type = "[INFO]";
                    break;
                case MsgType.Debug:
                    Color = Brushes.LightBlue;
                    Type = "[DEBUG]";
                    break;
                case MsgType.Warning:
                    Color = Brushes.Yellow;
                    Type = "[WARNING]";
                    break;
                case MsgType.Error:
                    Color = Brushes.Red;
                    Type = "[ERROR]";
                    break;
            }
        }
    }
    
    public static void Add(Message.MsgType type, string message)
    {
        LogViewModel.Messages.Add(new Message(type, message));
    }
    
    public static void Clear()
    {
        LogViewModel.Messages.Clear();
    }
}