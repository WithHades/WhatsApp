namespace Xzy.EmbeddedApp.Model
{
    public class Phonenum
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 模拟器编号
        /// </summary>
        public int SimulatorId { get; set; }

        /// <summary>
        /// 模拟器编号
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string Created { get; set; }
    }
}
