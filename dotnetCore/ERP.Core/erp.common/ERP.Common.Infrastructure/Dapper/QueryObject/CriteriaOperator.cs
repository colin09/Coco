using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Dapper.QueryObject
{
    public enum CriteriaOperator
    {
        None = 0,
        Equal = 1,
        GreaterThan = 2,
        GreaterThanOrEqual = 3,
        LessThen = 4,
        LessThenOrEqual = 5,
        Like = 6,
        In = 7,
        NotIn = 8,
        NotEqual = 9,
        IsNull = 10,
        IsNotNull = 11,
        NotLike = 12,
    }

    public static class CriteriaOperatorExtensions
    {
        public static string ToSqlOperator(this CriteriaOperator co)
        {
            switch (co)
            {
                case CriteriaOperator.Equal:
                    return "=";
                case CriteriaOperator.GreaterThan:
                    return ">";
                case CriteriaOperator.GreaterThanOrEqual:
                    return ">=";
                case CriteriaOperator.LessThen:
                    return "<";
                case CriteriaOperator.LessThenOrEqual:
                    return "<=";
                case CriteriaOperator.Like:
                    return "like";
                case CriteriaOperator.NotLike:
                    return "not like";
                case CriteriaOperator.In:
                    return "in";
                case CriteriaOperator.NotIn:
                    return "not in";
                case CriteriaOperator.NotEqual:
                    return "<>";
                case CriteriaOperator.IsNull:
                    return "IS NULL";
                case CriteriaOperator.IsNotNull:
                    return "IS NOT NULL";
                default:
                    return null;
            }
        }
    }
}
