using System;
using System.Collections.Generic;
using System.Data;
using Xzy.EmbeddedApp.Model;
using MySql.Data.MySqlClient;
using Xzy.EmbeddedApp.Utils;

namespace Cj.AppEmbeddedApp.DAL
{
    public class ConfigDALs
    {
        private const string strSql_select_allconfigs = @"select id,lang,rownums,groupnums from config";
        private const string strSql_insert_createconfigs = @"insert into config (lang,rownums,groupnums,created) values (@lang,@rownums,@groupnums,unix_timestamp())";
        private const string strSql_update_createconfigs = @"update config set lang=@lang,rownums=@rownums,groupnums=@groupnums,created=unix_timestamp() where id=@id";

        public Configs GetConfigs()
        {            
            Configs config = new Configs();
            List<Configs> list = new List<Configs>();
            using (MySqlDataReader dr = MySqlHelpers.ExecuteReader(MySqlHelpers.ConnectionString, CommandType.Text, strSql_select_allconfigs, null))
            {
                if(dr.Read())
                {
                    config.Id = Int32.Parse(dr["id"].ToString());
                    config.Lang = Int32.Parse(dr["lang"].ToString());
                    config.Rownums = Int32.Parse(dr["rownums"].ToString());
                    config.Groupnums = Int32.Parse(dr["groupnums"].ToString());
                }
            }
            return config;
        }

        /// <summary>
        /// 保存配置参数
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="groupnums"></param>
        /// <param name="rownums"></param>
        /// <returns></returns>
        public int InsertConfigs(int lang,int groupnums,int rownums)
        {
            int nflag = 0;
            MySqlParameter[] par = new MySqlParameter[4];
            par[0] = new MySqlParameter("@lang", MySqlDbType.Int32);
            par[0].Value = lang;

            par[1] = new MySqlParameter("@rownums", MySqlDbType.Int32);
            par[1].Value = rownums;

            par[2] = new MySqlParameter("@groupnums", MySqlDbType.Int32);
            par[2].Value = groupnums;

            par[3] = new MySqlParameter("@id", MySqlDbType.Int32);
            par[3].Value = 1;

            try
            {
                nflag = MySqlHelpers.ExecuteNonQuery(MySqlHelpers.ConnectionString, CommandType.Text
                    , strSql_update_createconfigs, par);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("WhatsApp", ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }

            return nflag;
        }
    }
}
