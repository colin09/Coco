using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Dapper.AggrQueryObject
{
    public class AggrCriterion
    {
        private AggrCriterion(string propertyName, AggrCriteriaOperator criteriaOperator, string tableAliasName, Type valueType)
        {
            PropertyName = propertyName;
            Operator = criteriaOperator;
            TableAliasName = tableAliasName;
            ValueType = valueType;
        }

        /// <summary>
        /// 属性名字
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// 结果属性名称
        /// </summary>
        public string ResultPropertyName { get; internal set; }

        public AggrCriteriaOperator Operator { get; private set; }

        public Type ValueType { get; private set; }

        /// <summary>
        /// 表别名 默认为空
        /// </summary>
        public string TableAliasName { get; private set; }

        public static AggrCriterion Create<T>(string propertyName, AggrCriteriaOperator criteriaOperator, string tableAliasName = null)
        {
            return new AggrCriterion(propertyName, criteriaOperator, tableAliasName, typeof(T));
        }
    }


    public static class AggrCriterionExtensions
    {
        public static string ToSql(this AggrCriterion c)
        {
            string oper = c.Operator.ToSqlOperator();
            if (oper == null) return null;

            var aliasName = string.IsNullOrWhiteSpace(c.TableAliasName) ? "" : c.TableAliasName + ".";

            return string.Format("ISNULL({0}({1}{2}),0) {3}", oper, aliasName, c.PropertyName, c.ResultPropertyName);
        }
    }
}
