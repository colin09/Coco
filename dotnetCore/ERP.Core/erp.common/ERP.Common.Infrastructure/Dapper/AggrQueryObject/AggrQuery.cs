using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Dapper.AggrQueryObject
{
    public class AggrQuery
    {
        private List<AggrCriterion> m_Criterions = new List<AggrCriterion>();
        private dynamic result;
        public IEnumerable<AggrCriterion> Criterions { get { return m_Criterions; } }

        public AggrQuery Add(AggrCriterion criterion)
        {
            m_Criterions.Add(criterion);
            return this;
        }

        public void SetResult(dynamic obj)
        {
            this.result = obj;
        }

        public static AggrQuery Create()
        {
            return new AggrQuery();
        }

        public T GetValue<T>(int index)
        {
            if (index > this.m_Criterions.Count - 1)
                throw new Exception("非法的index！");
            var criterion = this.m_Criterions[index];
            return GetValueFromDynamicObject<T>(criterion);
        }

        private T GetValueFromDynamicObject<T>(AggrCriterion criterion)
        {
            if (typeof(T) != criterion.ValueType)
                throw new Exception("获取类型与定义类型不同！");
            var dic = result as IDictionary<string, object>;
            return (T)Convert.ChangeType(dic[criterion.ResultPropertyName], criterion.ValueType);
        }
    }

    public static class AggrQueryExtensions
    {
        public static string Translate(this AggrQuery query)
        {
            if (query.Criterions.Any())
            {
                int i = 0;
                foreach (var criterion in query.Criterions)
                {
                    criterion.ResultPropertyName = "Result" + i.ToString();
                    i++;
                }
                return " " + string.Join(",", query.Criterions.Select(c => c.ToSql()).ToArray()) + " ";
            }
            throw new Exception("未指定聚合函数！");
        }
    }
}
