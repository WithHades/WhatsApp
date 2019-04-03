using Xzy.EmbeddedApp.Model;

namespace Xzy.EmbeddedApp.WinForm.Abstract
{
    public interface ITaskManager
    {
        string Message { get;  }
        string[] Paths { get;  }
        int[] MobileIndexs { get; }

        TaskType TaskType { get; }
    }
}
