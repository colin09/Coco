using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Dapper.QueryObject
{
    public class Criterion
    {
        /// <summary>
        /// 属性名字
        /// </summary>
        public string PropertyName { get; private set; }

        private string FixedKeywordPropertyName
        {
            get
            {
                return ERP.Common.Infrastructure.Utility.SqlUtility.HandleSqlServerKeyword(this.PropertyName);
            }
        }

        /// <summary>
        /// 参数属性名（正常 与PropertyName一致，当有多个同一属性的查询条件时，需重新定义）
        /// </summary>
        public string PropertyParameterName { get; private set; }

        public CriteriaOperator Operator { get; private set; }

        public object Value { get; private set; }

        /// <summary>
        /// 表别名 默认为空
        /// </summary>
        public string TableAliasName { get; private set; }

        private Criterion(string propertyName, object value, CriteriaOperator criteriaOperator, string tableAliasName)
        {
            PropertyName = propertyName;
            Value = value;
            Operator = criteriaOperator;
            TableAliasName = tableAliasName;
            PropertyParameterName = propertyName;
        }

        internal void RenamePropertyName(string renamePropertyName)
        {
            PropertyParameterName = renamePropertyName;
        }

        public static Criterion Create(string propertyName, object value, CriteriaOperator criteriaOperator, string tableAliasName = null, bool isTableValue = false)
        {
            if (isTableValue)
                return new TableValueCriterion(propertyName, value, criteriaOperator, tableAliasName);

            return new Criterion(propertyName, value, criteriaOperator, tableAliasName);
        }

        internal virtual string ToSql()
        {
            string oper = this.Operator.ToSqlOperator();
            if (oper == null) return null;

            var aliasName = string.IsNullOrWhiteSpace(this.TableAliasName) ? "" : this.TableAliasName + ".";

            if (this.Operator == CriteriaOperator.Like || this.Operator == CriteriaOperator.NotLike)
            {
                return string.Format("{0}{1} {2} '%' + @{3} + '%'", aliasName, this.FixedKeywordPropertyName, oper, this.PropertyParameterName);
            }

            if (this.Operator == CriteriaOperator.In || this.Operator == CriteriaOperator.NotIn)
            {
                return string.Format("{0}{1} {2} @{3}", aliasName, this.FixedKeywordPropertyName, oper, this.PropertyParameterName);
            }
            if (this.Operator == CriteriaOperator.IsNotNull || this.Operator == CriteriaOperator.IsNull)
            {
                return string.Format("{0}{1} {2}", aliasName, this.FixedKeywordPropertyName, oper);
            }

            return string.Format("{0}{1}{2}@{3}", aliasName, this.FixedKeywordPropertyName, oper, this.PropertyParameterName);
        }

        internal class TableValueCriterion : Criterion
        {
            internal TableValueCriterion(string propertyName, object value, CriteriaOperator criteriaOperator, string tableAliasName)
                : base(propertyName, value, criteriaOperator, tableAliasName)
            {
            }

            internal override string ToSql()
            {
                string oper = this.Operator.ToSqlOperator();
                if (oper == null) return null;

                var aliasName = string.IsNullOrWhiteSpace(this.TableAliasName) ? "" : this.TableAliasName + ".";

                if (this.Operator == CriteriaOperator.Like || this.Operator == CriteriaOperator.NotLike)
                {
                    throw new NotSupportedException("表条件不支持 Like和NotLike操作符！");
                }

                if (this.Operator == CriteriaOperator.In || this.Operator == CriteriaOperator.NotIn)
                {
                    return string.Format("{0}{1} {2} {3}", aliasName, this.FixedKeywordPropertyName, oper, this.Value);
                }
                if (this.Operator == CriteriaOperator.IsNotNull || this.Operator == CriteriaOperator.IsNull)
                {
                    throw new NotSupportedException("表条件不支持 IsNotNull和IsNull操作符！");
                }

                return string.Format("{0}{1}{2}{3}", aliasName, this.FixedKeywordPropertyName, oper, this.Value);
            }
        }
    }
}
