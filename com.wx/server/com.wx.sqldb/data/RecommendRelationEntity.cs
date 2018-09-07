using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    /// <summary>
    /// 用户推荐关系
    /// </summary>
    public class RecommendRelationEntity:BaseEntity
    {
        public int FromId { set; get; }
        public string RecommendId { set; get; }
        /// <summary>
        /// 1 扫描店铺
        /// </summary>
        public int RecommendType { set; get; }

    }
}
