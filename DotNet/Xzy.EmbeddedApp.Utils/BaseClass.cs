using System;


namespace Xzy.EmbeddedApp.Utils
{
    public static class BaseClass
    {
        /// <summary>
        /// 判断是否为空，为空返回true
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNull(this object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否为空，为空返回true
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNull(string data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 将obj类型转换为string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertToString(this object s)
        {
            if (s == null)
            {
                return "";
            }
            else
            {
                return Convert.ToString(s);
            }
        }

        /// <summary>
        /// 将字符串转int32
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Int32 ConvertToInt32(this string s)
        {
            int i = 0;
            if (s == null)
            {
                return 0;
            }
            else
            {
                int.TryParse(s, out i);
            }
            return i;
        }

        /// <summary>
        /// 将字符串转double
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ConvertToDouble(this object s)
        {
            double i = 0;
            if (s == null)
            {
                return 0;
            }
            else
            {
                double.TryParse(s.ToString(), out i);
            }
            return i;
        }

        /// <summary>
        /// 转换为datetime类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this string s)
        {
            DateTime dt = new DateTime();
            if (s==null||s=="")
            {
                return DateTime.Now;
            }
            DateTime.TryParse(s, out dt);
            return dt;
        }

        /// <summary>
        /// 转换为datetime类型的格式字符串
        /// </summary>
        /// <param name="s">要转换的对象</param>
        /// <param name="y">格式化字符串</param>
        /// <returns></returns>
        public static string ConvertToDateTime(this string s, string y)
        {
            DateTime dt = new DateTime();
            DateTime.TryParse(s, out dt);
            return dt.ToString(y);
        }


        /// <summary>
        /// 将字符串转换成decimal
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(this object s)
        {
            decimal d = 0;
            if (s == null || s.ToString() == "")
            {
                return 0;
            }

            Decimal.TryParse(s.ToString(), out d);

            return d;

        }

        public static string ReplaceHtml(this string s) {
            return s.Replace("&lt;","<").Replace("&gt;",">").Replace("&amp;", "&").Replace("&quot;","\"");
        }

        /// <summary>
        /// 时间戳转换为日期格式
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToTime(int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

    }
}
