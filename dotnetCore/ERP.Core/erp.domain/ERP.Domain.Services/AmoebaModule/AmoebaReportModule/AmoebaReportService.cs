using log4net;
using System;
using AutoMapper;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

using ERP.Common.Infrastructure.Data;
using ERP.Common.Infrastructure.Models;
using ERP.Domain.Enums.AmoebaModule;
using ERP.Domain.Model.AmoebaReportModule;

using ERP.Domain.Repository.AmoebaModule.AmoebaReportModule;
using ERP.Domain.Repository.AmoebaModule.AmoebaTargetModule;
using ERP.Domain.Repository.AmoebaModule.AmoebaImprovementModule;
using ERP.Domain.Repository.OrgModule.CityModule;
using ERP.Domain.IService.AmoebaModule.AmoebaReportModule;

using ERP.Domain.Contract.CityModule;
using ERP.Domain.Contract.AmoebaModule.AmoebaReportModule;
using ERP.Domain.Contract.AmoebaModule.AmoebaTargetModule;
using ERP.Domain.Contract.AmoebaModule.AmoebaImprovementModule;

using ERP.Domain.Services.CityModule;
using ERP.Domain.Services.AmoebaModule.AmoebaTargetModule;
using ERP.Domain.Services.AmoebaModule.AmoebaImprovementModule;

namespace ERP.Domain.Services.AmoebaModule.AmoebaReportModule
{
    public class AmoebaReportService : IAmoebaReportService
    {
        private ILog _log;
        private IUnitOfWork _uow;
        private IAmoebaReportRepository _repository;


        public AmoebaReportService(IUnitOfWork uow, ILog log)
        {
            _log = log;
            _uow = uow;
            _repository = _uow.GetRepository<IAmoebaReportRepository>();
        }







        public string ImportData(DataTable table, string dataMonth)
        {
            var list = TableToList(table);

            var updateList = CheckReportData(list, dataMonth);

            var errorMsg = Save(list, dataMonth, updateList);

            return errorMsg;

        }


        private List<AmoebaReportDTO> TableToList(DataTable table)
        {
            var list = new List<AmoebaReportDTO>();

            var rowCount = table.Rows.Count;
            var columnCount = table.Columns.Count;
            //DataTable --> string[,]
            var dataArray = new string[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                var row = table.Rows[i];
                for (int j = 0; j < columnCount; j++)
                {
                    dataArray[i, j] = row[j].ToString();
                }
            }
            //string[,] --> JSON
            var listJson = "[";
            for (int j = 0; j < columnCount; j++)
            {
                var value = dataArray[0, j];
                if (string.IsNullOrWhiteSpace(value))
                    continue;

                var mJson = "{";
                decimal val = 0;
                for (int i = 0; i < AmoebaReportConst.ColumnMapping.Length; i++)
                {
                    var cellValue = dataArray[i, j];
                    if (i > 0)
                    {
                        if (string.IsNullOrWhiteSpace(cellValue))
                            mJson += string.Format(" '{0}' : null ,", AmoebaReportConst.ColumnMapping[i]);
                        else
                        {
                            val = 0;
                            decimal.TryParse(cellValue, out val);
                            mJson += string.Format(" '{0}' : {1} ,", AmoebaReportConst.ColumnMapping[i], val);
                        }
                    }
                    else
                        mJson += string.Format(" '{0}' : '{1}' ,", AmoebaReportConst.ColumnMapping[i], cellValue);
                }
                mJson = mJson.Substring(0, mJson.Length - 1);
                mJson += "},";

                listJson += mJson;
            }
            listJson = listJson.Substring(0, listJson.Length - 1);
            listJson += "]";
            //JSON --> List
            list = JsonConvert.DeserializeObject<List<AmoebaReportDTO>>(listJson);

            return list;
        }

        private List<AmoebaReport> CheckReportData(List<AmoebaReportDTO> list, string dataMonth)
        {
            var updateList = new List<AmoebaReport>();
            var names = list.Select(m => m.CityName).Distinct().ToArray();

            var citySearch = new CitySearch { Names = names, OrgTypes = new List<Model.OrgModule.OrgType> { Model.OrgModule.OrgType.城市 } } as CitySearchQuery;
            var citysQuery = _uow.GetRepository<ICityRepository>().Query(citySearch.CreateQuery(), Sort.DefaultSort);
            if (citysQuery.Any())
            {
                //取出已导入过数据的city
                var cityIds = citysQuery.Select(c => c.Id).ToArray();
                var overCitySearch = new AmoebaReportSearch { DataMonth = dataMonth, Citys = cityIds };
                var overCityReportList = _uow.GetRepository<IAmoebaReportRepository>().Query(overCitySearch.CreateQuery(), Sort.DefaultSort).ToList();
                var overCityList = overCityReportList.Select(m => m.CityId).ToList();

                //去除city中name重复的数据
                //var citys = citysQuery.ToList();
                var errorCity = citysQuery.GroupBy(c => c.Name).Where(c => c.Count() > 1).Select(c => c.Key).ToList();

                var dic = citysQuery.Where(c => !errorCity.Contains(c.Name)).ToDictionary(k => k.Name, v => v.Id);
                foreach (var item in list)
                {
                    if (dic.ContainsKey(item.CityName))
                    {
                        item.CityId = dic[item.CityName];
                        if (overCityList != null && overCityList.Contains(item.CityId))
                        {
                            item.Error = "update";
                            var entity = UpdateReport(overCityReportList.FirstOrDefault(m => m.CityId == item.CityId), item);
                            updateList.Add(entity);
                        }
                    }
                    else if (errorCity.Contains(item.CityName))
                        item.Error = "无法识别出唯一的城市名称，请联系管理员。";
                    else
                        item.Error = "城市名称不存在。";
                }
            }
            return updateList;
        }


        private AmoebaReport UpdateReport(AmoebaReport entity, AmoebaReportDTO dto)
        {
            foreach (var p in dto.GetType().GetProperties())
            {
                var name = p.Name;
                if (!AmoebaReportConst.ColumnMapping.Contains(name))
                    continue;

                var value = p.GetValue(dto, null);
                var decimalValue = 0m;
                if (value != null && decimal.TryParse(value.ToString(), out decimalValue))
                {
                    if (decimalValue != 0)
                        entity.SetValue(name, decimalValue);
                }
            }
            return entity;
        }

        private string Save(List<AmoebaReportDTO> list, string month, List<AmoebaReport> updateList)
        {
            var msg = "";

            var errorList = list.Where(l => l.Error != null && l.Error.Length > 0 && l.Error != "update")
                .Select(m => string.Format("{0} : {1} ", m.CityName, m.Error)).ToList();
            var successList = list.Where(l => l.Error == null || l.Error.Length < 1).ToList();

            var models = Mapper.Map<List<AmoebaReport>>(successList);
            foreach (var m in models)
                m.DataMonth = month;

            using (_uow.Begin())
            {
                _repository.InsertBatch(models);

                if (updateList != null)
                    _repository.UpdateBatch(updateList);

                _uow.Commit();
            }
            if (errorList != null)
                return string.Join(";", errorList);
            return msg;
        }












        #region  write target

        public string ImproveTarget(AmoebaTargetImproveDTO dto)
        {
            var monthStr = dto.Month.ToString("yyyy-MM");

            using (_uow.Begin())
            {
                var targetInsert = false;
                //var target = context.AmoebaTargets.FirstOrDefault(m => m.CityId == dto.CityId && m.DataMonth == monthStr);
                var targetSearch = new AmoebaTargetSearch { CityId = dto.CityId, DataMonth = monthStr } as AmoebaTargetSearchQuery;
                var target = _uow.GetRepository<IAmoebaTargetRepository>().Query(targetSearch.CreateQuery()).FirstOrDefault();
                if (target == null)
                {
                    target = new AmoebaTarget
                    {
                        CityId = dto.CityId,
                        DataMonth = monthStr
                    };
                    targetInsert = true;
                }

                foreach (var item in dto.Items)
                {
                    var config = AmoebaReportConst.AmoebaReportDataList.FirstOrDefault(m => m.HasTarget && m.TargetType == AmoebaTargetType.Edit && m.Id == item.Id);
                    if (config == null)
                    { /*log.Warn(string.Format("配置数据项外的目标：id = {0},target ={1}", item.Id, item.Target));*/ }
                    else
                    {
                        target.SetValue(config.Alias, item.Target);
                    }
                }
                CalculateRelationTarget(target);

                if (targetInsert)
                    //context.AmoebaTargets.Add(target);
                    _uow.GetRepository<IAmoebaTargetRepository>().Insert(target);
                else
                    //context.Entry(target).State = System.Data.Entity.EntityState.Modified;
                    _uow.GetRepository<IAmoebaTargetRepository>().Update(target);

                WriteImprovement(dto, _uow);

                _uow.Commit();
                return "OK";
            }
        }


        private void WriteImprovement(AmoebaTargetImproveDTO dto, IUnitOfWork _uow)
        {
            var monthStr = dto.Month.ToString("yyyy-MM");
            var improveSearch = new AmoebaImprovementSearch { CityId = dto.CityId, DataMonth = monthStr } as AmoebaImprovementSearchQuery;
            var list = _uow.GetRepository<IAmoebaImprovementRepository>().Query(improveSearch.CreateQuery());
            //var list = context.AmoebaImprovements.Where(m => m.CityId == dto.CityId && m.DataMonth == monthStr).ToList();

            var addList = new List<AmoebaImprovement>();
            var updateList = new List<AmoebaImprovement>();
            foreach (var item in dto.Items)
            {
                AmoebaImprovement model = null;
                var alias = "";

                var config = AmoebaReportConst.AmoebaReportDataList.FirstOrDefault(m => m.HasTarget && m.TargetType == AmoebaTargetType.Edit && m.Id == item.Id);
                if (config != null)
                {
                    alias = config.Alias;
                    model = list.FirstOrDefault(m => m.DataAlias == config.Alias);
                }
                else continue;

                if (model == null)
                    addList.Add(new AmoebaImprovement
                    {
                        CityId = dto.CityId,
                        DataMonth = monthStr,
                        DataAlias = alias,
                        Improvement = item.Improvement,
                    });
                else
                {
                    model.Improvement = item.Improvement;
                    updateList.Add(model);
                    //improveRepository.Update(model);
                    //context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
            }
            var improveRepository = _uow.GetRepository<IAmoebaImprovementRepository>();
            if (addList.Count > 0) improveRepository.InsertBatch(addList);
            if (updateList.Count > 0) improveRepository.UpdateBatch(updateList);
        }

        private void CalculateRelationTarget(AmoebaTarget target)
        {
            target.SaleCount_Sum_Other = target.SaleCount_Yinliao + target.SaleCount_liangyou + target.SaleCount_Rihua + target.SaleCount_Qita;
            target.SaleCount_Sum_All = target.SaleCount_Sum_Other + target.SaleCount_Jiu;

            target.GrossMargin_Unit_Avg_Other = target.SaleCount_Sum_Other == 0 ? 0 :
                                    (target.GrossMargin_Unit_Yinliao * target.SaleCount_Yinliao
                                    + target.GrossMargin_Unit_liangyou * target.SaleCount_liangyou
                                    + target.GrossMargin_Unit_Rihua * target.SaleCount_Rihua
                                    + target.GrossMargin_Unit_Qita * target.SaleCount_Qita)
                                     / target.SaleCount_Sum_Other;
            target.GrossMargin_Unit_Avg_All = target.SaleCount_Sum_All == 0 ? 0 :
                                    (target.GrossMargin_Unit_Jiu * target.SaleCount_Jiu
                                    + target.GrossMargin_Unit_Yinliao * target.SaleCount_Yinliao
                                    + target.GrossMargin_Unit_liangyou * target.SaleCount_liangyou
                                    + target.GrossMargin_Unit_Rihua * target.SaleCount_Rihua
                                    + target.GrossMargin_Unit_Qita * target.SaleCount_Qita)
                                     / target.SaleCount_Sum_All;
        }


        #endregion





    }
}
