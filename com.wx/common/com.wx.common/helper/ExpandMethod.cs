using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.common.helper
{
    public static class ExpandMethod
    {
        public static string Frmt(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        public static byte[] ToByte(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }


        public static T DesJson<T>(this string obj)
        {
          return  JsonConvert.DeserializeObject<T>(obj);
        }



    }
}
