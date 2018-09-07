using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{

    /// <summary>
    /// 用户计数信息
    /// </summary>
    public class UserCountEntity : BaseEntity
    {

        public int UserId { set; get; }
        /// <summary>
        /// enum
        ///     一级代理数
        ///     二级代理数
        ///     三级代理数
        /// </summary>
        public int PropertyType { set; get; }
        public int PropertyValue { set; get; }

    }
}
