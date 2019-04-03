using Xzy.EmbeddedApp.Model;

namespace Wx.Qunkong360.Wpf.Abstract
{
    public interface ITaskManager
    {
        string Message { get;  }
        string[] Paths { get;  }
        int[] MobileIndexs { get; }

        TaskType TaskType { get; }
    }
}
