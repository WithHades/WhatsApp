using Xzy.EmbeddedApp.Model;
using Cj.AppEmbeddedApp.DAL;
using System.Collections.Generic;

namespace Cj.EmbeddedAPP.BLL
{
    public class PhonenumBLL
    {
        public int InsertPhoneNum(Phonenum phonenum)
        {
            PhonenumDAL dal = new PhonenumDAL();
            int nflag = dal.InsertPhoneNum(phonenum);

            return nflag;
        }

        public static int updatePhoneStatus(int phonenum,int status)
        {
            PhonenumDAL dal = new PhonenumDAL();
            int res = dal.UpdatePhoneStatus(phonenum, status);
            return res;
        }
        public List<Phonenum> SelectPhoneNumber()
        {
            PhonenumDAL dal = new PhonenumDAL();

            return dal.SelectPhoneNumber();
        }

    }
}
