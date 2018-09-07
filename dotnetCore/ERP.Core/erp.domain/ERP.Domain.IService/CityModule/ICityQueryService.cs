using System;
using System.Collections.Generic;

using ERP.Common.Infrastructure.Models;
using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Contract.CityModule;

namespace ERP.Domain.Services.CityModule
{
    public interface ICityQueryService
    {
        CityVO[] Query(CitySearch search, Sort sort = null);
        CityVO[] QueryPaged(CitySearch search, Pager pager, Sort sort = null);
        CityVO[] QuerySupplierCityPaged(CitySearch search, Pager pager, Sort sort = null);
        CityDTO GetHQCity();



        CityVO GetOneById(string id);

        bool ExistsCityName(string cityName);
        CityVO GetByCityName(string cityName);


        CityDTO Get(string id);
        CityVO GetVO(string id);
        CityDTO GetCityByStoreHouse(string storeHouseId);

        CityDTO GetCityByProvider(string providerId, ProviderType? providerType);

        CombineSubCityVO[] GetCombineSubCities();

        CityMode GetCityModeByCityId(string cityId);

        ERP.Domain.Contract.CityModule.Location[] GetAllCities();

        List<Location> GetLocations();

        List<Location> GetAllCities(OrgUserType orgUserType);
    }
}
