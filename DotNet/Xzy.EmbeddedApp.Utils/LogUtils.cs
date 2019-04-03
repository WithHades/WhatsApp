using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace Xzy.EmbeddedApp.Utils
{
    public static class LogUtils
    {

        public static void SetupLogger()
        {
            string logPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "logs");

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            logPath += $"\\log_{DateTime.Now.ToString("yyyyMMdd")}.txt";

            Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(logPath)
    .CreateLogger();

        }

        public static void Debug(string msg)
        {
            Log.Logger.Debug(msg);
        }

        public static void Information(string info)
        {
            Log.Logger.Information(info);
        }

        public static void Error(string err)
        {
            Log.Logger.Error(err);
        }
    }
}
