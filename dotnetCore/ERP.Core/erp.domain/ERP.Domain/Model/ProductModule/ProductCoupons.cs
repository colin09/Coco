using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ProductModule
{
    public class ProductCoupons : IValueObject
    {

        ///<summary>
        ///发放面值
        ///</summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// 打折比例（0-1）
        /// </summary>
        public decimal? Percent { get; set; }

        #region 使用条件

        ///<summary>
        ///使用条件起始金额
        ///</summary>
        public decimal? UseOrderAmountFrom { get; set; }

        ///<summary>
        ///使用条件封顶金额
        ///</summary>
        public decimal? UseOrderAmountTo { get; set; }

        #endregion

        /// <summary>
        /// 获取优惠券，最低采购数量
        /// </summary>
        public int? PurchasedNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 最多可获取优惠券金额
        /// </summary>
        public decimal? MaxCoupons { get; set; }

        /// <summary>
        /// 优惠券有效时长
        /// </summary>
        public int? Duration { get; set; }

    }
}
