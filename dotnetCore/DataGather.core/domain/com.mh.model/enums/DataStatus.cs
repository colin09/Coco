
using System.ComponentModel;

namespace com.mh.model.enums
{


    public enum DataStatus
    {
        /// <summary>
        /// 已删除（逻辑删除）
        /// </summary>
        [Description("删除")]
        Deleted = -1,
        /// <summary>
        /// 默认状态
        /// </summary>
        [Description("未上架")]
        Default = 0,
        /// <summary>
        /// 正常状态
        /// </summary>
        [Description("已上架")]
        Normal = 1,
    }
}