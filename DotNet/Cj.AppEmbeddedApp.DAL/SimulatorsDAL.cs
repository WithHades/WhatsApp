using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;

namespace Cj.AppEmbeddedApp.DAL
{
    public class SimulatorsDAL
    {
        /// <summary>
        /// 查询模拟器号码
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<Simulators> GetSimulatorsList()
        {
            string sql = "select id,phonenum,imei,androidid,created from simulators";
            MySqlDataReader objReader = MySqlHelpers.GetReader(sql);
            List<Simulators> list = new List<Simulators>();
            while (objReader.Read())
            {
                list.Add(new Simulators()
                {
                    id=Convert.ToInt32(objReader["id"]),
                    phonenum=objReader["phonenum"].ToString(),
                    androidid=objReader["androidid"].ToString(),
                    imei=objReader["imei"].ToString(),
                    created= objReader["created"].ToString()
                });
            };
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 新增模拟器号码
        /// </summary>
        /// <param name="objSimulators"></param>
        /// <returns></returns>
        public int UpdateSimulators(Simulators objSimulators)
        {
            //string sql = "Update simulators set phonenum=@phonenum,imei=@imei,";
            //sql += "androidid=@androidid,created=@created where id=@Id";
           // StringBuilder sqlBuilder = new StringBuilder();
            string sql= "insert into simulators (id,phonenum,imei,androidid,created)";
            sql+= " VALUES (@id,@phonenum,@imei,@androidid,@created)";
            
            MySqlParameter[] param = new MySqlParameter[]
            {
                new MySqlParameter("@Id",objSimulators.id),
                new MySqlParameter("@phonenum",objSimulators.phonenum),
                new MySqlParameter("@imei",objSimulators.imei),
                new MySqlParameter("@androidid",objSimulators.androidid),
                new MySqlParameter("@created",objSimulators.created)
            };
            return MySqlHelpers.Update(sql.ToString(), param);
        }
    }
}
