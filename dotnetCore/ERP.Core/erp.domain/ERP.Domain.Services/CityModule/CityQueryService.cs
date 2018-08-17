using System;
using System.Collections.Generic;
using System.Linq;

using log4net;
using AutoMapper;

//using ERP.Common.Cache.Implements.CityModeModule;
using ERP.Common.Infrastructure.Models;

using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Contract.CityModule;
using ERP.Domain.Repository.OrgModule;
using ERP.Domain.Repository.OrgModule.CityModule;




namespace ERP.Domain.Services.CityModule
{
    public class CityQueryService : ICityQueryService
    {
        private ICityQueryRepository _repository;
        private ILog _log;

        public CityQueryService(ICityQueryRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        public CityVO GetOneById(string id)
        {
            var result = _repository.GetById(id);
            return Mapper.Map<CityVO>(result);
        }


        public bool ExistsCityName(string cityName)
        {
            var search = new CitySearchQuery { Name = cityName } ;
            var result = _repository.Count(search.CreateQuery());
            return result > 0;
        }
        public CityVO GetByCityName(string cityName)
        {
            var search = new CitySearchQuery { Name = cityName };
            var result = _repository.FirstOrDefault(search.CreateQuery());
            return Mapper.Map<CityVO>(result);
        }


        public CityVO[] Query(CitySearch search, Sort sort = null)
        {
            var cities = _repository.Query(((CitySearchQuery)search).CreateQuery(), CheckSort(sort));
            return Mapper.Map<CityVO[]>(cities);
        }
        public CityVO[] QueryPaged(CitySearch search, Pager pager, Sort sort = null)
        {
            var cities = _repository.QueryPaged(((CitySearchQuery)search).CreateQuery(), pager, CheckSort(sort));
            return Mapper.Map<CityVO[]>(cities);
        }
        public CityDTO Get(string id)
        {
            var city = _repository.FirstOrDefault(id);
            return Mapper.Map<CityDTO>(city);
        }

        public CityVO GetVO(string id)
        {
            var city = _repository.FirstOrDefault(id);
            return Mapper.Map<CityVO>(city);
        }

        private Sort CheckSort(Sort sort)
        {
            if (sort == null) sort = Sort.Create().Add(SortCondition.Create("CreateTime", false, "[Orgs]"));
            return sort;
        }
        public CityDTO GetHQCity()
        {
            return Mapper.Map<CityDTO>(_repository.GetHQCity());
        }

        public CityDTO GetCityByStoreHouse(string storeHouseId)
        {
            var sql = @"SELECT [City].Id,[City].NewId,[City].Name FROM dbo.StoreHouse WITH(NOLOCK)
                        INNER JOIN  dbo.Orgs StoreHouseOrg WITH(NOLOCK) ON StoreHouseOrg.Id = StoreHouse.Id
                        INNER JOIN dbo.Orgs City WITH(NOLOCK) ON City_Id=City.Id
                        WHERE StoreHouseOrg.Id=@Id OR StoreHouseOrg.NewId=@Id";
            return Mapper.Map<CityDTO>(_repository.Query(sql, new { Id = storeHouseId }).FirstOrDefault());
        }

        public CityDTO GetCityByProvider(string providerId, Enums.OrgModule.ProviderType? providerType)
        {
            var sql = @"SELECT  [City].Id,[City].NewId,[City].Name FROM dbo.Providers WITH(NOLOCK)
                        INNER JOIN dbo.Orgs City WITH(NOLOCK) ON City_Id=[City].Id
                        WHERE [Providers].Id=@Id AND
                        (@ProviderType IS NULL OR [Providers].ProviderType=@ProviderType)";
            return Mapper.Map<CityDTO>(_repository.Query(sql, new { Id = providerId, ProviderType = providerType }).FirstOrDefault());
        }

        public CombineSubCityVO[] GetCombineSubCities()
        {
            var sql = @" SELECT s.Id,s.MasterCity_Id MasterCityId,o.Name MasterCityName,s.SubCity_Id SubCityId,o1.Name SubCityName,s.CreateTime
                    FROM dbo.CombineCitySettings s WITH(NOLOCK) 
                   INNER JOIN erp.dbo.Orgs o  WITH(NOLOCK) ON s.MasterCity_Id = o.Id
                   INNER JOIN erp.dbo.orgs o1  WITH(NOLOCK) ON s.SubCity_Id = o1.Id";
            return _repository.Query<CombineSubCityVO>(sql).ToArray();
        }

        public CityMode GetCityModeByCityId(string cityId)
        {
            var cacheKey = CityMode.自营;
            // if (CityModeCacheManager.ContainsKey(cityId))
            // {
            //     var result = CityModeCacheManager.Get(cityId);
            //     if (result.HasValue) return result.Value;
            //     return cacheKey;
            // }

            var city = _repository.FirstOrDefault(cityId);

            // if (city != null) cacheKey = city.CityMode;
            // CityModeCacheManager.Add(cityId, cacheKey);

            return cacheKey;
        }


        public ERP.Domain.Contract.CityModule.Location[] GetAllCities()
        {
            var sql = @" SELECT Address_Province Province,Address_City City,Address_County County FROM dbo.City WITH(NOLOCK) JOIN dbo.Orgs  WITH(NOLOCK)  ON	Orgs.Id = City.Id";
            return _repository.Query<ERP.Domain.Contract.CityModule.Location>(sql).ToArray();
        }


        public List<Location> GetLocations()
        {
            var sql = @"SELECT  DISTINCT Orgs.Address_City City,Address_County County,Address_Province Province FROM dbo.OrgUserAuths 
                        JOIN dbo.Orgs ON Orgs.Id = OrgUserAuths.OrgId
                        WHERE  AuthType=21 AND Enable =1";
            return _repository.Query<Location>(sql).ToList();
        }


        public List<Location> GetAllCities(OrgUserType orgUserType)
        {
            var sql = @"SELECT  DISTINCT Orgs.Address_City City,Address_County County,Address_Province Province FROM dbo.OrgUserAuths 
                        JOIN dbo.Orgs ON Orgs.Id = OrgUserAuths.OrgId
                        WHERE  AuthType=@AuthType AND Enable =1";
            return _repository.Query<Location>(sql, new { AuthType = orgUserType }).ToList();
        }

         
        public CityVO[] QuerySupplierCityPaged(CitySearch search, Pager pager, Sort sort = null)
        {
            throw new NotImplementedException();
            /*  
            var repository = IocManager.Resolve<IOrgQueryRepository>();
            return Mapper.Map<CityVO[]>(repository.QueryPaged(((CitySearchQuery)search).CreateQuery(), pager,
                Sort.Create().Add(SortCondition.Create("Address_Province", false))
                .Add(SortCondition.Create("Address_City", false))
                ));*/
        }
    }
}
