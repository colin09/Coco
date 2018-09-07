using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dapper.OrgModule.ShopModule
{
    public class ShopSqlResource : SqlResourceBase
    {
        public override string FileName
        {
            get { return "ShopSqlResource"; }
        }
        public string Select { get { return GetSql("Select"); } }
        public string Insert { get { return GetSql("Insert"); } }
        public string Update { get { return GetSql("Update"); } }
        public string Delete { get { return GetSql("Delete"); } }
    }
}
