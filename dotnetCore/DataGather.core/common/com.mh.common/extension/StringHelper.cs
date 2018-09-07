using System;


namespace com.mh.common.extension
{
    public class StringHelper
    {
        public static string GetGUIDString()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32).ToLower();
        }
    }
}