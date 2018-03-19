

using System.ComponentModel;

namespace com.mh.model.enums
{

    /// <summary>
    /// 活动类型
    /// </summary>
    public enum ActionTypeEnum
    {
        /// <summary>
        /// 自动化营销
        /// </summary>
        [Description("自动化营销")]
        Auto = 0,
        /// <summary>
        /// 自定义营销
        /// </summary>
        [Description("自定义营销")]
        UserDefined = 1
    }



    /// <summary>
    /// 推送周期设置
    /// </summary>
    public enum PushCycleEnum
    {
        /// <summary>
        /// 周期推送
        /// </summary>
        [Description("周期推送")]
        Cycle = 0,
        /// <summary>
        /// 单次发送
        /// </summary>
        [Description("单次发送")]
        Single = 1,
    }


    /// <summary>
    /// 活动审核状态 '状态：0 审核中、1 未开始、3 进行中、4 已结束、-1 已撤销、2审核未通过',
    /// </summary>
    public enum StrategyActionStatus
    {
        /// <summary>
        /// 已终止
        /// </summary>
        [Description("已终止")]
        Terminate = -2,

        /// <summary>
        /// 已撤销
        /// </summary>
        [Description("已撤销")]
        Void = -1,

        /// <summary>
        /// 未开始，等待审核
        /// </summary>
        [Description("等待审核")]
        NotAudit = 0,

        /// <summary>
        /// 审核通过或udmp创建自动通过。
        /// </summary>
        [Description("未开始")]
        AuditPass = 1,

        /// <summary>
        /// 审核未通过
        /// </summary>
        [Description("审核未通过")]
        AuditRefuse = 2,

        /// <summary>
        /// 进行中
        /// </summary>
        [Description("进行中")]
        Started = 3,

        /// <summary>
        /// 已经结束
        /// </summary>
        [Description("已结束")]
        Finish = 4
    }


    public enum SectionSourceTypeEnum
    {

        /// <summary>
        /// 组织架构树选择
        /// </summary>
        [Description("组织架构树选择")]
        OrgTree = 0,
        /// <summary>
        /// 用户自定义选择
        /// </summary>
        [Description("用户自定义选择")]
        UserDefined = 1,

    }



    public class LevelRange
    {
        /// <summary>
        /// 是否使用子等级
        /// </summary>
        public bool IsSubLevel { get; set; }
        public double From { get; set; }
        public double To { get; set; }

        //子等级
        public int UnderLevel { get; set; }
        //原等级,如：新3-->高挽回预流失
        public int SourceLevel { get; set; }
    }

    public enum SelectedTargetTypeEnum
    {
        /// <summary>
        /// 共享分组
        /// </summary>
        Share = 1,
        /// <summary>
        /// 我的分组
        /// </summary>
        Privete = 2,
    }
}