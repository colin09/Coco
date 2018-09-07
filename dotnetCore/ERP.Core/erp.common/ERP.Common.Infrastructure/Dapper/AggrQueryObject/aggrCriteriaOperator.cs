using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Dapper.AggrQueryObject
{
    public enum AggrCriteriaOperator
    {
        None = 0,
        Sum = 1,
        Count = 2,
        Max = 3,
        Min = 4,
        Avg = 5,
    }

    public static class AggrCriteriaOperatorExtensions
    {
        public static string ToSqlOperator(this AggrCriteriaOperator co)
        {
            switch (co)
            {
                case AggrCriteriaOperator.Sum:
                    return "SUM";
                case AggrCriteriaOperator.Count:
                    return "COUNT";
                case AggrCriteriaOperator.Max:
                    return "MAX";
                case AggrCriteriaOperator.Min:
                    return "MIN";
                case AggrCriteriaOperator.Avg:
                    return "AVG";
                default:
                    return null;
            }
        }
    }
}
