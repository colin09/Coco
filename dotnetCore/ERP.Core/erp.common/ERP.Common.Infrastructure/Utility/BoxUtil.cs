using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Utility
{
    public static class BoxUtil
    {
        public static readonly DateTime MinDate = (DateTime)SqlDateTime.MinValue;

        #region 从对象中获取String

        /// <summary>
        /// 从对象中获取String
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetStringFromObject(object o)
        {
            return GetStringFromObject(o, string.Empty);
        }

        /// <summary>
        /// 从对象中获取String
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static string GetStringFromObject(object o, string leap)
        {
            string rtn = string.Empty;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToString(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Float

        /// <summary>
        /// 从对象中获取Float
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static float GetFloatFromObject(object o)
        {
            return GetFloatFromObject(o, 0);
        }

        /// <summary>
        /// 从对象中获取Float
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static float GetFloatFromObject(object o, int leap)
        {
            float rtn = 0;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = float.Parse(o.ToString());
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取double

        /// <summary>
        /// 从对象中获取double
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static double GetDoubleFromObject(object o)
        {
            return GetFloatFromObject(o, 0);
        }

        /// <summary>
        /// 从对象中获取double
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static double GetDoubleFromObject(object o, double leap)
        {
            double rtn = 0;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = double.Parse(o.ToString());
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Int32

        /// <summary>
        /// 从对象中获取Int32
        /// added by lwy 06-03-30 20:17
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int GetInt32FromObject(object o)
        {
            return GetInt32FromObject(o, 0);
        }

        /// <summary>
        /// 从对象中获取Int32
        /// added by lwy 06-03-30 20:17
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static int GetInt32FromObject(object o, int leap)
        {
            int rtn = 0;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToInt32(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Int16

        /// <summary>
        /// 从对象中获取Int16
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static short GetInt16FromObject(object o)
        {
            return GetInt16FromObject(o, 0);
        }

        /// <summary>
        /// 从对象中获取Int16
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static short GetInt16FromObject(object o, short leap)
        {
            short rtn = 0;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToInt16(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Byte

        /// <summary>
        /// 从对象中获取Byte
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static byte GetByteFromObject(object o)
        {
            return GetByteFromObject(o, 0);
        }

        /// <summary>
        /// 从对象中获取Byte
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static byte GetByteFromObject(object o, byte leap)
        {
            byte rtn = 0;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToByte(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Int64

        /// <summary>
        /// 从对象中获取Int64
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static long GetInt64FromObject(object o)
        {
            return GetInt64FromObject(o, 0);
        }

        /// <summary>
        /// 从对象中获取Int64
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static long GetInt64FromObject(object o, long leap)
        {
            long rtn = 0;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToInt64(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Decimal

        /// <summary>
        /// 从对象中获取Decimal
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Decimal GetDecimalFromObject(object o)
        {
            return GetDecimalFromObject(o, 0);
        }

        /// <summary>
        /// 从对象中获取Decimal
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static Decimal GetDecimalFromObject(object o, Decimal leap)
        {
            Decimal rtn = 0;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToDecimal(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Boolean

        public static bool GetBooleanFromObject(object o)
        {
            return GetBooleanFromObject(o, false);
        }

        public static bool GetBooleanFromObject(object o, bool leap)
        {
            bool rtn = false;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToBoolean(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取DateTime
        public static DateTime GetDateTimeFromObject(object o)
        {
            return GetDateTimeFromObject(o, DateTime.MinValue);
        }

        public static DateTime GetDateTimeFromObject(object o, DateTime leap)
        {
            DateTime rtn = DateTime.MinValue;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Guid

        public static Guid GetGuidFromObject(object o)
        {
            return GetGuidFromObject(o, Guid.Empty);
        }

        public static Guid GetGuidFromObject(object o, Guid leap)
        {
            Guid rtn = Guid.Empty;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = new Guid(o.ToString());
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        #region 从对象中获取Char

        /// <summary>
        /// 从对象中获取Char
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int GetCharFromObject(object o)
        {
            return GetCharFromObject(o, 'A');
        }

        /// <summary>
        /// 从对象中获取Char
        /// </summary>
        /// <param name="o"></param>
        /// <param name="leap"></param>
        /// <returns></returns>
        public static int GetCharFromObject(object o, char leap)
        {
            int rtn = 0;
            if (o == null)
            {
                return leap;
            }

            try
            {
                rtn = Convert.ToChar(o);
            }
            catch (Exception)
            {
                rtn = leap;
            }

            return rtn;
        }

        #endregion

        public static Byte[] ConvertByteFromObject(object o)
        {
            return ConvertByteFromObject(o, null);
        }

        public static Byte[] ConvertByteFromObject(object o, Byte[] bytes)
        {
            Byte[] b = bytes;
            if (o != null)
            {
                try
                {
                    //b = (byte[]) o;
                    //b = System.Text.Encoding.Default.GetBytes(BoxUtil.GetStringFromObject(o));
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream rems = new MemoryStream();
                    formatter.Serialize(rems, o);
                    b = rems.GetBuffer();
                }
                catch (Exception)
                {
                }
            }

            return b;
        }

        public static object ConvertObjectFromByte(Byte[] byt)
        {
            return ConvertObjectFromByte(byt, null);
        }

        public static object ConvertObjectFromByte(Byte[] b, object o)
        {
            object obj = o;
            if (b != null)
            {
                try
                {
                    //obj = System.Text.Encoding.Default.GetString(b);
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream rems = new MemoryStream(b);

                    obj = formatter.Deserialize(rems);
                }
                catch (Exception)
                {
                }
            }
            return obj;
        }

        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable GetDataTableFromIList(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    //if (pi.Name == "Owner")
                    //{
                    //    result.Columns.Add(pi.Name, typeof(Guid));
                    //}
                    //else if (pi.Name == "UpdateTime")
                    //{
                    //    result.Columns.Add(pi.Name, typeof(DateTime));
                    //}
                    //else
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
    }
}
