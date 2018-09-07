using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ERP.Domain.Context;
using ERP.Domain.Model.AmoebaReportModule;

using ERP.Domain.Repository.AmoebaModule.AmoebaConfigModule;

namespace ERP.Domain.Dapper.AmoebaModule.AmoebaConfigModule
{
    public class AmoebaConfigRepository : RepositoryBase<AmoebaConfig>, IAmoebaConfigRepository, IAmoebaConfigQueryRepository
    {
        private AmoebaConfigSqlResource sqlResource = new AmoebaConfigSqlResource();

        public AmoebaConfigRepository(IERPContext conn) : base(conn)
        {
        }

        public override string SelectSql { get { return this.sqlResource.Select; } }

        public override string InsertSql { get { return this.sqlResource.Insert; } }

        public override string UpdateSql { get { return this.sqlResource.Update; } }

        public override string DeleteSql { get { return this.sqlResource.Delete; } }

    }
}
