using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.common.helper
{
   public class Utility
    {

        public static long GetSecond2015()
        {
            DateTime start = new DateTime(2015, 1, 1);
            TimeSpan duration = DateTime.Now - start;
            long sec = duration.Seconds + duration.Minutes * 60 + duration.Hours * 60 * 60 + duration.Days * 24 * 60 * 60;
            return sec;
        }

        public static long GetSecond1970()
        {
            DateTime start = new DateTime(1970, 1, 1);
            TimeSpan duration = DateTime.Now - start;
            long sec = duration.Seconds + duration.Minutes * 60 + duration.Hours * 60 * 60 + duration.Days * 24 * 60 * 60;
            return sec;
        }

        public static string GetDataRandom()
        {
            return System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

    }
}
