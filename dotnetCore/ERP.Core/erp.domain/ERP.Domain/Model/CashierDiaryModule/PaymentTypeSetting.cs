using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.CashierDiaryModule
{
    public class PaymentTypeSetting : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 支付方式名称
        /// </summary>
        [MaxLength(64)]
        public string PaymentName { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("CityBank")]
        public string CityBank_Id { get; set; }
        public CityBank CityBank { get; set; }
        #endregion

    }
}
