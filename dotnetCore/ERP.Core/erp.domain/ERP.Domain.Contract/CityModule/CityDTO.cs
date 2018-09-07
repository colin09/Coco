using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Model.OrgModule;
//using ERP.Domain.Services.CommonModule;

namespace ERP.Domain.Contract.CityModule
{
    public class CityDTO
    {

        public string Id { get; set; }
        /// <summary>
        /// 城市类型（自营：0，加盟：1）
        /// </summary>
        public CityMode CityMode { get; set; }

        ///<summary>
        ///名称
        ///</summary>
        public string Name { get; set; }

        public EnableState Enable { get; set; }

        public string NewId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
