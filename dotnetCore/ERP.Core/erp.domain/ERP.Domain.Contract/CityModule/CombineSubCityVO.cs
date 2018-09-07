using System;
using System.Collections.Generic;


namespace ERP.Domain.Contract.CityModule
{
    public class CombineSubCityVO
    {
        public string Id { get; set; }

        public string MasterCityId { get; set; }
        public string MasterCityName { get; set; }
        public string SubCityId { get; set; }
        public string SubCityName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
