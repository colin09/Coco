using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ERP.Common.Infrastructure.Exceptions;
using ERP.Common.Infrastructure.Models;
using ERP.Domain.Contract.AmoebaModule.AmoebaImprovementModule;
using ERP.Domain.Contract.AmoebaModule.AmoebaReportModule;
using ERP.Domain.Contract.AmoebaModule.AmoebaTargetModule;
using ERP.Domain.IService.AmoebaModule.AmoebaReportModule;
using ERP.Domain.Model.AmoebaReportModule;
using ERP.Domain.Repository.AmoebaModule.AmoebaImprovementModule;
using ERP.Domain.Repository.AmoebaModule.AmoebaReportModule;
using ERP.Domain.Repository.AmoebaModule.AmoebaTargetModule;
using ERP.Domain.Services.AmoebaModule.AmoebaImprovementModule;
using ERP.Domain.Services.AmoebaModule.AmoebaTargetModule;

namespace ERP.Domain.Services.AmoebaModule.AmoebaReportModule {

    public class AmoebaReportQueryService : IAmoebaReportQueryService {

        private readonly IMapper _mapper;
        private readonly IAmoebaReportQueryRepository _amoebaReportRepository;
        private readonly IAmoebaTargetQueryRepository _amoebaTargetRepository;
        private readonly IAmoebaImprovementQueryRepository _amoebaImproveRepository;

        public AmoebaReportQueryService (IMapper mapper, IAmoebaReportQueryRepository amoebaReportRepository, IAmoebaTargetQueryRepository amoebaTargetRepository, IAmoebaImprovementQueryRepository amoebaImproveRepository) {
            _mapper = mapper;
            _amoebaReportRepository = amoebaReportRepository;
            _amoebaTargetRepository = amoebaTargetRepository;
            _amoebaImproveRepository = amoebaImproveRepository;
        }

        public ReportDateVO GetImportDate (string cityId) {
            var date = _amoebaReportRepository.QueryImportDate (cityId);
            var lastMonth = _amoebaReportRepository.QueryLastMonth (cityId).FirstOrDefault ();

            var lastImport = (date == null || date.Count () < 1) ? "" : date.FirstOrDefault ().ToString ("yyyy-MM-dd HH:mm");
            return new ReportDateVO { LastCreateDate = lastImport, LastDataMonth = lastMonth == null ? "" : lastMonth };
        }

        public AmoebaReportRankingVO GetDataRanking (int id, string month, string cityId, bool sort) {
            var amoebaItem = AmoebaReportConst.AmoebaReportDataList.Where (m => m.Id == id).FirstOrDefault ();
            if (amoebaItem == null) throw new BusinessException ("数据编号错误！");

            var rankingData = _amoebaReportRepository.QueryDataRanking<AmoebaReportRankingItems> (amoebaItem.Alias, month).ToList ();

            if (rankingData != null && rankingData.Count > 0) {
                if (!sort)
                    rankingData.Reverse ();

                rankingData.ForEach (m => {
                    m.IsCurrent = m.CityId == cityId;
                    if (amoebaItem.ValuePercent && m.Value.HasValue)
                        m.Value = m.Value * 100;
                });
            }
            return new AmoebaReportRankingVO {
                Id = id,
                    Name = amoebaItem.Name,
                    FirstName = amoebaItem.FirstName,
                    SecondName = amoebaItem.SecondName,
                    ThirdName = amoebaItem.ThirdName,
                    Unit = amoebaItem.Unit,
                    ValuePercent = amoebaItem.ValuePercent,

                    Items = rankingData
            };
        }

        public AmoebaReportSimpleVO[] QuerySimpleInfo (AmoebaReportSearch search, Pager pager, Sort sort = null) {
            var result = _amoebaReportRepository.QuerySimpleInfo (search.CreateQuery (), pager, sort == null ? Sort.DefaultSort : sort);
            return _mapper.Map<AmoebaReportSimpleVO[]> (result);
        }

        public AmoebaReportVO[] QueryAll (AmoebaReportSearch search, Pager pager = null, Sort sort = null) {
            var result = _amoebaReportRepository.QueryAll (search.CreateQuery (), pager, sort == null ? Sort.DefaultSort : sort);
            return _mapper.Map<AmoebaReportVO[]> (result);
        }

        public List<AmoebaReportComparisonVO> GetDataComparison (string cityId, DateTime month, string compareCity, DateTime? compareMonth) {
            var compareType = 0;
            var compareProp = "";

            if (string.IsNullOrWhiteSpace (cityId))
                throw new BusinessException ("城市Id不存在！");

            if (!string.IsNullOrWhiteSpace (compareCity)) {
                compareType = 1;
                compareProp = compareCity;
            }

            var monthStr = month.ToString ("yyyy-MM");
            var monthNextStr = month.AddMonths (1).ToString ("yyyy-MM");
            if (compareMonth.HasValue && compareMonth < DateTime.Now) {
                compareType = 2;
                compareProp = compareMonth.Value.ToString ("yyyy-MM");
            } else if (compareMonth > DateTime.Now)
                throw new BusinessException ("对比月份错误！");

            /*///////////////////////////
            var search = new AmoebaReportSearch() { Citys = citys.ToArray(), Months = months.ToArray() };
            var queryList = _amoebaReportRepository.QueryAll(search.CreateQuery(), pager: null, sort: Sort.DefaultSort);
            ////////////////////////////

            var report = queryList.Where(m => m.CityId == cityId && m.DataMonth == monthStr).FirstOrDefault();

            Model.AmoebaReportModule.AmoebaReport compareReport = null;
            if (!string.IsNullOrWhiteSpace(compareCity))
                compareReport = queryList.Where(m => m.CityId == compareCity && m.DataMonth == monthStr).FirstOrDefault();
            else if (compareMonth.HasValue)
                compareReport = queryList.Where(m => m.CityId == cityId && m.DataMonth == compareMonthStr).FirstOrDefault();
            */

            var source = GetAmoebaRrportSource (cityId, monthStr, compareProp, compareType, monthNextStr);
            var report = source.Item1;
            var reportCompare = source.Item2;
            var target = source.Item3;
            var targetNext = source.Item4;
            var improvements = source.Item5;

            #region

            var responseItems = new List<AmoebaReportComparisonItems> ();
            foreach (var item in AmoebaReportConst.AmoebaReportDataList) {
                var value = report == null ? null : report.GetValue<decimal?> (item.Alias, 0m);
                var compareValue = reportCompare == null ? null : reportCompare.GetValue<decimal?> (item.Alias, 0m);
                decimal? increase = null;
                if (value.HasValue && compareValue.HasValue)
                    increase = value.Value - compareValue.Value;
                var improve = improvements == null ? null : improvements.FirstOrDefault (i => i.DataAlias == item.Alias);
                //var percent = compareValue == 0 ? 0 : increase / compareValue;

                var m = new AmoebaReportComparisonItems {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    Value = (item.ValuePercent && value.HasValue) ? value * 100 : value,
                    CompareValue = (item.ValuePercent && compareValue.HasValue) ? compareValue * 100 : compareValue,
                    //Increase = item.Percent ? (compareValue == 0 ? 0 : increase / compareValue) : increase,
                    Increase = (item.Percent && increase.HasValue) ?
                    (compareValue == 0 ?
                    0 :
                    Math.Round ((increase.Value / Math.Abs (compareValue.Value)) * 100, 2, MidpointRounding.AwayFromZero)) :
                    increase,
                    Percent = item.Percent,
                    EndNode = item.EndNode,
                    ValuePercent = item.ValuePercent,

                    Target = target == null ? 0 : target.GetValue<decimal?> (item.Alias, 0m),
                    TargetNext = targetNext == null ? 0 : targetNext.GetValue<decimal?> (item.Alias, 0m),
                    TargetType = item.TargetType,
                    Improvement = improve == null ? "" : improve.Improvement,
                };
                responseItems.Add (m);
            }

            var response = new List<AmoebaReportComparisonVO> ();
            foreach (var item in AmoebaReportConst.AmoebaRepostComparisonGroup) {
                var vo = new AmoebaReportComparisonVO {
                    Id = item.Id,
                    Title = item.Name,
                    Items = responseItems.Where (m => m.ParentId == item.Id).ToList ()
                };
                response.Add (vo);
            }

            #endregion

            return response;

        }

        public List<AmoebaReportComparisonVO> DataTargets (string cityId) {
            var month = DateTime.Now.AddMonths (-1);
            var monthStr = month.ToString ("yyyy-MM");
            var monthNextStr = month.AddMonths (1).ToString ("yyyy-MM");

            var source = GetAmoebaRrportSource (cityId, monthStr, "", 0, monthNextStr);
            var report = source.Item1;
            //var reportCompare = source.Item2;
            var target = source.Item3;
            var targetNext = source.Item4;
            var improvements = source.Item5;

            #region

            var responseItems = new List<AmoebaReportComparisonItems> ();
            foreach (var item in AmoebaReportConst.AmoebaReportDataList.Where (i => i.HasTarget)) {
                var value = report == null ? null : report.GetValue<decimal?> (item.Alias, 0m);
                var improve = improvements == null ? null : improvements.FirstOrDefault (i => i.DataAlias == item.Alias);

                var m = new AmoebaReportComparisonItems {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    Value = (item.ValuePercent && value.HasValue) ? value * 100 : value,
                    ValuePercent = item.ValuePercent,
                    EndNode = item.EndNode,

                    Target = target == null ? null : target.GetValue<decimal?> (item.Alias, 0m),
                    TargetNext = targetNext == null ? null : targetNext.GetValue<decimal?> (item.Alias, 0m),
                    TargetType = item.TargetType,
                    Improvement = improve == null ? "" : improve.Improvement,
                };
                responseItems.Add (m);
            }

            var response = new List<AmoebaReportComparisonVO> ();
            foreach (var item in AmoebaReportConst.AmoebaRepostTargetGroup) {
                var vo = new AmoebaReportComparisonVO {
                    Id = item.Id,
                    Title = item.Name,
                    Items = responseItems.Where (m => m.ParentId == item.Id).ToList ()
                };
                response.Add (vo);
            }

            #endregion

            return response;

        }

        /// <summary>
        /// 获取报表-对比报表-目标-改进 源数据
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="monthStr"></param>
        /// <param name="compareProp"></param>
        /// <param name="compareType">0-不对比, 1-同月对比, 2-同城对比,</param>
        /// <param name="monthNextStr"></param>
        /// <returns>报表，对比报表，本月目标，下月目标，下月改进</returns>
        private Tuple<AmoebaReport, AmoebaReport, AmoebaTarget, AmoebaTarget, List<AmoebaImprovement>> GetAmoebaRrportSource (string cityId, string monthStr, string compareProp, int compareType, string monthNextStr) {
            var citys = new List<string> { cityId };
            var months = new List<string> { monthStr };
            if (compareType == 1) //同月对比
                citys.Add (compareProp);
            else if (compareType == 2) //同城对比
                months.Add (compareProp);

            var reportSearch = new AmoebaReportSearch { Citys = citys.ToArray (), Months = months.ToArray () };
            var reportList = _amoebaReportRepository.Query (reportSearch.CreateQuery (), Sort.DefaultSort);

            var report = reportList.FirstOrDefault (m => m.CityId == cityId && m.DataMonth == monthStr);
            AmoebaReport reportCompare = null;
            if (compareType == 1)
                reportCompare = reportList.FirstOrDefault (m => m.CityId == compareProp && m.DataMonth == monthStr);
            else if (compareType == 2)
                reportCompare = reportList.FirstOrDefault (m => m.CityId == cityId && m.DataMonth == compareProp);

            //var targetMonths = new string[] { monthStr, monthNextStr };
            //var targetList = context.AmoebaTargets.Where(m => m.CityId == cityId && targetMonths.Contains(m.DataMonth)).ToList();
            var targetSearch = new AmoebaTargetSearch { CityId = cityId, Months = new string[] { monthStr, monthNextStr } } as AmoebaTargetSearchQuery;
            var targetList = _amoebaTargetRepository.Query (targetSearch.CreateQuery ());

            var target = targetList.FirstOrDefault (m => m.DataMonth == monthStr);
            var targetNext = targetList.FirstOrDefault (m => m.DataMonth == monthNextStr);

            //var improveList = context.AmoebaImprovements.Where(m => m.CityId == cityId && m.DataMonth == monthNextStr).ToList();
            var improveSearch = new AmoebaImprovementSearch { CityId = cityId, DataMonth = monthNextStr } as AmoebaImprovementSearchQuery;
            var improveList = _amoebaImproveRepository.Query (improveSearch.CreateQuery ()).ToList ();
            return new Tuple<AmoebaReport, AmoebaReport, AmoebaTarget, AmoebaTarget, List<AmoebaImprovement>> (report, reportCompare, target, targetNext, improveList);

        }
    }
}