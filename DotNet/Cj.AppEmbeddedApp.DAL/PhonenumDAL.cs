using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;

namespace Cj.AppEmbeddedApp.DAL
{
    public class PhonenumDAL
    {
        private const string strSql_insert_phonenum = @"insert into phonenum (simulator_id,phonenum,status,created) 
                                                        values (@simulator_id,@phonenum,@status,unix_timestamp())";

        private const string strSql_update_phonestatus = @"update phonenum set `status`=@statusval,lastuptime=unix_timestamp() where phonenum=@phonenum";

        private const string strSql_select_phoneNumber = @"select id,simulator_id,phonenum,`status`,created from phonenum GROUP BY phonenum";
        public int UpdatePhoneStatus(int phone,int respone)
        {
            int nflag = 1;
            MySqlParameter[] par = new MySqlParameter[2];
            par[0] = new MySqlParameter("@statusval", MySqlDbType.Int32);
            par[0].Value = respone;

            par[1] = new MySqlParameter("@phonenum", MySqlDbType.Int32);
            par[1].Value = phone;

            try
            {
                nflag = MySqlHelpers.ExecuteNonQuery(MySqlHelpers.ConnectionString, CommandType.Text
                    , strSql_update_phonestatus, par);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("WhatsApp", ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }

            return nflag;
        }

        public int InsertPhoneNum(Phonenum phonenum)
        {
            int nflag = 1;

            MySqlParameter[] par = new MySqlParameter[3];
            par[0] = new MySqlParameter("@simulator_id", MySqlDbType.Int32);
            par[0].Value = phonenum.SimulatorId;

            par[1] = new MySqlParameter("@phonenum", MySqlDbType.Text);
            par[1].Value = phonenum.PhoneNum;

            par[2] = new MySqlParameter("@status", MySqlDbType.Int32);
            par[2].Value = phonenum.Status;

            try
            {
                nflag = MySqlHelpers.ExecuteNonQuery(MySqlHelpers.ConnectionString, CommandType.Text
                    , strSql_insert_phonenum, par);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("WhatsApp", ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }

            return nflag;    
        }

        public List<Phonenum> SelectPhoneNumber()
        {
           
            List<Phonenum> list = new List<Phonenum>();
            using (MySqlDataReader dr = MySqlHelpers.ExecuteReader(MySqlHelpers.ConnectionString, CommandType.Text, strSql_select_phoneNumber, null))
            {
                while(dr.Read())
                {
                    Phonenum phonenum = new Phonenum();
                    phonenum.Id = Int32.Parse(dr["id"].ToString());
                    phonenum.PhoneNum = dr["phonenum"].ToString();
                    phonenum.SimulatorId = Int32.Parse(dr["simulator_id"].ToString());
                    phonenum.Status = Int32.Parse(dr["status"].ToString());
                    phonenum.Created = dr["created"].ToString();
                    list.Add(phonenum);
                }
            }
            return list;
        }
        public void test()
        {

        }
    }
}
