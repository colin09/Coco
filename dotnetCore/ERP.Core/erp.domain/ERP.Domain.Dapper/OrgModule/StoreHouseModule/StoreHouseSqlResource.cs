using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Dapper.OrgModule.StoreHouseModule
{
    public class StoreHouseSqlResource : SqlResourceBase
    {
        public override string FileName
        {
            get { return "StoreHouseSqlResource"; }
        }
        public string Select { get { return GetSql("Select"); } }
        public string SelectIncludeCity { get { return GetSql("SelectIncludeCity"); } }
        public string Insert { get { return GetSql("Insert"); } }
        public string Update { get { return GetSql("Update"); } }
        public string Exists { get { return GetSql("Exists"); } }

        

        public string GetHQStoreHouse { get { return GetSql("GetHQStoreHouse"); } }
        public string GetHQAgentStoreHouse { get { return GetSql("GetHQAgentStoreHouse"); } }


        public string GetStoreHouseAddress { get { return GetSql("GetStoreHouseAddress"); } }
    }
}
