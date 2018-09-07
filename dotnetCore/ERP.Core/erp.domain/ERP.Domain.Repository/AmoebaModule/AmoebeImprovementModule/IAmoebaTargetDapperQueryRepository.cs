
using System;
using System.Collections.Generic;

using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Model.AmoebaReportModule;

namespace ERP.Domain.Repository.AmoebaModule.AmoebaImprovementModule
{
    public interface IAmoebaImprovementQueryRepository : IAmoebaImprovementRepository
    {


        IEnumerable<AmoebaImprovement> Query(Query query);

    }
}
