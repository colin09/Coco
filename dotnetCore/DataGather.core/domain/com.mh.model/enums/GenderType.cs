using System.ComponentModel;

namespace com.mh.model.enums
{
    public enum GenderType
    {
        [Description("保密")]
        Default = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 2
    }

}