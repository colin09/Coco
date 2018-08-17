using System;
using System.Collections.Generic;

using ERP.Domain.Enums.OrgModule;

namespace ERP.Domain.Contract.CityModule
{
    public class CityVO
    {
        public string Id { get; set; }
        public string NewId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Name { get; set; }
        public EnableState Enable { get; set; }
        public CityMode CityMode { get; set; }
    }
}
