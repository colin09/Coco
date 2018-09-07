using System;

namespace com.mh.model.messages.message
{
    /// <summary>
    /// MemberInfo 相关操作
    /// </summary>
    public class MemberInfoMessage : BaseMessage
    {
        public override int SourceType => (int)MessageSourceType.MemberInfo;

        public override int ActionType => (int)MessageAction.MemberInfo;

        public MemberInfoSyncType SyncType { get; set; }

        public MemberInfoDto MemberInfo { get; set; }
    }


    public enum MemberInfoSyncType
    {
        Add = 0,
        ModifyByOrder = 1,
        /// <summary>
        /// 级别变化
        /// </summary>
        LeaveChange = 2,

        /// <summary>
        /// 扫码变化
        /// </summary>
        ModifByScan = 3
    }


    public class MemberInfoDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 集团编号
        /// </summary>
        public int GroupId { set; get; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public int StoreId { set; get; }
        /// <summary>
        /// 专柜编号
        /// </summary>
        public int SectionId { set; get; }

        /// <summary>
        /// 专柜编码
        /// </summary>
        public string SectionCode { get; set; }

        /// <summary>
        /// 顾客id
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birtday { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? Paymenttime { get; set; }

        /// <summary>
        /// 品牌编码
        /// </summary>
        public string BrandCode { get; set; }

        /// <summary>
        /// 买手编号
        /// </summary>
        public int BuyerUserId { get; set; }

        public int UserId { get; set; }

        public string VipCode { get; set; }

        public string OpenId { get; set; }

        public int OutsiteType { get; set; }

        public string MobilePhone { get; set; }

        /// <summary>
        /// 是否购买过
        /// </summary>
        public bool IsBuy { get; set; }

        /// <summary>
        /// 来源   shopping  15mins 等
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 会员当前等级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 会员当前等级
        /// </summary>
        public int LevelLast { get; set; }


        public string SubLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SubLevelLast { get; set; }

        /// <summary>
        /// 是否是扫码
        /// </summary>
        public bool IsScan { get; set; }

        /// <summary>
        /// 最后扫码时间
        /// </summary>
        public DateTime? LastScanTime { get; set; }

        /// <summary>
        /// 是否关注公众号
        /// </summary>
        public bool IsSubscribe { get; set; }

        /// <summary>
        /// 关注时间
        /// </summary>
        public DateTime? SubscribeTime { get; set; }

        /// <summary>
        ///取消关注时间
        /// </summary>
        public DateTime? UnSubscribeTime { get; set; }

        public DateTime? RegisterTime { get; set; }


        //有效积分   会员卡级别   累计积分

        /// <summary>
        /// 累计积分
        /// </summary>
        public double Integral { get; set; }

        /// <summary>
        /// 会员卡级别
        /// </summary>
        public string CardLevel { get; set; }
        /// <summary>
        /// 有效积分
        /// </summary>
        public double ValidAmount { get; set; }

    }
}
