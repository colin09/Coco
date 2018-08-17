using System.Collections.Generic;
using System.Linq;
using Dapper;
using ERP.Domain.Context;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Model.AmoebaReportModule;

using ERP.Domain.Repository.AmoebaModule.AmoebaImprovementModule;

namespace ERP.Domain.Dapper.AmoebaModule.AmoebaImprovementModule
{
   public class AmoebaImprovementRepository : RepositoryBase<AmoebaImprovement>, IAmoebaImprovementRepository, IAmoebaImprovementQueryRepository
    {
        private AmoebaImprovementSqlResource sqlResource = new AmoebaImprovementSqlResource();
        public AmoebaImprovementRepository(IERPContext conn)
              : base(conn)
        {
        }
        public override string SelectSql { get { return this.sqlResource.Select; } }
        public override string InsertSql { get { return this.sqlResource.Insert; } }
        public override string UpdateSql { get { return this.sqlResource.Update; } }
        public override string DeleteSql { get { return this.sqlResource.Delete; } }









        public IEnumerable<AmoebaImprovement> Query(Query query)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);

            return Conn.Query<AmoebaImprovement>(CombineSql(SelectSql, whereSql), param, Tran, commandTimeout: Default_CommandTimeout);
        }
    }
}
