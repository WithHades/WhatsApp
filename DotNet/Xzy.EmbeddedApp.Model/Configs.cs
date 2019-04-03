namespace Xzy.EmbeddedApp.Model
{
    public class Configs
    {
        protected int _Id;
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 语言 1;中文 2：英文
        /// </summary>
        protected int _Lang;
        public int Lang
        {
            get { return _Lang; }
            set { _Lang = value; }
        }

        /// <summary>
        /// 每行个数
        /// </summary>
        protected int _Rownums;
        public int Rownums
        {
            get { return _Rownums; }
            set { _Rownums = value; }
        }

        /// <summary>
        /// 每组数量
        /// </summary>
        protected int _Groupnums;
        public int Groupnums
        {
            get { return _Groupnums; }
            set { _Groupnums = value; }
        }
    }
}
