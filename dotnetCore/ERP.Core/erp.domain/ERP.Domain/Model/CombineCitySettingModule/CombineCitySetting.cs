using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.CombineCitySettingModule
{
    /// <summary>
    /// 合并城市
    /// </summary>
    public class CombineCitySetting : BaseEntity, IAggregationRoot
    {
        public CombineCitySetting() {
            SubCity = new City();
            MasterCity = new City();
        }

        [ForeignKey("SubCity")]
        [MaxLength(64)]
        public string SubCity_Id { get; set; }

        /// <summary>
        /// 子城市
        /// </summary>
        public City SubCity { get; set; }

        [ForeignKey("MasterCity")]
        [MaxLength(64)]
        public string MasterCity_Id { get; set; }

        /// <summary>
        /// 主城市
        /// </summary>
        public City MasterCity { get; set; }
    }
}
