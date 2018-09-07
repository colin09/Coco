using System;
using System.Collections.Generic;


namespace ERP.Domain.Dapper.AmoebaModule.AmoebaConfigModule
{
    public class AmoebaConfigSqlResource : SqlResourceBase
    {
        public override string FileName { get { return "AmoebaConfigSqlResource"; } }
        public string Select { get { return GetSql("Select"); } }
        public string Insert { get { return GetSql("Insert"); } }
        public string Update { get { return GetSql("Update"); } }
        public string Delete { get { return GetSql("Delete"); } }
    }
}
