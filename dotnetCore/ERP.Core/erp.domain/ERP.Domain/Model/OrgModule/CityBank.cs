using ERP.Domain.Enums.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    public class CityBank : BaseEntity, IAggregationRoot
    {
        public CityBank()
        {
            this.State = EnableState.启用;
        }

        #region 基本属性

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [MaxLength(64)]
        [Required]
        public string BankNo { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public EnableState State { get; set; }

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }

        #endregion

        #region 聚合属性

        public City City { get; set; }

        #endregion
    }
}
