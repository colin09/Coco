using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.AgentSettlementNoteModule;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialBillModule;
using ERP.Domain.Model.FinancialBillModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema; // for Annotations Index
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.AgentSettlementNoteModule
{
    public class AgentSettlementNote : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 单据编号
        /// </summary>
        [MaxLength(64)]
        //[Index(IsClustered = false, IsUnique = false)]        
        [Index]
        public string NoteNo { get; set; }

        /// <summary>
        /// 核算单状态
        /// </summary>
        public AgentSettlementNoteState State { get; set; }

        /// <summary>
        /// 大单位数量
        /// </summary>
        public int SpecificationsCount { get; set; }
        /// <summary>
        /// 小单位数量
        /// </summary>
        public int UnitCount { get; set; }

        /// <summary>
        /// 区域销售金额
        /// </summary>
        public decimal SaleAmount { get; set; }

        /// <summary>
        /// 总部 应收金额
        /// </summary>
        public decimal ReceivableAmount { get; set; }

        /// <summary>
        /// 区域应付金额
        /// </summary>
        public decimal PayableAmount { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }

        #endregion

        #region 聚合属性

        [ForeignKey("Provider")]
        [MaxLength(64)]
        public string Provider_Id { get; set; }

        [ForeignKey("AgentCity")]
        [MaxLength(64)]
        public string AgentCity_Id { get; set; }

        [ForeignKey("SettlementCity")]
        [MaxLength(64)]
        public string SettlementCity_Id { get; set; }
        /// <summary>
        /// 代理供应商
        /// </summary>
        public Provider Provider { get; set; }

        /// <summary>
        /// 代理城市（易酒批代理）
        /// </summary>
        public City AgentCity { get; set; }

        /// <summary>
        /// 结算城市
        /// </summary>
        public City SettlementCity { get; set; }

        public List<AgentSettlementNoteProductItem> Items { get; set; }
        #endregion

        #region 业务方法

        /// <summary>
        /// 开始核算
        /// </summary>
        public virtual void Settlement()
        {
            if (this.State != AgentSettlementNoteState.待提交)
            {
                throw new BusinessException("只有待提交状态可核算！");
            }

            this.State = AgentSettlementNoteState.待核算;
        }

        /*   
        /// <summary>
        /// 付款
        /// </summary>
        public virtual string[] Pay(ERPContext context, Settlement settlement, string bankNo = null)
        {
            if (this.State != AgentSettlementNoteState.待核算)
            {
                throw new BusinessException("只有待核算状态可付款！");
            }
            this.State = AgentSettlementNoteState.已付款;

            var paymentBills = context.PaymentBills.Include("Items").Where(b => b.AgentSettlementNote_Id == this.Id).ToList();

            var auditBillNos = paymentBills.Where(b => b.AuditState == AuditState.已审核).Select(b => b.BillNo).ToArray();
            if (auditBillNos.Any())
            {
                throw new BusinessException(string.Format("付款单：{0}已审核，付款失败！", string.Join(",", auditBillNos)));
            }

            paymentBills.ForEach(b =>
            {
                b.Items.ForEach(i =>
                {
                    i.Settlement = settlement;
                    i.OurBankNo = bankNo;
                });

                b.ComputeAmount();
            });
            return paymentBills.Select(b => b.Id).ToArray();
        }

        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settlement"></param>
        /// <param name="bankNo"></param>
        /// <returns>收款单id</returns>
        public virtual string[] ReceiptMoney(ERPContext context, Settlement settlement, string bankNo = null)
        {
            if (this.State != AgentSettlementNoteState.已付款)
            {
                throw new BusinessException("只有已付款状态可确认收款！");
            }
            this.State = AgentSettlementNoteState.已核算;

            var receiptBills = context.ReceiptBills.Include("Items").Where(b => b.AgentSettlementNote_Id == this.Id).ToList();

            var auditBillNos = receiptBills.Where(b => b.AuditState == AuditState.已审核).Select(b => b.BillNo).ToArray();
            if (auditBillNos.Any())
            {
                throw new BusinessException(string.Format("收款单：{0}已审核，收款失败！", string.Join(",", auditBillNos)));
            }

            receiptBills.ForEach(b =>
            {
                b.Items.ForEach(i =>
                {
                    i.Settlement = settlement;
                    i.OurBankNo = bankNo;
                });

                b.ComputeAmount();
            });
            return receiptBills.Select(b => b.Id).ToArray();

        }*/

        #endregion
    }
}
