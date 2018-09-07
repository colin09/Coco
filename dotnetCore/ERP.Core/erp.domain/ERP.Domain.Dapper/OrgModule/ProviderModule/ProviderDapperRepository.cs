using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;

using ERP.Common.Infrastructure.Data;
using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Utility;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Context;
using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Model.OrgModule;

using ERP.Domain.Repository.OrgModule.ProviderModule;

namespace ERP.Domain.Dapper.OrgModule.ProviderModule
{
    public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository, IProviderQueryRepository
    {

        private ProviderSqlResource resource = new ProviderSqlResource();

        public ProviderRepository(IERPContext conn)
            : base(conn)
        {
        }

        public ProviderSqlResource SqlResource
        {
            get { return resource; }
        }

        private Func<Provider, OrgHead, OrgAddress, Provider> providerMap = (provider, head, address) =>
        {
            provider.Head = head;
            provider.Address = address;
            return provider;
        };
        public override Provider FirstOrDefault(Query query, Sort sort = null)
        {
            object param;
            var whereSql = GetWhereSql(query, out param);
            return Conn.Query(SqlUtility.CreateTop1SelectSql(SqlResource.Select, whereSql, sort == null ? null : sort.Translate())
                , providerMap, param, Tran, splitOn: "MobileNO,Province", commandTimeout: Default_CommandTimeout).FirstOrDefault();
        }

        public override IEnumerable<Provider> QueryPaged(Query query, Pager pager, Sort sort)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);
            param = AddPagedParams(param, pager);
            if (param == null) param = new { PageIndex = pager.PageIndex, PageSize = pager.PageSize };
            if (pager.IsGetTotalCount)
            {
                pager.TotalCount = this.Count(CombineCountSql(this.SelectSql, whereSql), param);
            }
            return Conn.Query(CombinePagedSql(SelectSql, whereSql, sort.Translate()), providerMap, param, Tran,
                splitOn: "MobileNO,Province", commandTimeout: Default_CommandTimeout);
        }

        public override IEnumerable<Provider> Query(Query query, Sort sort)
        {
            object param;
            string sql = GetQuerySql(query, sort, out param);
            return Conn.Query(sql, providerMap, param, Tran, splitOn: "MobileNO,Province", commandTimeout: Default_CommandTimeout);
        }

        public override void Insert(Provider entity)
        {
            Execute(resource.Insert, CreateProviderParamter(entity));
        }

        public override void Update(Provider entity)
        {
            Execute(resource.Update, CreateProviderParamter(entity));
        }

        private object CreateProviderParamter(Provider entity)
        {
            return new
            {
                Id = entity.Id,
                Name = entity.Name,
                Remark = entity.Remark,
                Enable = entity.Enable,
                Head_Name = entity.Head.Name,
                Head_Gender = entity.Head.Gender,
                Head_CardNO = entity.Head.CardNO,
                Head_MobileNO = entity.Head.MobileNO,
                Head_PhoneNO = entity.Head.PhoneNO,
                Head_Email = entity.Head.Email,
                Address_Province = entity.Address.Province,
                Address_City = entity.Address.City,
                Address_County = entity.Address.County,
                Address_DetailAddress = entity.Address.DetailAddress,
                City_Id = entity.City_Id,
                AuditState = entity.AuditState,
                AuditRemark = entity.AuditRemark,
                DepositBank = entity.DepositBank,
                BankName = entity.BankName,
                AccountNumber = entity.AccountNumber,
                BusinessLicense = entity.BusinessLicense,
                ProviderType = entity.ProviderType,
                ProviderProperty = entity.ProviderProperty,
                RequiredAgreement = entity.RequiredAgreement,
                DonotTakeupMoney = entity.DonotTakeupMoney,
                HasCreateYJXAccount = entity.HasCreateYJXAccount,
                AgreementBeginTime = entity.AgreementBeginTime,
                AgreementEndTime = entity.AgreementEndTime
            };
        }

        public override string InsertSql
        {
            get { return this.SqlResource.Insert; }
        }

        public override string UpdateSql
        {
            get { return this.SqlResource.Update; }
        }

        public override string DeleteSql
        {
            get { return this.SqlResource.Delete; }
        }

        public override string SelectSql
        {
            get { return this.SqlResource.Select; }
        }

        public IEnumerable<Provider> GetByIds(string[] ids)
        {
            var query = Common.Infrastructure.Dapper.QueryObject.Query.Create().Add(Criterion.Create("Id", ids, CriteriaOperator.In));
            object param;
            var sql = CombineSql(this.SelectSql, GetWhereSql(query, out param));
            return Conn.Query(sql, providerMap, param, Tran, splitOn: "MobileNO,Province", commandTimeout: Default_CommandTimeout);
        }
    }
}
