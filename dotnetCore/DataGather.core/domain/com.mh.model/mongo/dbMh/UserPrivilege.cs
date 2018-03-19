using System;
using System.ComponentModel;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{
    /// <summary>
    /// 用户特权
    /// </summary>
    [BsonIgnoreExtraElements]
    public class UserPrivilege : MagicHorseBase
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 专柜id
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// 买手id
        /// </summary>
        public int BuyerUserId { get; set; }

        /// <summary>
        /// 兑换时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 特权id
        /// </summary>
        public string PrivilegeId { get; set; }

        /// <summary>
        /// 特权名称
        /// </summary>
        public string PrivilegeName { get; set; }

        /// <summary>
        /// 金币数
        /// </summary>
        public int GoldAmount { get; set; }

        /// <summary>
        /// 经验值
        /// </summary>
        public int ExperienceAmount { get; set; }

        /// <summary>
        /// 特权状态   未使用  已使用   已失效  已冻结  已删除 已过期
        /// </summary>
        public UserPrivilegeStatusEnum Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FromTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ToTime { get; set; }

        /// <summary>
        /// 优惠券信息
        /// </summary>
        public List<UserPrivilegeItem> PrivilegeItem { get; set; }

    }

    /// <summary>
    /// 用户特权优惠券配置
    /// </summary>
    [BsonIgnoreExtraElements]
    public class UserPrivilegeItem
    {
        /// <summary>
        /// 优惠券Id
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string CouponCode { get; set; }
        public string CouponPwd { get; set; }
        /// <summary>
        /// 优惠券类型;   神马 、百丽??????
        /// </summary>
        public CouponSource CouponType { get; set; }


        public CouponClass CouponClass { get; set; }
        /// <summary>
        /// 满多少
        /// </summary>
        public decimal Fullminus { get; set; }
        /// <summary>
        /// 减多少或者折多少 1-0.85 为打85折
        /// </summary>
        public decimal Discount { get; set; }
    }


public enum UserPrivilegeStatusEnum
    {
        //未使用  已使用   已失效  已冻结  已删除 已过期


        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Expired = -3,

        /// <summary>
        /// 已失效
        /// </summary>
        [Description("已失效")]
        Invalid = -2,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Delete = -1,

        /// <summary>
        /// 未使用
        /// </summary>
        [Description("未使用")]
        Unused = 0,

        /// <summary>
        /// 已冻结
        /// </summary>
        [Description("已冻结")]
        Freeze = 1,

        /// <summary>
        /// 已使用
        /// </summary>
        [Description("已使用")]
        Used = 2,



    }
}