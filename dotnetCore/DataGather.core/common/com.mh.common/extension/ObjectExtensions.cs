using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using com.mh.common.configuration;

namespace com.mh.common.extension
{

    public static class ObjectExt
    {
        /*********************************************************************************************************************
         *****  object
         */
        public static string ToString2(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        public static Decimal ToDecimal(this object obj)
        {
            if (obj == null)
                return 0;
            else
                return Convert.ToDecimal(obj);
        }


        public static int ToInt32(this object obj)
        {
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }
        public static string ToJson(this object obj)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return str;
        }

        /*********************************************************************************************************************
         ***** string
         */

        public static bool IsNull(this string st)
        {
            return string.IsNullOrWhiteSpace(st);

            // string.IsNullOrWhiteSpace = String.IsNullOrEmpty(value) || value.Trim().Length == 0;
        }

        public static bool IsEmail(this string em)
        {
            var reg = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex g = new Regex(reg);
            return g.IsMatch(em);
        }

        public static bool IsMobile(this string st)
        {
            if (st.IsNull())
                return false;
            var reg = @"^[1][0-9]{10}$";
            Regex g = new Regex(reg);
            return g.IsMatch(st);
        }

        public static string ReplaceHtmlTag(this string str)
        {
            var strText = Regex.Replace(str, "<[^>]+>", "");
            strText = Regex.Replace(strText, "&[^;]+;", "");
            return strText;
        }



        /*********************************************************************************************************************
         ***** DateTime
         */
        public static DateTime FirstDayOfMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).Date;
        }

        public static bool IsFirstDayOfMonth(this DateTime datetime)
        {
            return datetime.Date == datetime.FirstDayOfMonth();
        }

        public static DateTime LastDayOfMonth(this DateTime datetime)
        {
            return datetime.FirstDayOfMonth().AddMonths(1).AddDays(-1).Date;
        }

        public static DateTime LastTimeOfDay(this DateTime datetime)
        {
            return datetime.Date.AddDays(1).AddSeconds(-1);
        }
        public static DateTime FirstDayOfWeek(this DateTime datetime)
        {
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (datetime.DayOfWeek == DayOfWeek.Sunday ? (7 - 1) : (weeknow - 1));
            return datetime.AddDays(-weeknow).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime datetime)
        {
            var date = datetime.FirstDayOfWeek();
            return date.AddDays(6);
        }

        public static bool IsFirstDayOfWeek(this DateTime datetime)
        {
            return datetime.DayOfWeek == DayOfWeek.Monday;
        }
        public static int WeekOfYear(this DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }
        public static int SeasonOfYear(this DateTime dt)
        {
            if (dt.Month > 9)
                return 4;
            if (dt.Month > 6 && dt.Month < 10)
                return 3;
            if (dt.Month > 3 && dt.Month < 7)
                return 2;
            if (dt.Month < 4)
                return 1;
            return 0;
        }

        public static string Star(this DateTime dt)
        {
            var list = new dynamic[]
            {
                new { Name = "摩羯座", FromDate = new DateTime(1999, 12, 22), ToDate = new DateTime(2000, 1, 19) },
                new { Name = "水瓶座", FromDate = new DateTime(2000, 1,  20), ToDate = new DateTime(2000, 2 , 18) },
                new { Name = "双鱼座", FromDate = new DateTime(2000, 2,  19), ToDate = new DateTime(2000, 3 , 20) },
                new { Name = "白羊座", FromDate = new DateTime(2000, 3,  21), ToDate = new DateTime(2000, 4 , 20) },
                new { Name = "金牛座", FromDate = new DateTime(2000, 4,  21), ToDate = new DateTime(2000, 5 , 20) },
                new { Name = "双子座", FromDate = new DateTime(2000, 5,  21), ToDate = new DateTime(2000, 6 , 21) },
                new { Name = "巨蟹座", FromDate = new DateTime(2000, 6,  22), ToDate = new DateTime(2000, 7 , 22) },
                new { Name = "狮子座", FromDate = new DateTime(2000, 7,  23), ToDate = new DateTime(2000, 8 , 22) },
                new { Name = "处女座", FromDate = new DateTime(2000, 8,  23), ToDate = new DateTime(2000, 9 , 22) },
                new { Name = "天秤座", FromDate = new DateTime(2000, 9,  23), ToDate = new DateTime(2000, 10, 22) },
                new { Name = "天蝎座", FromDate = new DateTime(2000, 10, 23), ToDate = new DateTime(2000, 11, 21) },
                new { Name = "射手座", FromDate = new DateTime(2000, 11, 22), ToDate = new DateTime(2000, 12, 21) },
            };

            dt = new DateTime(2000, dt.Month, dt.Day);
            var m = list.FirstOrDefault(l => l.FromDate <= dt && l.ToDate >= dt);
            return m != null ? m.Name : list[0].Name;
        }


        /*********************************************************************************************************************
         ***** Dictionary
         */
        public static Dictionary<string, string> Set(this Dictionary<string, string> dic, string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                return dic;
            if (dic.ContainsKey(key))
                dic[key] = value;
            else
                dic.Add(key, value);
            return dic;
        }
        public static string Get(this Dictionary<string, string> dic, string key, string value = "")
        {
            if (string.IsNullOrEmpty(key))
                return value;
            if (dic.ContainsKey(key))
                return dic[key];
            return value;
        }
        public static Dictionary<int, string> Merge(this Dictionary<int, string> first, Dictionary<int, string> second)
        {
            if (first == null) first = new Dictionary<int, string>();
            if (second == null) return first;

            foreach (int key in second.Keys)
            {
                if (!first.ContainsKey(key))
                    first.Add(key, second[key]);
            }
            return first;
        }


        public static string FriendlyName(this Enum proStatus)
        {
            FieldInfo EnumInfo = proStatus.GetType().GetField(proStatus.ToString());
            DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.
                GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (EnumAttributes.Length > 0)
            {
                return EnumAttributes[0].Description;
            }
            return proStatus.ToString();
        }


        /*********************************************************************************************************************
        ***** ImageUrlWithSize
        */

        public static string ImageUrlWithSize(string name, string size, string imageExtension = "jpg")
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            return string.Format($"{Path.Combine(ConfigManager.GetHttpImagePath(), name)}_{size}.{imageExtension}");
        }
        public static string Image160Url(this string name)
        {
            return ImageUrlWithSize(name, "160x0");
        }

        public static string Image100Url(this string name)
        {
            return ImageUrlWithSize(name, "100x100");
        }

        public static bool IsEmpty<T>(this List<T> l)
        {
            return (l == null || l.Count < 1);
        }
    }
}