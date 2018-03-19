using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{


    /// <summary>
    /// 效果分析表
    /// </summary>
    [BsonIgnoreExtraElements]
    public class StrategyEffect : MagicHorseBase
    {
        /// <summary>
        /// 策略信息
        /// </summary>
        public EffectStrategyInfo Strategy { get; set; }
        /// <summary>
        /// 活动信息
        /// </summary>
        public EffectActionInfo Action { get; set; }
        /// <summary>
        /// 优惠券信息
        /// </summary>
        public EffectCouponInfo Coupon { get; set; }


        /// <summary>
        /// 附加券
        /// </summary>
        public EffectCouponInfo AdditionCoupon { get; set; }

        /// <summary>
        /// 发送的信息
        /// </summary>
        public EffectMessage Message { get; set; }


        /// <summary>
        /// 针对多张券多条消息
        /// </summary>
        public List<EffectMessage> Messages { get; set; }

        public int GroupId { get; set; }//"集团编号",
        public int StoreId { get; set; } //门店编号
        public int SectionId { get; set; } //专柜编号
        public string SectionCode { get; set; } //专柜编码
        public int BuyerUserId { get; set; }
        public string MemberId { get; set; }//会员Id" //

        /// <summary>
        /// 顾客所在的集团
        /// </summary>
        public int MemberGroupId { get; set; }

        /// <summary>
        /// 顾客所在的门店
        /// </summary>
        public int MemberStoreId { get; set; }

        /// <summary>
        /// 顾客所在的专柜
        /// </summary>
        public int MemberSectionId { get; set; }

        /// <summary>
        /// 是否是其他商户的顾客
        /// </summary>
        public bool IsOutSiteMember { get; set; } = false;

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string VipCode { get; set; }

        /// <summary>
        /// 会员级别变化情况
        /// </summary>
        public string MemberChangeLeave { get; set; }
        public bool UserIsBack { get; set; } //标示用户是否被活动拉回
        public DataStatus Status { get; set; } //状态 -1 被删除 0：被淘汰 1：正常

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; } //创建时间

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateDate { get; set; } //修改时间

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime SendDate { get; set; } //消息推送时间

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateGroup SendDateGroup { get; set; }
        /// <summary>
        /// 顾客回头时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? BackDate { get; set; } //顾客回到购买的时间

        /// <summary>
        /// 回头时间
        /// </summary>
        public DateGroup BackDateGroup { get; set; }


        /// <summary>
        /// 回头购买列表
        /// </summary>
        public List<BackInfo> BackList { get; set; }

        //添加当前执行的是顾客的哪天的行为

        /// <summary>
        /// 对应当前用户哪一天的状态
        /// </summary>
        public DateTime CalDate { get; set; }

    }


    [BsonIgnoreExtraElements]
    public class BackInfo
    {
        public DateTime BackDate { get; set; }

        /// <summary>
        /// 回头时间
        /// </summary>
        public DateGroup BackDateGroup { get; set; }

    }


    [BsonIgnoreExtraElements]
    public class EffectStrategyInfo
    {
        public int Id { get; set; }//策略编号
        public string SubId { get; set; } //子策略编号
        public string Name { get; set; }//策略名称
        public string SubName { get; set; }//子策略名称
    }

    [BsonIgnoreExtraElements]
    public class EffectActionInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ActionExecuteDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ActionExecuteTime { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class EffectMessage
    {
        public MessageToCustomer ToCustomer { get; set; }
        public MessageToDaogouApp ToDaogouApp { get; set; }

        public SmsToCustomer SmsToCustomer { get; set; }

        /// <summary>
        /// 优惠券信息
        /// </summary>
        public EffectCouponInfo Coupon { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class EffectCouponInfo
    {
        /// <summary>
        /// 优惠券来源 0：shopping 1:15mins ,
        /// </summary>
        public CouponSource CouponFromType { get; set; }



        /// <summary>
        /// 优惠券类型   满减 还是满折
        /// </summary>
        public CouponClass CouponClass { get; set; }

        /// <summary>
        /// 优惠券编号 如果是0：user_coupon表Id 如果是1:15minscoupon表Id
        /// </summary>
        public int CouponId { get; set; }

        public string CouponCode { get; set; }

        /// <summary>
        /// 优惠券的string类型Id
        /// </summary>
        public string CouponStringId { get; set; }
    }









    /// <summary>
    /// 写订单执行反馈用
    /// </summary>
    [BsonIgnoreExtraElements]
    public class ActionEffect
    {
        public ActionEffect()
        {
        }
        public ActionEffect(string memberId, DateTime paymentTime)
        {
            MemberId = memberId;
            PaymentTime = paymentTime;
        }
        public ActionEffect(string memberId, string actionId, DateTime paymentTime)
        {
            MemberId = memberId;
            ActionId = actionId;
            PaymentTime = paymentTime;
        }

        public string MemberId { set; get; }
        public string ActionId { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PaymentTime { set; get; }

    }

    [BsonIgnoreExtraElements]
    public class MessageToCustomer
    {
        public string Content { get; set; }

        public string Url { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class MessageToDaogouApp
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 推送单个顾客   进店扫码的就默认推送一个顾客
        /// </summary>
        public bool SendSingleCustomer { get; set; }
        /// <summary>
        /// 推送多个顾客
        /// </summary>
        public bool SendMultiCustomer { get; set; }
    }

    /// <summary>
    /// 给顾客推送断线
    /// </summary>
    public class SmsToCustomer
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }


}