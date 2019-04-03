using Xzy.EmbeddedApp.Model;

namespace Xzy.EmbeddedApp.Utils
{
    public static class ProcessUtils
    {
        public static string LDPath = "";
        
        /// <summary>
        /// 初始化雷电模拟器参数
        /// </summary>
        /// <param name="path"></param>
        /// <param name="simulator"></param>
        public static void Init(Simulator simulator)
        {
            string cmdCpu = $"--cpu {simulator.Cpu}";
            string cmdResolution = $"--resolution {simulator.Width},{simulator.Height},{simulator.Dpi}";
            string cmdMemory = $"--memory {simulator.Memory}";
            //string cmdImei = $"--imei {simulator.Imei}";
            string androidid = $"--androidid {simulator.Androidid}";
            string cmdStr = $"{LDPath}/dnconsole.exe modify --index 0 {cmdResolution} {cmdCpu} {cmdMemory} {androidid}";
            CmdUtils.RunCmd(cmdStr);
        }

        /// <summary>
        /// 初始化雷电模拟器参数
        /// </summary>
        /// <param name="path"></param>
        /// <param name="simulator"></param>
        public static void Init(int index,Simulator simulator)
        {
            string cmdCpu = $"--cpu {simulator.Cpu}";
            string cmdResolution = $"--resolution {simulator.Width},{simulator.Height},{simulator.Dpi}";
            string cmdMemory = $"--memory {simulator.Memory}";
            //string cmdImei = $"--imei {simulator.Imei}";
            string androidid = $"--androidid {simulator.Androidid}";
            string cmdStr = $"{LDPath}/dnconsole.exe modify --index {index} {cmdResolution} {cmdCpu} {cmdMemory} {androidid}";
            CmdUtils.RunCmd(cmdStr);
        }

        /// <summary>
        /// 退出模拟器
        /// </summary>
        /// <param name="index"></param>
        public static void Quit(int index)
        {
            string cmdStr = $"{LDPath}/dnconsole.exe quit --index {index}";
            CmdUtils.RunCmd(cmdStr);
        }

        public static void QuitAll()
        {
            string cmdStr = $"{LDPath}/dnconsole.exe quitall";
            CmdUtils.RunCmd(cmdStr);
        }

        public static int Run(int index)
        {
            string cmdStr = $"{LDPath}/dnconsole.exe launch --index {index}";
            var cmd = CmdUtils.RunCmd(cmdStr);
            int id = cmd.Item2;
            return id;
            //Process.GetProcessById(cmd.Item2).MainWindowHandle;
        }

        /// <summary>
        /// 启动一个App
        /// </summary>
        /// <param name="index"></param>
        /// <param name="packagename"></param>
        /// <returns></returns>
        public static int AdbOpenApps(int index,string packagename)
        {
            string package = $"--packagename {packagename}";
            string cmdStr = $"{LDPath}/dnconsole.exe runapp --index {index} {package}";
            var cmd = CmdUtils.RunCmd(cmdStr);

            return 1;
        }

        /// <summary>
        /// 关闭一个App
        /// </summary>
        /// <param name="index"></param>
        /// <param name="packagename"></param>
        /// <returns></returns>
        public static int AdbCloseApps(int index,string packagename)
        {
            string package = $"--packagename {packagename}";
            string cmdStr = $"{LDPath}/dnconsole.exe killapp --index {index} {package}";
            var cmd = CmdUtils.RunCmd(cmdStr);
            return 1;
        }  
        
        public static void AdbSortDevices()
        {
            string cmdStr = $"{LDPath}/dnconsole.exe sortWnd";
            var cmd = CmdUtils.RunCmd(cmdStr);
        }

        /// <summary>
        /// 安装apps
        /// </summary>
        /// <param name="index"></param>
        /// <param name="filename"></param>
        public static int AdbInstallApp(int index,string filename)
        {
            string files = $"--filename {filename} ";
            string cmdStr = $"{LDPath}/dnconsole.exe installapp --index {index} {files}";
            var cmd = CmdUtils.RunCmd(cmdStr);

            return 1;
        }

        /// <summary>
        /// 卸载app
        /// </summary>
        /// <param name="index"></param>
        /// <param name="packagename"></param>
        /// <returns></returns>
        public static int AdbUnInstallApp(int index, string packagename)
        {
            return 1;
        }

        public static int PushFileToVm(int index, string sourceFilePath, string targetFilePath)
        {
            string cmd = $"{LDPath}/adb.exe -s 127.0.0.1:{5555 + index * 2} push {sourceFilePath} {targetFilePath}";

            CmdUtils.RunCmd(cmd);

            return 1;
        }
    }
}