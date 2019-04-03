using ProtoBuf;

namespace Xzy.EmbeddedApp.WinForm.Socket
{
    public class ProtoBufHelper
    {
        /// <summary>
        /// object转bytes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static byte[] ObjectToBytes<T>(T instance)
        {
            try
            {
                byte[] array;
                if (instance == null)
                {
                    array = new byte[0];
                }
                else
                {
                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                    Serializer.Serialize<T>(memoryStream, instance);
                    array = new byte[memoryStream.Length];
                    memoryStream.Position = 0L;
                    memoryStream.Read(array, 0, array.Length);
                    memoryStream.Dispose();
                }
                return array;
            }
            catch (System.Exception)
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// byte转object
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
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                memoryStream.Write(bytesData, offset, length);
                memoryStream.Position = 0L;
                T result = Serializer.Deserialize<T>(memoryStream);
                return result;
            }
            catch (System.Exception ex)
            {
                return default(T);
            }
        }
    }
}
