using ERP.Domain.Context;
using ERP.Domain.Enums.OrgModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    /// <summary>
    /// 城市
    /// </summary>
    [Table("City")]
    public class City : Org, IAggregationRoot
    {
        public City()
            : base()
        {
            this.OrgType = OrgModule.OrgType.城市;
        }

        /// <summary>
        /// 城市类型（自营：0，加盟：1）
        /// </summary>
        public CityMode CityMode { get; set; }

        /// <summary>
        /// 大区Id
        /// </summary>
        [MaxLength(64)]
        public string Region_Id { get; set; }
    }
}
