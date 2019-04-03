using Newtonsoft.Json;
using System;
using System.Text;

namespace Xzy.EmbeddedApp.WinForm.Socket
{
    public class JsonBufHelper
    {
        /// <summary>
        /// 字符转换为json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytesData"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T BytesToObject<T>(byte[] bytesData, int offset, int length)
        {
            if (bytesData.Length == 0)
            {
                return default(T);
            }
            try
            {
                byte[] buffer = new byte[length+10];

                //int recCount = 0;
                string returnMsg = Encoding.UTF8.GetString(bytesData, offset, length);
                Console.WriteLine("收到消息");
                Console.WriteLine(returnMsg);
                /*System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                memoryStream.Write(bytesData, offset, length);
                memoryStream.Position = 0L;*/
                T result = JsonConvert.DeserializeObject<T>(returnMsg);
                return result;
            }
            catch (System.Exception ex)
            {
                return default(T);
            }
        }

    }
}
