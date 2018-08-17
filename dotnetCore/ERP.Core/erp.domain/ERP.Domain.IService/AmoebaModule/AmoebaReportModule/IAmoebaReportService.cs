using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Domain.Contract.AmoebaModule.AmoebaReportModule;

namespace ERP.Domain.IService.AmoebaModule.AmoebaReportModule
{
    public interface IAmoebaReportService
    {


        string ImportData(DataTable table, string dataMonth);

        string ImproveTarget(AmoebaTargetImproveDTO dto);
    }
}
