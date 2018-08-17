using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ERP.Domain.Dapper
{
    public abstract class SqlResourceBase
    {
        public abstract string FileName { get; }

        internal XElement document;
        public virtual XElement Root
        {
            get
            {
                if (document == null)
                {
                    string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SqlResources\" + FileName + ".xml");
                    document = XElement.Load(path);
                }
                return document;
            }
        }
        private static Regex selectRegex = new Regex("select", RegexOptions.IgnoreCase);
        private static Regex withNolockRegex = new Regex(@"with\(nolock\)", RegexOptions.IgnoreCase);
        private static ConcurrentDictionary<string, bool> lockCheckCache = new ConcurrentDictionary<string, bool>();
        public virtual string GetSql(string elementName)
        {
            string cacheKey = FileName + "_" + elementName;
            if (lockCheckCache.ContainsKey(cacheKey))
            {
                bool value;
                if (lockCheckCache.TryGetValue(cacheKey, out value))
                {
                    if (!value)
                        throw new Exception("select 语句必须增加 with(nolock)!");
                }
                return Root.Element(elementName).Value;
            }

            string sql = Root.Element(elementName).Value;
            if (selectRegex.IsMatch(sql) && !withNolockRegex.IsMatch(sql))
            {
                lockCheckCache.TryAdd(cacheKey, false);
                throw new Exception("select 语句必须增加 with(nolock)!");
            }
            lockCheckCache.TryAdd(cacheKey, true);
            return sql;
        }
    }
}