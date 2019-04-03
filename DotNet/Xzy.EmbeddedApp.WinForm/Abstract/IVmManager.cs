using System.Collections.Generic;

namespace Xzy.EmbeddedApp.WinForm.Abstract
{

    public interface IVmManager
    {
        void Initialize(int totalVmNumber, int groupCapacity);

        int MaxVmNumber { get; }
        int Row { get; }
        int Column { get; }

        int[,] VmIndexArray { get; }
        int RunningGroupIndex { get; }

        /// <summary>
        /// 模拟器索引和相关信息一一对应的字典
        /// </summary>
        Dictionary<int, VmModel> VmModels { get; }

        /// <summary>
        /// 添加模拟器索引及其相关信息
        /// </summary>
        /// <param name="vmIndex">模拟器索引</param>
        /// <param name="vmModel">模拟器相关信息</param>
        void AddVmModel(int vmIndex, VmModel vmModel);
    }
}
