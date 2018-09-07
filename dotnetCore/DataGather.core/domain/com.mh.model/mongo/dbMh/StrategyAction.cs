using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{
 [BsonIgnoreExtraElements]
    public class StrategyAction : MagicHorseBase
    {
        /// <summary>
        /// 集团id
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 门店id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 创建人的OrgId
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 活动显示名称/别名
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FromDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ToDate { get; set; }
        /// <summary>
        /// 参与活动的专柜列表
        /// </summary>
        public List<int> Sections { get; set; }

        /// <summary>
        /// 此活动关联的组织架构Id
        /// </summary>
        public List<AssociateOrg> AssociateOrgIds { get; set; }

        /// <summary>
        /// 选择的发送目标
        /// </summary>
        public SelectedTarget SelectedTarget { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public ActionTypeEnum ActionType { get; set; }

        /// <summary>
        /// 活动标签
        /// </summary>
        public List<Tag> Tags { get; set; }
        /// <summary>
        /// 推送周期设置
        /// </summary>
        public PushCycleEnum PushCycle { get; set; }

        /// <summary>
        /// 执行的时间点
        /// </summary>
        public int PushMsgTimePoint { get; set; }

        /// <summary>
        /// 插件信息
        /// </summary>
        public PluginConfig PluginConfig { get; set; }
        /// <summary>
        /// 消息配置
        /// </summary>
        public MemberConfig MemberConfig { get; set; }

        /// <summary>
        /// 会员级别
        /// </summary>
        public VipInfoConfig VipInfoConfig { get; set; }

        /// <summary>
        /// 活动信息、券信息
        /// </summary>
        public List<SendBody> SendBody { get; set; }


        #region 保留原有属性，兼容交易引擎

        /// <summary>
        /// 消息
        /// </summary>
        public MessageConfig MessageConfig { get; set; }

        /// <summary>
        /// 优惠券
        /// </summary>
        public CouponConfig CouponConfig { get; set; }

        #endregion

        /// <summary>
        /// 参与顾客次数
        /// </summary>
        public int CustomerCount { get; set; }
        /// <summary>
        /// 成交顾客次数
        /// </summary>
        public int CustomerCostCount { get; set; }
        /// <summary>
        /// '转化率 CustomerCostCount/CustomerCount',
        /// </summary>
        public double ConversionRate { get; set; }
        /// <summary>
        /// '状态
        /// </summary>
        public StrategyActionStatus Status { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }


        /// <summary>
        /// 审核意见
        /// </summary>
        public List<ActionAudit> Audit { get; set; }

        /// <summary>
        /// 操作日志
        /// </summary>
        public List<ActionOperateLog> OperateLog { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? StopDate { get; set; }

        /// <summary>
        /// 创建活动，门店选择的源类型
        /// </summary>
        public SectionSourceTypeEnum SectionSourceType { get; set; }

        /// <summary>
        /// 是否为批量创建
        /// </summary>
        public bool IsBatch { get; set; }

        /// <summary>
        /// 批量表示
        /// </summary>
        public string BatchKey { get; set; }
        /// <summary>
        /// 是否已更改过。
        /// </summary>
        public bool IsChanged { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class AssociateOrg
    {
        public string OrgId { get; set; }
        public string ActionId { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class SendBody
    {
        public int Index { get; set; }
        /// <summary>
        /// 消息配置
        /// </summary>
        public MessageConfig MessageConfig { get; set; }
        /// <summary>
        /// 优惠券配置
        /// </summary>
        public CouponConfig CouponConfig { get; set; }

    }

    /// <summary>
    /// 插件配置
    /// </summary>
    [BsonIgnoreExtraElements]
    public class PluginConfig
    {
        /// <summary>
        /// 模块id
        /// </summary>
        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        /// <summary>
        /// 插件英文描述
        /// </summary>
        public string ExecuteModuleName { get; set; }

        /// <summary>
        /// 子模块id
        /// </summary>
		public string SubModuleId { get; set; }

        public string SubModuleName { get; set; }

        /// <summary>
        /// 发送方式
        /// </summary>
        public OnRuntime OnRuntime { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public LevelRange LevelRange { get; set; }
    }

    /// <summary>
    /// 自定义推送配置
    /// </summary>
    [BsonIgnoreExtraElements]
    public class PushMsgTime
    {
        public PushMsgTime()
        {
            this.PushDays = new List<DayOfWeek>();
        }

        /// <summary>
        /// 每隔几天发送
        /// </summary>
        public int SplitDays { get; set; }

        /// <summary>
        /// 推送时间。
        /// </summary>
        public List<DayOfWeek> PushDays { get; set; }

        /// <summary>
        /// 只包含周末
        /// </summary>
        public bool OnWeekend { get; set; }
        /// <summary>
        /// 不包括周末
        /// </summary>
        public bool NotOnWeekend { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class OnRuntime
    {
        /// <summary>
        /// 是否实时发送
        /// </summary>
        public bool OnRealtime { get; set; }
        /// <summary>
        /// 是否生日发送
        /// </summary>
        public bool OnBirthday { get; set; }


        /// <summary>
        /// 提前多少天
        /// </summary>
        public int BeforeDays { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeRange TimeRange { get; set; }

        /// <summary>
        /// 发送周期
        /// </summary>
        public PushMsgTime PushMsgTime { get; set; }


        public LimitPushConfig LimitPushConfig { get; set; }


    }
    /// <summary>
    /// 限制发送配置
    /// 如果一个活动设置了该字段，则执行的时候。
    /// 判断该用户是否有其他升降级，生日，节庆 发送。 
    /// 如果有并且发送时间在该活动设置的天数以内。 
    /// 则该活动不再给客户发送信息/优惠券等
    /// </summary>
    [BsonIgnoreExtraElements]
    public class LimitPushConfig
    {
        public bool IsChecked { get; set; }

        //public List<int> LimitModules { get; set; } //不可以并行发送的活动类型

        //public List<string> LimitSubModules { get; set; } //不可以并发的子类型

        public int LimitDay { get; set; }

    }


    [BsonIgnoreExtraElements]
    public class TimeRange
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime From { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime To { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Begin { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime End { get; set; }
    }
    /// <summary>
    /// 会员配置
    /// </summary>
    [BsonIgnoreExtraElements]
    public class MemberConfig
    {

        #region 老字段
        /// <summary>
        /// 是否推送准会员
        /// </summary>
        public bool Level0IsCheck { get; set; }
        /// <summary>
        /// 是否发送其他会员(udmp不再使用。)
        /// </summary>
        public bool LevelOtherIschecked { get; set; }
        /// <summary>
        /// 发送给会员的级别
        /// </summary>
        public List<int> LevelArray { get; set; }

        /// <summary>
        /// 新会员是否选择
        /// </summary>
        public bool NewMemberIsCheck { get; set; }

        /// <summary>
        /// 活跃会员
        /// </summary>
        public bool ActiveMemberIsCheck { get; set; }
        /// <summary>
        /// 预流失会员
        /// </summary>
        public bool PreLossMemberIsCheck { get; set; }
        /// <summary>
        /// 流失及休眠
        /// </summary>
        public bool LossAndSleepMemberIsCheck { get; set; }
        #endregion

    }

    #region 消息
    [BsonIgnoreExtraElements]
    public class MessageConfig
    {
        public bool IsChecked { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public ToCustomer ToCustomer { get; set; }

        /// <summary>
        /// 导购app
        /// </summary>
        public ToDaogouApp ToDaogouApp { get; set; }


        /// <summary>
        /// 活动短信
        /// </summary>
        public ToActionSms ToActionSms { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class ToCustomer
    {
        public bool IsChecked { get; set; }

        /// <summary>
        /// 推送消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 推送消息url
        /// </summary>
        public string Url { get; set; }

        /*
        /// <summary>
        /// 模版Id
        /// </summary>
        public string TemplateId { get; set; }
        */

        /// <summary>
        /// 通知类型
        /// </summary>
        public string NoticeType { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class ToDaogouApp
    {
        public bool IsChecked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool SendSingleCustomer { get; set; }
        /// <summary>
        /// </summary>
        public bool SendMultiCustomer { get; set; }
        /// <summary>
        /// 推送消息内容
        /// </summary>
        public string Content { get; set; }
    }


    /// <summary>
    /// 活动短信推送
    /// </summary>
    [BsonIgnoreExtraElements]
    public class ToActionSms
    {
        /// <summary>
        /// 是否打开短信功能
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 短信签名位置  0：前面  1：后面
        /// </summary>
        public int SignPosition { get; set; }

        /// <summary>
        /// 短信模版Id
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
    }
    #endregion

    #region 优惠券

    /// <summary>
    /// 优惠券配置
    /// </summary>
    [BsonIgnoreExtraElements]
    public class CouponConfig
    {
        public bool IsChecked { get; set; }
        /// <summary>
        /// 0:控制优惠力度，系统只能送券 1:给顾客发送同一种神马券 2:给顾客发送同一种15mins券
        /// </summary>
		public CouponMethodEnum CouponMethod { get; set; }

        public CouponConfigBody Body { get; set; }

        /// <summary>
        /// //优惠券生效时间 0:当前有效
        /// </summary>
        public int IntervalDays { get; set; }

        /// <summary>
        /// //优惠券的有效期
        /// </summary>
        public int EffectiveDays { get; set; }



        /// <summary>
        /// 过期提醒配置
        /// </summary>
        public CouponNoticConfig NoticConfig { get; set; }

        /// <summary>
        /// 详情页模板配置
        /// </summary>
        public CouponTempleteConfig DetailTempleteConfig { get; set; }
    }
    /// <summary>
    /// 优惠券配置内容
    /// </summary>
    [BsonIgnoreExtraElements]
    public class CouponConfigBody
    {
        /*
        /// <summary>
        /// 优惠券生效时间 0:当前有效
        /// </summary>
        public int IntervalDays { get; set; }

        /// <summary>
        /// 优惠券的有效期
        /// </summary>
		public int EffectiveDays { get; set; }
        */
        /// <summary>
        /// 优惠度设定值
        /// </summary>
        public DiscountRate DiscountRate { get; set; }

        /// <summary>
        /// 优惠券类型
        /// </summary>
        public SysCoupon SysCoupon { get; set; }

        public FFMinsCoupon FFMinsCoupon { get; set; }
    }

    /// <summary>
    /// 优惠券提醒配置
    /// </summary>
    [BsonIgnoreExtraElements]
    public class CouponNoticConfig
    {
        /// <summary>
        /// 是否发送优惠券提醒
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 有效期前第几天  可以是个天数  前 10天一次  前5天一次
        /// </summary>
        public List<int> SendDays { get; set; }
        /// <summary>
        /// 推送的时间点
        /// </summary>
        public int NoticTimePoint { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public ToCustomer ToCustomer { get; set; }

        /// <summary>
        /// 活动短信
        /// </summary>
        public ToActionSms ToActionSms { get; set; }

    }

    /// <summary>
    /// 消息模板配置
    /// </summary>
    [BsonIgnoreExtraElements]
    public class CouponTempleteConfig
    {
        /// <summary>
        /// 是否选择模板
        /// </summary>
        public bool IsChecked { get; set; }

        public string TempleteId { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class DiscountRate
    {
        public double From { get; set; }
        public double To { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class SysCoupon
    {
        /// <summary>
        /// 优惠券类型
        /// </summary>
        public CouponClass CouponClass { get; set; } //优惠券类型
        /// <summary>
        /// 满多少
        /// </summary>
        public double Fullminus { get; set; }

        /// <summary>
        /// 减多少或者折多少 1-0.85 为打85折
        /// </summary>
        public double Discount { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class FFMinsCoupon
    {
        /// <summary>
        /// mysql中的分类Id
        /// </summary>
        public int CouponGroupId { get; set; }

        /// <summary>
        /// 分类Id   Mongo 中的分类Id
        /// </summary>
        public string CategoryId { get; set; }
    }
    #endregion

    #region 审核意见
    /// <summary>
    /// 活动审核意见
    /// </summary>
    [BsonIgnoreExtraElements]
    public class ActionAudit
    {
        /// <summary>
        /// 审核意见值：1：同意 -1 不同意
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 老的状态
        /// </summary>
        public StrategyActionStatus OldStatus { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public StrategyActionStatus CurrentStatus { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string Remark { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
    }
    #endregion

    #region 操作日志

    [BsonIgnoreExtraElements]
    public class ActionOperateLog
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public int OperateUser { get; set; } //操作人

        /// <summary>
        /// 操作类型
        /// </summary>
		public string Operate { get; set; }//动作

        /// <summary>
        /// 操作时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime OperateDate { get; set; } //操作时间
    }


    public enum ActionOperate
    {
        Create = 0,
        Update = 1,
    }
    #endregion

    #region 优惠券命中
    [BsonIgnoreExtraElements]
    public class ActionHitCouponConfig : MagicHorseBase
    {
        /// <summary>
        /// 当前actionIds
        /// </summary>
        public string ActionId { get; set; }

        public int CouponType { get; set; }
        /// <summary>
        /// 优惠券类型
        /// </summary>
        public CouponClass Class { get; set; }
        /// <summary>
        /// 满多少
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 减多少或者折多少，如果class = 1 then 0-1内小数
        /// </summary>
        public double DiscountAmount { get; set; }

        /// <summary>
        /// 优惠比率 如果是满多少，则discountamount/amount  否则 discountamount
        /// </summary>
        public double DiscountRate { get; set; }// 减多少或者折多少，如果class = 1 then 0-1内小数     
        /// <summary>
        /// 消费次数
        /// </summary>
        public int CostCount { get; set; }
        /// <summary>
        /// 命中次数
        /// </summary>
        public int HitCount { get; set; }
        /// <summary>
        /// 转化率
        /// </summary>
        public double ConversionRate { get; set; }
        /// <summary>
        /// 对应基础数据表Id
        /// </summary>
        public string StoreBaseCouponConfigId { get; set; }
        /// <summary>
        /// //状态 -1 被删除 0：被淘汰 1：正常
        /// </summary>
        public OpcCouponConfigStatus Status { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
    }
    #endregion

    #region 会员卡级别
    /// <summary>
    /// 会员卡级别相关配置
    /// </summary>
    [BsonIgnoreExtraElements]
    public class VipInfoConfig
    {
        public bool IsChecked { get; set; }

        public VipLeaves VipLeaves { get; set; }

        public VipChange VipChange { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class VipLeaves
    {
        public bool IsChecked { get; set; }

        /// <summary>
        /// 会员卡级别：存储viplevel code，去重。
        /// </summary>
        public List<double> Leaves { get; set; }

        /// <summary>
        /// 变化前多少天
        /// </summary>
        public int FromChangeBeforeDay { get; set; }

        /// <summary>
        /// 变化后多少月
        /// </summary>
        public int FromChangeMonth { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public VipLeaveNoticType NoticType { get; set; }

    }

    public class VipChange
    {
        public bool IsChecked { get; set; }

        /// <summary>
        /// 上一个级别
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 下一个级别
        /// </summary>
        public string To { get; set; }


    }
    #endregion


    /// <summary>
    /// 选择的发送目标
    /// </summary>
    [BsonIgnoreExtraElements]
    public class SelectedTarget
    {
        /// <summary>
        /// 分组类型。
        /// </summary>
        public SelectedTargetTypeEnum SelectedTargetType { get; set; } = SelectedTargetTypeEnum.Privete;

        /// <summary>
        /// 选择的分组,自定义营销
        /// </summary>
        public List<string> GroupId { get; set; }
        /// <summary>
        /// 选择的组织架构,自定义营销
        /// </summary>
        public List<string> OrgId { get; set; }
    }


    /// <summary>
    /// 活动标签
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Tag
    {
        /// <summary>
        /// 标签编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 操作日志
    /// </summary>
    [BsonIgnoreExtraElements]
    public class OperateLogItem
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public int OperateUser { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        public string Operate { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime OperateDate { get; set; }
    }


}
