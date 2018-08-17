using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ProductModule
{
    public class ProductBonus : IValueObject
    {
        /// <summary>
        /// 获取红包，最低采购数量
        /// </summary>
        public int? PurchasedNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 赠送红包金额
        /// </summary>

        public decimal? BonusAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 生效前禁止使用时间
        /// </summary>
        public int? DisableDays { get; set; }

        /// <summary>
        /// 红包有效时长
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// 红包有效截止时间
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 本次订单能否立即使用
        /// </summary>
        public bool? UseImmediately { get; set; }

        /// <summary>
        /// 最多可获取红包数量
        /// </summary>
        public decimal? MaxBonus { get; set; }
    }
}
