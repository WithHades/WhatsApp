using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;

namespace Xzy.EmbeddedApp.WinForm
{
    public partial class WhatAppForm : Form
    {
        public List<int> processIdList = new List<int>();

        public WhatAppForm()
        {
            InitializeComponent();
            this.components = new System.ComponentModel.Container();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ProcessUtils.LDPath = txtPath.Text;
            if (ProcessUtils.LDPath == "")
            {
                MessageBox.Show("请先选择雷电模拟器路径");
                return;
            }
            int count = int.Parse(txtCount.Text);
            for (int i = 0; i < count; i++)
            {
                int id = ProcessUtils.Run(i);
                IntPtr idp = (IntPtr)id;
                Process[] process = Process.GetProcessesByName("dnplayer");
                foreach (var p in process)
                {
                    if (!processIdList.Contains(p.Id))
                    {
                        Thread.Sleep(500);
                        processIdList.Add(p.Id);
                        var handle = p.MainWindowHandle;
                        AppContainer.SetParent(handle, Handle);
                        AppContainer.SetWindowLong(new HandleRef(appContainer1, handle), GWL_STYLE, WS_VISIBLE);
                        break;
                    }
                }
               //Thread.Sleep(200);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        [DllImport("user32", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern uint SetWindowLong(
       IntPtr hwnd,
       int nIndex,
       uint dwNewLong
       );

        private void btnChose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                txtPath.Text = foldPath;
                txtPath.Enabled = false;
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            ProcessUtils.LDPath = txtPath.Text;
            if (ProcessUtils.LDPath == "")
            {
                MessageBox.Show("请先选择雷电模拟器路径");
                return;
            }
            Simulator simulator = new Simulator()
            {
                Cpu = 1,
                Memory = 1024,
                Width = 320,
                Height = 480,
                Dpi = 240,
                Imei = "auto"
            };
            ProcessUtils.Init(simulator);
        }
    }
}