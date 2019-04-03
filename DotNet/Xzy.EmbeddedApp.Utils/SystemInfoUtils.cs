using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using System.Net;

namespace Xzy.EmbeddedApp.Utils
{
    public class SystemInfoUtils
    {
        /// <summary>  
        /// 操作系统的登录用户名  
        /// </summary>  
        /// <returns>系统的登录用户名</returns>  
        public static string GetUserName()
        {
            try
            {
                string strUserName = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    strUserName = mo["UserName"].ToString();
                }
                moc = null;
                mc = null;
                return strUserName;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取本机MAC地址  
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public static string GetMacAddress()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取本机的物理地址  
        /// </summary>  
        /// <returns></returns>  
        public static string getMacAddr_Local()
        {
            string madAddr = null;
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc2 = mc.GetInstances();
                foreach (ManagementObject mo in moc2)
                {
                    if (Convert.ToBoolean(mo["IPEnabled"]) == true)
                    {
                        madAddr = mo["MacAddress"].ToString();
                        madAddr = madAddr.Replace(':', '-');
                    }
                    mo.Dispose();
                }
                if (madAddr == null)
                {
                    return "unknown";
                }
                else
                {
                    return madAddr;
                }
            }
            catch (Exception)
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取客户端内网IPv6地址  
        /// </summary>  
        /// <returns>客户端内网IPv6地址</returns>  
        public static string GetClientLocalIPv6Address()
        {
            string strLocalIP = string.Empty;
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHost.AddressList[0];
                strLocalIP = ipAddress.ToString();
                return strLocalIP;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取客户端内网IPv4地址  
        /// </summary>  
        /// <returns>客户端内网IPv4地址</returns>  
        public static string GetClientLocalIPv4Address()
        {
            string strLocalIP = string.Empty;
            try
            {
                IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHost.AddressList[0];
                strLocalIP = ipAddress.ToString();
                return strLocalIP;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取客户端内网IPv4地址集合  
        /// </summary>  
        /// <returns>返回客户端内网IPv4地址集合</returns>  
        public static List<string> GetClientLocalIPv4AddressList()
        {
            List<string> ipAddressList = new List<string>();
            try
            {
                IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
                foreach (IPAddress ipAddress in ipHost.AddressList)
                {
                    if (!ipAddressList.Contains(ipAddress.ToString()))
                    {
                        ipAddressList.Add(ipAddress.ToString());
                    }
                }
            }
            catch
            {

            }
            return ipAddressList;
        }
     
        /// <summary>  
        /// 获取硬盘序号  
        /// </summary>  
        /// <returns>硬盘序号</returns>  
        public static string GetDiskID()
        {
            try
            {
                string strDiskID = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    strDiskID = mo.Properties["Model"].Value.ToString();
                }
                moc = null;
                mc = null;
                return strDiskID;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取CpuID  
        /// </summary>  
        /// <returns>CpuID</returns>  
        public static string GetCpuID()
        {
            try
            {
                string strCpuID = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return strCpuID;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取操作系统类型  
        /// </summary>  
        /// <returns>操作系统类型</returns>  
        public static string GetSystemType()
        {
            try
            {
                string strSystemType = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    strSystemType = mo["SystemType"].ToString();
                }
                moc = null;
                mc = null;
                return strSystemType;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取操作系统名称  
        /// </summary>  
        /// <returns>操作系统名称</returns>  
        public static string GetSystemName()
        {
            try
            {
                string strSystemName = string.Empty;
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT PartComponent FROM Win32_SystemOperatingSystem");
                foreach (ManagementObject mo in mos.Get())
                {
                    strSystemName = mo["PartComponent"].ToString();
                }
                mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption FROM Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.Get())
                {
                    strSystemName = mo["Caption"].ToString();
                }
                return strSystemName;
            }
            catch
            {
                return "unknown";
            }
        }
        /// <summary>  
        /// 获取物理内存信息  
        /// </summary>  
        /// <returns>物理内存信息</returns>  
        public static string GetTotalPhysicalMemory()
        {
            try
            {
                string strTotalPhysicalMemory = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    strTotalPhysicalMemory = mo["TotalPhysicalMemory"].ToString();
                }
                moc = null;
                mc = null;
                return strTotalPhysicalMemory;
            }
            catch
            {
                return "unknown";
            }
        }

        /// <summary>  
        /// 获取主板id  
        /// </summary>  
        /// <returns></returns>  
        public static string GetMotherBoardID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_BaseBoard");
                ManagementObjectCollection moc = mc.GetInstances();
                string strID = null;
                foreach (ManagementObject mo in moc)
                {
                    strID = mo.Properties["SerialNumber"].Value.ToString();
                    break;
                }
                return strID;
            }
            catch
            {
                return "unknown";
            }
        }

        /// <summary>  
        /// 获取公用桌面路径
        /// 
        public static string GetAllUsersDesktopFolderPath()
        {
            RegistryKey folders;
            folders = OpenRegistryPath(Registry.LocalMachine, @"/software/microsoft/windows/currentversion/explorer/shell folders");
            string desktopPath = folders.GetValue("Common Desktop").ToString();
            return desktopPath;
        }

        /// <summary>  
        /// 获取公用启动项路径  
        /// </summary>  
        public static string GetAllUsersStartupFolderPath()
        {
            RegistryKey folders;
            folders = OpenRegistryPath(Registry.LocalMachine, @"/software/microsoft/windows/currentversion/explorer/shell folders");
            string Startup = folders.GetValue("Common Startup").ToString();
            return Startup;
        }
        private static RegistryKey OpenRegistryPath(RegistryKey root, string s)
        {
            s = s.Remove(0, 1) + @"/";
            while (s.IndexOf(@"/") != -1)
            {
                root = root.OpenSubKey(s.Substring(0, s.IndexOf(@"/")));
                s = s.Remove(0, s.IndexOf(@"/") + 1);
            }
            return root;
        }
    }
}
