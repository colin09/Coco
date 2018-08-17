using System;
using System.Collections.Generic;

using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Model.AmoebaReportModule;


namespace ERP.Domain.Repository.AmoebaModule.AmoebaTargetModule
{
    public interface IAmoebaTargetQueryRepository : IAmoebaTargetRepository
    {

        IEnumerable<AmoebaTarget> Query(Query query);
    }
}
