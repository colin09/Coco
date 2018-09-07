using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.QueryObject
{
    public static class SqlParameterExtensions
    {
        public static object ToDynamicObject(this List<System.Data.SqlClient.SqlParameter> parameters)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            var dic = obj as IDictionary<string, object>;
            foreach (var item in parameters)
            {
                dic.Add(item.ParameterName, item.Value);
            }
            return obj as object;
        }
    }
}
