using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.FinancialSettlementModule
{
    public class FinancialSettlementCheck : BaseEntity, IAggregationRoot
    {
        #region 金蝶数据

        /// <summary>
        /// 主营业务收入（普通销售收入）
        /// </summary>
        public decimal JD_OrderAmount { get; set; }
        /// <summary>
        /// 主营业务成本（普通销售成本）
        /// </summary>
        public decimal JD_CostAmount { get; set; }
        /// <summary>
        /// 其他业务收入（集团内部销售收入）
        /// </summary>
        public decimal JD_OrderAmountInternal { get; set; }
        /// <summary>
        /// 其他业务成本（集团内部销售成本）
        /// </summary>
        public decimal JD_CostAmountInternal { get; set; }
        /// <summary>
        /// 销售费用-折扣
        /// </summary>
        public decimal JD_OrderDiscountAmount { get; set; }
        /// <summary>
        /// 金蝶库存商品
        /// </summary>
        public decimal JD_StoreAmount { get; set; }
        /// <summary>
        /// 应付账款-供应商往来
        /// </summary>
        public decimal JD_PayableAmount { get; set; }
        /// <summary>
        /// 其他应付款-集团内部采购
        /// </summary>
        public decimal JD_PayableAmountInternal { get; set; }
        /// <summary>
        /// 其他应收款-集团内部销售
        /// </summary>
        public decimal JD_ReceivableAmountInternal { get; set; }

        #endregion

        #region 非集团内部
        /// <summary>
        /// 本月销售出库单价税合计+折扣额
        /// </summary>
        public decimal OrderTotalAmount { get; set; }
        /// <summary>
        /// 本月销售出库单折扣额
        /// </summary>
        public decimal OrderDiscountAmount { get; set; }
        /// <summary>
        /// 本月销售出库单价税合计
        /// </summary>
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 本月退货入库单价税合计+折扣额
        /// </summary>
        public decimal ReturnOrderTotalAmount { get; set; }
        /// <summary>
        /// 退货入库单折扣额
        /// </summary>
        public decimal ReturnOrderDiscountAmount { get; set; }
        /// <summary>
        /// 退货入库单价税合计
        /// </summary>
        public decimal ReturnOrderAmount { get; set; }
        /// <summary>
        /// 销售利润估算分析表 销售成本（销售出库单成本-退货入库单成本）
        /// </summary>
        public decimal SaleCostAmount { get; set; }
        /// <summary>
        /// 本月应付未付款
        /// </summary>
        public decimal NopaidAmount { get; set; }
        #endregion

        #region 集团内部销售
        /// <summary>
        /// 本月销售出库单价税合计
        /// </summary>
        public decimal OrderAmountInternal { get; set; }
        /// <summary>
        /// 本月销售退货单价税合计
        /// </summary>
        public decimal ReturnOrderAmountInternal { get; set; }
        /// <summary>
        /// 本月销售毛利估算分析表成本
        /// </summary>
        public decimal SaleCostAmountInternal { get; set; }
        /// <summary>
        /// 本月底应付未付款余额
        /// </summary>
        public decimal NotReceiptAmountInternal { get; set; }
        /// <summary>
        /// 本月底应收未收款余额
        /// </summary>
        public decimal NopaidAmountInternal { get; set; }
        #endregion

        #region 库存核对

        public decimal BalanceAmount { get; set; }
        public decimal PrizeInAmount { get; set; }
        public decimal PrizeOutAmount { get; set; }
        public decimal DamageAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public decimal CheckInAmount { get; set; }
        public decimal CheckOutAmount { get; set; }
        public decimal CostAdjustAmount { get; set; }

        #endregion
    }
}
