namespace Xzy.EmbeddedApp.Model
{
    public class Simulator
    {
        /// <summary>
        /// cpu核心数
        /// </summary>
        public int Cpu { get; set; }
        /// <summary>
        /// 内存（M）
        /// </summary>
        public int Memory { get; set; }
        /// <summary>
        /// 分辨率宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 分辨率高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// DPI
        /// </summary>
        public int Dpi { get; set; }
        /// <summary>
        /// IMEI
        /// </summary>
        public string Imei { get; set; }

        /// <summary>
        /// Androidid
        /// </summary>
        public string Androidid { get; set; }
    }
}
