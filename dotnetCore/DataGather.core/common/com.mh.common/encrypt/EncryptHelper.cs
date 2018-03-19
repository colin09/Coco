using System;
using System.Text;
using System.Security.Cryptography;

namespace com.mh.common.encrypt{

    public class EncryptHelper{
        
        public static string MD5Encrypt(string strText)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(strText));

            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}