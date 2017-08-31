using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    /// <summary>
    /// 提现申请
    /// </summary>
  public  class SalerIncomeCashApplyEntity : BaseEntity
    {
        public int SalerUserId { set; get; }
        public decimal Amount { set; get; }
        public int ApplyState { set; get; }
        public int RetryCount { set; get; }
        public int PaymentType { set; get; }
        public decimal TransferFee { set; get; }

        public string ErrorCode { set; get; }
        public string ErrorMsg { set; get; }
    }
}
