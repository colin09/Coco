using System;
using System.Collections.Generic;
using ERP.Common.Infrastructure.Models;
using ERP.Domain.Contract.AmoebaModule.AmoebaReportModule;

namespace ERP.Domain.IService.AmoebaModule.AmoebaReportModule
{
    public interface IAmoebaReportQueryService
    {

        ReportDateVO GetImportDate(string cityId);

        AmoebaReportRankingVO GetDataRanking(int id, string month, string cityId, bool sort);

        AmoebaReportSimpleVO[] QuerySimpleInfo(AmoebaReportSearch search, Pager pager, Sort sort = null);

        AmoebaReportVO[] QueryAll(AmoebaReportSearch search, Pager pager = null, Sort sort = null);

        List<AmoebaReportComparisonVO> DataTargets(string cityId);

        List<AmoebaReportComparisonVO> GetDataComparison(string cityId, DateTime month, string compareCity, DateTime? compareMonth);
    }
}
