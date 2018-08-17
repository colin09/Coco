using System.Collections.Generic;
using System.Linq;
using Dapper;
using ERP.Domain.Context;

using ERP.Domain.Model.AmoebaReportModule;
using ERP.Common.Infrastructure.Dapper.QueryObject;

using ERP.Domain.Repository.AmoebaModule.AmoebaTargetModule;

namespace ERP.Domain.Dapper.AmoebaModule.AmoebaTargetModule
{
   public class AmoebaTargetRepository : RepositoryBase<AmoebaTarget>, IAmoebaTargetRepository, IAmoebaTargetQueryRepository
    {
        private AmoebaTargetSqlResource sqlResource = new AmoebaTargetSqlResource();
        public AmoebaTargetRepository(IERPContext conn)
              : base(conn)
        {
        }
        public override string SelectSql { get { return this.sqlResource.Select; } }
        public override string InsertSql { get { return this.sqlResource.Insert; } }
        public override string UpdateSql { get { return this.sqlResource.Update; } }
        public override string DeleteSql { get { return this.sqlResource.Delete; } }






        public IEnumerable<AmoebaTarget> Query(Query query)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);

            return Conn.Query<AmoebaTarget>(CombineSql(SelectSql, whereSql), param, Tran, commandTimeout: Default_CommandTimeout);
        }
    }
}
