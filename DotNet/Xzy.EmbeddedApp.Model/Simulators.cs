using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xzy.EmbeddedApp.Model
{
   public class Simulators
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string phonenum { get; set; }
        /// <summary>
        /// 手机串号
        /// </summary>
        public string imei { get; set; }
        /// <summary>
        /// 手机id
        /// </summary>
        public string androidid { get; set; }
        
        public string created { get; set; }

    }
}
