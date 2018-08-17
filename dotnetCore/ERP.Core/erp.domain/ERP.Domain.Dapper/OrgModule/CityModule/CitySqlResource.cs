using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dapper.OrgModule.CityModule
{
    public class CitySqlResource : SqlResourceBase
    {
        public override string FileName
        {
            get { return "CitySqlResource"; }
        }

        public string Select { get { return GetSql("Select"); } }
        public string Insert { get { return GetSql("Insert"); } }
        public string Update { get { return GetSql("Update"); } }

        public string GetHQCity { get { return GetSql("GetHQCity"); } }
        public string GetAgentCity { get { return GetSql("GetAgentCity"); } }

        public string SearchOnLine { get { return GetSql("SearchOnLine"); } }
        public string SelectById { get { return GetSql("SelectById"); } }
    }
}
