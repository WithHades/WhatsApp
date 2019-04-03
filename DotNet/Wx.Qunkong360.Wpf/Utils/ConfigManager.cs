using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xzy.EmbeddedApp.Model;

namespace Wx.Qunkong360.Wpf.Utils
{
    public class ConfigManager
    {
        public static readonly ConfigManager Instance = new ConfigManager();

        public ConfigModel Config { get; set; }

        private ConfigManager()
        {

        }
    }
}
