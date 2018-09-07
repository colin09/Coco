using ERP.Domain.Enums.OrderModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrderModule
{
    public class ReturnOrder : BaseEntity, IAggregationRoot
    {
        public ReturnOrder()
        {
            ReturnOrderTime = new ReturnOrderTime();
        }

        public string ReturnOrderNo { get; set; }
        /// <summary>
        /// 是否已下推生成了 退货入库单
        /// </summary>
        public bool HasCreateReturnNote { get; set; }
        /// <summary>
        /// 退货商品金额
        /// </summary>
        public decimal ReturnProductAmount { get; set; }

        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal ReturnAmount { get; set; }
        /// <summary>
        /// 补充利润
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        ///<summary>
        ///退货单状态(客服用)
        ///</summary>
        public ReturnState ReturnState { get; set; }
        /// <summary>
        /// 退货单时间
        /// </summary>
        public ReturnOrderTime ReturnOrderTime { get; set; }

        #region 聚合属性
        /// <summary>
        /// 城市Id
        /// </summary>
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
       
        public City City { get; set; }

        [MaxLength(64)]
        [ForeignKey("Order")]
        public string Order_Id { get; set; }
        public Order Order { get; set; }

        /// <summary>
        /// 退货单明细
        /// </summary>
        public List<ReturnOrderItem> Items { get; set; }
        #endregion
    }
}
