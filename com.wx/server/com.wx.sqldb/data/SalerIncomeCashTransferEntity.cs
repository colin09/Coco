using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    /// <summary>
    /// 提现申请执行记录
    /// </summary>
    public class SalerIncomeCashTransferEntity : BaseEntity
    {
        public int SalerUserId { set; get; }
        public int CashApplyId { set; get; }
        public decimal Amount { set; get; }
        public int TransferState { set; get; }

        public string TransferNO { set; get; }
        public string ResultCode { set; get; }
        public string ResultMsg { set; get; }
        public string ErrorCode { set; get; }
        public string Content { set; get; }


    }
}
