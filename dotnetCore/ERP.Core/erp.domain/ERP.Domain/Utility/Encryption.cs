using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ERP.Domain.Utility
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    public static class Encryption
    {
        /// <summary>
        /// 32位MD5
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string MD5_32(string decryptString) //加密，不可逆
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string str = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(decryptString)));
            //BitConverter.ToString()得到的字符串形式为2个一对，对与对之间加个“-”符号，如，“7F-2C-4A”。 
            //md5.ComputeHash(UTF8Encoding.Default.GetBytes(t16.Text.Trim()))，计算哈希值。 
            //4表示初始位置，8表示有8个对，每个对都是2位，故有16位（32位为16对），即就是从第4对开始连续取8对。 
            str = str.Replace("-", "");
            return str;
        }

        /// <summary>
        /// 32位MD5
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string MD5_32(byte[] decryptBytes) //加密，不可逆
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string str = BitConverter.ToString(md5.ComputeHash(decryptBytes));
            //BitConverter.ToString()得到的字符串形式为2个一对，对与对之间加个“-”符号，如，“7F-2C-4A”。 
            //md5.ComputeHash(UTF8Encoding.Default.GetBytes(t16.Text.Trim()))，计算哈希值。 
            //4表示初始位置，8表示有8个对，每个对都是2位，故有16位（32位为16对），即就是从第4对开始连续取8对。 
            str = str.Replace("-", "");
            return str;
        }

    }
}
