using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dapper.AmoebaModule.AmoebaTargetModule
{
    public class AmoebaTargetSqlResource : SqlResourceBase
    {
        public override string FileName
        {
            get { return "AmoebaTargetSqlResource"; }
        }

        public string Select { get { return GetSql("Select"); } }
        public string Insert { get { return GetSql("Insert"); } }
        public string Update { get { return GetSql("Update"); } }
        public string Delete { get { return GetSql("Delete"); } }
    }
}
