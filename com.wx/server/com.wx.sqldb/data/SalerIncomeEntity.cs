using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    /// <summary>
    /// 卖家提成
    /// </summary>
    public class SalerIncomeEntity : BaseEntity
    {
        public int SalerUserId { set; get; }
        public decimal TotalIncome { set; get; }
        public decimal AvailIncome { set; get; }
        public decimal RequestAmount { set; get; }
        public decimal ReceiveAmount { set; get; }
    }
}
