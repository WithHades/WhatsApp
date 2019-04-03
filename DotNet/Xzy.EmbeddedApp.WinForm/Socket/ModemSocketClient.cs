using System;
using System.Linq;
using Xzy.EmbeddedApp.Utils;
using Xzy.EmbeddedApp.WinForm.Implementation;
using Xzy.EmbeddedApp.WinForm.Tasks;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Configuration;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json.Linq;
using Xzy.EmbeddedApp.Model;
using Cj.EmbeddedAPP.BLL;
using System.Text.RegularExpressions;

namespace Xzy.EmbeddedApp.WinForm.Socket
{
    public class ModemSocketClient
    {
        private static System.Net.Sockets.Socket _client2;
        private static Timer _timer;
        private static int ModemPort = int.Parse(ConfigurationManager.AppSettings["ModemServerPort"]);
        private static Action ReconnectAction = null;

        public static void Connect()
        {
            try
            {
                _client2 = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                InitTimer();

                _client2.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), ModemPort));

                Task.Run(() => { Process(); });
            }
            catch (Exception ex)
            {
                ReconnectAction = new Action(Reconnect);
                Console.WriteLine(ex);
                LogUtils.Error($"{ex}");
            }
        }

        private static void InitTimer()
        {
            _timer = new Timer(TimeSpan.FromSeconds(15).TotalMilliseconds);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (ReconnectAction != null)
                {
                    Console.WriteLine("reconnect action begins...");
                    Task.Run(ReconnectAction);
                    ReconnectAction = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                LogUtils.Error($"{ex}");
            }
        }

        private static void Process()
        {
            while (true)
            {
                byte[] dataBytes = new byte[4096];

                try
                {
                    var length = _client2.Receive(dataBytes);

                    if (length == 0)
                    {
                        Console.WriteLine("server socket closed...");
                        ReconnectAction = new Action(Reconnect);
                        break;
                    }

                    string rawData = Encoding.Default.GetString(dataBytes, 0, length);

                    Console.WriteLine(rawData);

                    if (rawData.Contains("+NEW_SMS") && rawData.ToLower().Contains("whatsapp"))
                    {
                        VerificationCode verificationCode = ExtractInfo(rawData);

                        Task.Run(() =>
                        {
                            HandleInfo(verificationCode);
                        });
                    }
                    //else if (rawData.Contains("+DEVICES_DISCONNECTED"))
                    //{
                    //    ReconnectAction = new Action(Reconnect);
                    //    break;
                    //}

                }
                catch (Exception ex)
                {
                    Console.WriteLine("receive exception occurs...");
                    ReconnectAction = new Action(Reconnect);
                    Console.WriteLine(ex);
                    LogUtils.Error($"{ex}");
                    break;
                }
            }
        }

        public class VerificationCode
        {
            public string PhoneNum { get; set; }
            public string Code { get; set; }
        }

        private static VerificationCode ExtractInfo(string data)
        {
            if (data.Length < 6)
            {
                return null;
            }
            else
            {
                VerificationCode verificationCode = new VerificationCode();

                try
                {
                    string[] slices = data.Split(new char[] { '┇' }, StringSplitOptions.None);

                    string phoneNum = slices[1];

                    string code = string.Empty;

                    Match match = Regex.Matches(slices[4], @"\d\d\d-\d\d\d")[0];

                    string result = match.Value.Replace("-", "");

                    verificationCode.Code = result;
                    verificationCode.PhoneNum = phoneNum;
                }
                catch (Exception ex)
                {
                    LogUtils.Error($"{ex}");
                }

                return verificationCode;
            }
        }

        private static void HandleInfo(VerificationCode verificationCode)
        {
            Console.WriteLine($"手机号：{verificationCode.PhoneNum}, 验证码：{verificationCode.Code}");

            try
            {
                var obj = new JObject() { { "tasktype", (int)TaskType.SendVerificationCode }, { "txtmsg", verificationCode.Code } };
                var list = new JArray();
                obj.Add("list", list);

                int vmIndex = VmManager.Instance.VmModels.Values.FirstOrDefault(vm => vm.PhoneNumber == verificationCode.PhoneNum).Index;

                TaskSch taskSch = new TaskSch()
                {
                    Bodys = obj.ToString(Formatting.None),
                    MobileIndex = vmIndex,
                    TypeId = (int)TaskType.SendVerificationCode,
                    Status = "waiting",

                };

                TasksBLL.CreateTask(taskSch);

                ConfigVals.IsRunning = 1;
                TasksSchedule tasks = new TasksSchedule();
                tasks.ProessTask();
            }
            catch (Exception ex)
            {
                LogUtils.Error($"{ex}");
            }
        }

        private static async void Reconnect()
        {
            _client2.Dispose();
            _client2 = null;

            bool reconnected = false;

            while (!reconnected)
            {
                try
                {
                    if (_client2 == null)
                    {
                        _client2 = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    _client2.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), ModemPort));
                    reconnected = true;
                }
                catch (Exception ex)
                {
                    await Task.Delay(10000);
                    Console.WriteLine(ex);
                    continue;
                }
            }

            Task.Run(() => { Process(); });
        }
    }
}
