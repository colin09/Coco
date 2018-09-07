using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.QueryObject
{
    public class TranslateResult
    {
        public TranslateResult()
        {
            this.SqlParameters = new List<SqlParameter>();
        }
        public string Sql { get; set; }
        public List<SqlParameter> SqlParameters { get; set; }

        public object Parameter { get { return this.SqlParameters.ToDynamicObject(); } }
    }
}
