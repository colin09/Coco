using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Models
{
    public class Sort
    {
        public static Sort DefaultSort = new Sort().Add(SortCondition.Create("CreateTime", false));
        private Sort()
        {
        }

        private List<SortCondition> m_sort = new List<SortCondition>();
        public IEnumerable<SortCondition> Sorts { get { return this.m_sort; } }

        public Sort Add(SortCondition sortCondition)
        {
            m_sort.Add(sortCondition);
            return this;
        }

        public static Sort Create()
        {
            return new Sort();
        }
    }

    public static class SortExtensions
    {
        public static string Translate(this Sort query)
        {
            string[] tempSQLs = query.Sorts.Select(s => s.ToSql()).ToArray();

            if (tempSQLs.Length == 0)
                return null;

            return string.Format(" order by {0}", string.Join(",", tempSQLs));
        }
    }
}
