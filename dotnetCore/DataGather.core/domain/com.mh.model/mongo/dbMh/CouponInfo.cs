using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{

   [BsonIgnoreExtraElements]
    public class CouponInfo : MagicHorseBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 第三方的Id
        /// </summary>
        public string OutsiteId { get; set; }

        /// <summary>
        /// 第三方分类Id
        /// </summary>
        public string OutSiteCategoryId { get; set; }

        /// <summary>
        /// 优惠券所属
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 优惠券所属组织
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 优惠券所属集团
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 发券专柜
        /// </summary>
        public int SendSectionId { get; set; }

        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 优惠券密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 优惠券类型
        /// </summary>
        public CouponClass CouponClass { get; set; }


        /// <summary>
        /// 优惠券来源
        /// </summary>
        public CouponSource CouponSource { get; set; }


        /// <summary>
        /// 满多少
        /// </summary>
        public decimal FullAmount { get; set; }

        /// <summary>
        /// 减多少
        /// </summary>
        public decimal DisAmount { get; set; }

        /// <summary>
        /// 生效开始时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 生效结束时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CouponStatus Status { get; set; }

        /// <summary>
        /// 优惠券创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 顾客所在集团
        /// </summary>
        public int MemberGroupId { get; set; }

        /// <summary>
        /// 会员编号
        /// </summary>
        public string MemberId { get; set; }

        /// <summary>
        /// 顾客所在门店
        /// </summary>
        public int MemberStoreId { get; set; }

        /// <summary>
        /// 顾客所在专柜
        /// </summary>
        public int MemberSectionId { get; set; }

        /// <summary>
        /// 是否是其他商户的顾客
        /// </summary>
        public bool IsOutSiteMember { get; set; } = false;


        /// <summary>
        /// 用户的VipCode
        /// </summary>
        public string VipCode { get; set; }

        /// <summary>
        /// 核销时间
        /// </summary>
        public DateTime? VerifyTime { get; set; }

        #region 活动
        /// <summary>
        /// 活动id
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 子模块编号
        /// </summary>
        public string SubModuleId { get; set; }

        /// <summary>
        /// 子模块名称
        /// </summary>
        public string SubModuleName { get; set; }

        /// <summary>
        /// 执行策略Id
        /// </summary>
        public string StrategyActionId { get; set; }

        /// <summary>
        /// 策略名称
        /// </summary>
        public string StrategyActionName { get; set; }
        #endregion

        /// <summary>
        /// 其他附加信息
        /// </summary>
        public Dictionary<string, string> ExtendInfo { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 第几个发送的，对应action中sendbody下的index字段
        /// </summary>
        public int SendIndex { get; set; }

        /// <summary>
        /// 优惠券模板id
        /// </summary>
        public string TempleteId { get; set; }
    }
}