using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Utility
{
    public class SqlUtility
    {
        /// <summary>
        /// 生成selectSql 对应的 select count(1) 语句
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public static string CreateCountSqlBySelectSql(string selectSql)
        {
            return ReplaceFieldSql(RemoveSortBySql(selectSql), "Count(1)");
        }

        private static readonly Regex whereRegex = new Regex(@"\s*where\s*", RegexOptions.IgnoreCase);
        private static readonly Regex orderByRegex = new Regex(@"(.*)?order\s*by.*", RegexOptions.IgnoreCase);

        public static string CreateTop1SelectSql(string selectSql, string whereSql = "", string sortSql = "")
        {
            var fieldListSql = GetFieldListSql(selectSql);
            var sql = ReplaceFieldSql(selectSql, " TOP 1 " + fieldListSql);
            if (string.IsNullOrWhiteSpace(whereSql)) return sql + sortSql;

            return sql + (whereRegex.IsMatch(whereSql) ? whereSql : " where " + whereSql) + sortSql;
        }

        public static string CreatePagedSql(string selectSql, string whereSql = null, string sortSql = null)
        {
            return string.Format("{0} {1} {2} {3}", selectSql, whereSql, sortSql,
                "OFFSET (@PageIndex-1)*@PageSize ROWS FETCH NEXT @PageSize ROWS ONLY");
        }

        /// <summary>
        /// 替换 字段Sql 为 指定 语句
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="newSql"></param>
        /// <returns></returns>
        public static string ReplaceFieldSql(string selectSql, string newSql)
        {
            var fieldListSql = GetFieldListSql(selectSql);
            return selectSql.Replace(fieldListSql, " " + newSql + " ");
        }

        public static string RemoveSortBySql(string selectSql)
        {
            var sortBySql = GetSortBySql(selectSql);
            if (string.IsNullOrWhiteSpace(sortBySql))
                return selectSql;
            return selectSql.Replace(sortBySql, "");
        }

        /// <summary>
        /// 重命名 重复的列名（仅用作通用count语句）
        /// </summary>
        /// <param name="fieldListSql"></param>
        /// <returns></returns>
        public static string RenameRepeatColumnName(string sql)
        {
            var fieldListSql = GetFieldListSql(sql);
            string newSql = fieldListSql.Replace("[", "").Replace("]", "").Replace("'", "");
            var propertyStrings = newSql.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(r => r.Trim()).ToArray();

            Dictionary<string, int> countDic = new Dictionary<string, int>();
            string fieldString;
            string key;
            string[] array;
            string newFieldString;
            Regex reg = new Regex(@".*\."); //表名+.
            List<string> replacePropertyStrings = new List<string>();
            foreach (var propertyString in propertyStrings)
            {
                fieldString = reg.Replace(propertyString, "");
                array = fieldString.Split(new string[] { " ", "as", "As", "AS", "aS" }
                       , StringSplitOptions.RemoveEmptyEntries);
                int length = array.Length;
                key = length > 1 ? array[1] : array[0];

                if (countDic.ContainsKey(key))
                {
                    int count = countDic[key] + 1;
                    //新字段 原Id 变为 Id Id1 ，原 Id  Id  变为Id Id1
                    newFieldString = length > 1 ? fieldString + count.ToString() :
                        fieldString + " " + fieldString + count.ToString();
                    replacePropertyStrings.Add(propertyString.Replace(fieldString, newFieldString));
                    countDic[key] = count;
                }
                else
                {
                    replacePropertyStrings.Add(propertyString);
                    countDic.Add(key, 0);
                }
            }

            return sql.Replace(fieldListSql, " " + string.Join(",", replacePropertyStrings) + " ");
        }

        private static string GetFieldListSql(string sql)
        {
            Regex reg = new Regex(@"select([\s\S]*?)\s+from\s+", RegexOptions.IgnoreCase);
            foreach (Match item in reg.Matches(sql))
            {
                if (item.Groups.Count > 1) return item.Groups[1].Value;
            }
            throw new Exception("未匹配到select……from语句！");
        }

        private static string GetSortBySql(string sql)
        {
            foreach (Match item in orderByRegex.Matches(sql))
            {
                return item.Value;
            }
            return null;
        }
        private static string[] sqlKeywords = new string[] { "key", "user", "state", "enable" };
        private static Regex keywordRegex = new Regex("key|user|state|enable", RegexOptions.IgnoreCase);
        internal static bool IsSqlServerKeyword(string str)
        {
            return sqlKeywords.Any(k => k.Equals(str, StringComparison.CurrentCultureIgnoreCase));//keywordRegex.IsMatch(str);
        }

        internal static string HandleSqlServerKeyword(string str)
        {
            if (IsSqlServerKeyword(str)) return "[" + str + "]";
            return str;
        }
    }
}
