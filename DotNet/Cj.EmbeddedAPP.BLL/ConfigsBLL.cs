using Xzy.EmbeddedApp.Model;
using Cj.AppEmbeddedApp.DAL;

namespace Cj.EmbeddedApp.BLL
{
    public class ConfigsBLL
    {
        /*public static IList<Configs> GetAllData()
        {
            ConfigDALs condal = new ConfigDALs();
            return condal.GetConfigs();
        }*/
        public Configs GetAllData()
        {
            ConfigDALs condal = new ConfigDALs();
            return condal.GetConfigs();
        }

        public int SaveConfigs(int lang, int groupnums, int rownums)
        {
            ConfigDALs dal = new ConfigDALs();
            int nflag = dal.InsertConfigs(lang, groupnums, rownums);
            return nflag;
        }
    }
}
