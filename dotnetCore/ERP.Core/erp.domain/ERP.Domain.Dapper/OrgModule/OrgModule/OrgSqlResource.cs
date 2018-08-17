using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dapper.OrgModule
{
    public class OrgSqlResource : SqlResourceBase
    {
        public override string FileName
        {
            get { return "OrgSqlResource"; }
        }
        public string Select { get { return GetSql("Select"); } }

        public string Update { get { return GetSql("Update"); } }

        public string Insert { get { return GetSql("Insert"); } }
    }
}
