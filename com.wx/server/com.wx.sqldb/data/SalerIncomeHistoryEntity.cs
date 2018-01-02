using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    /// <summary>
    /// 卖家收到提成记录
    /// </summary>
    public class SalerIncomeHistoryEntity : BaseEntity
    {
        public int SalerUserId { set; get; }
        public int SourceType { set; get; }
        public string SourceNO { set; get; }


        public decimal IncomeAmount { set; get; }
        /// <summary>
        /// 默认
        /// 冻结
        /// 失效
        /// 有效
        /// </summary>
        public int IncomeState { set; get; }


    }
}
