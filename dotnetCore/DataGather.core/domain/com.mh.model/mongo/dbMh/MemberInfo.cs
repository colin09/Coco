using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{

    [BsonIgnoreExtraElements]
    public class MemberInfo : MagicHorseBase
    {

        public MemberInfo()
        {

        }
        public MemberInfo(string id)
        {
            this.Id = GetObjectId(id);
        }

        public MemberInfo(bool isCreateId)
        {
            if (isCreateId)
                this.Id = GetObjectId();
        }
        public int GroupId { set; get; }
        public int StoreId { set; get; }
        public int SectionId { set; get; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        public string Logo { set; get; }
        /// <summary>
        /// 性别  1:男，2:女，0:未知
        /// </summary>
        public GenderType Gender { set; get; }

        public int BuyerUserId { set; get; }
        public string SectionCode { set; get; }
        public string VipCode { set; get; }
        public int CustomerId { set; get; }
        public string MemberId { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Birthday { set; get; }

        /// <summary>
        /// 生日是哪一天
        /// </summary>
        public int BirthdayOfYear
        {
            set { }
            get
            {
                var day = 0;
                if (Birthday != null)
                    day = Convert.ToInt32(Birthday.Value.ToString("MMdd"));
                return day;
            }
        }
        public string MobilePhone { set; get; }
        public string Email { set; get; }

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
        /// 顾客级别变化
        /// </summary>
        [BsonIgnore]
        public string LeavelChange { get; set; }


        /// <summary>
        /// 外部用户记录
        /// </summary>
        public OutSiteInfo OutSite { set; get; }
        /// <summary>
        /// 公众号关注信息
        /// </summary>
        public SubScribeInfo SubScribe { set; get; }
        /// <summary>
        /// 成交数据
        /// </summary>
        public SaleDataInfo SaleData { set; get; }

        public int Status { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { set; get; }
        public int CreateUser { set; get; }
        public int UpdateUser { set; get; }

        /// <summary>
        /// 成为会员时间  如果不赋值，则只能定义成当前时间哦。我能怎样。。。呵呵哒
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? RegisterTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 15分钟专用，对方更新时间 ，同步会员的时候写这个时间，如果没有这个时间，则写成跟updatetime 一样
        /// </summary>
        public DateTime LatestModifyTim { get; set; }
    }



    [BsonIgnoreExtraElements]
    public class OutSiteInfo
    {
        /// <summary>
        /// openId
        /// </summary>
        public string OutSiteUId { set; get; }

        /// <summary>
        /// 第三方的编码
        /// </summary>
        public string OutSiteIndex { get; set; }

        /// <summary>
        /// 第三方的会员状态
        /// </summary>
        public DataStatus OutSiteStatus { get; set; } = DataStatus.Normal;

        /// <summary>
        /// 有效积分
        /// </summary>
        public double AviableIntegral { get; set; } = 0;

        /// <summary>
        /// 总积分
        /// </summary>
        public double TotalIntegral { get; set; } = 0;

        /// <summary>
        /// 冻结积分
        /// </summary>
        public double FreezeIntegral { get; set; } = 0;
        /// <summary>
        /// 数据来源 enum
        /// </summary>
        public int OutSiteType { set; get; }
        /// <summary>
        /// 会员从哪创建的
        /// </summary>
        public string From { set; get; }
        /// <summary>
        /// 会员详细描述，根据分类获取detail并将数组转化为使用的实体
        /// </summary>
        public Dictionary<string, string> Detail { set; get; }

        public string LoginName { set; get; }


        //新加字段
        /// <summary>
        /// 会员卡级别
        /// </summary>
        public string VipCardLevel { get; set; }

        /// <summary>
        /// 会员卡变化时间
        /// </summary>
        public DateTime? VipChangeDate { get; set; }

        /// <summary>
        /// 注册成为会员时间
        /// </summary>
        public DateTime? VipRegisterTime { get; set; }

        /// <summary>
        /// 品牌编码
        /// </summary>
        public string BrandCode { get; set; }

        /// <summary>
        /// 注册的门店编码
        /// </summary>
        public string RegisterStoreNo { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class SubScribeInfo
    {
        /// <summary>
        /// 是否关注公众号
        /// </summary>
        public bool IsSubscribe { set; get; }
        /// <summary>
        /// 关注时间
        /// </summary>
        public string SubscribeTime { set; get; }
        /// <summary>
        /// 取消关注时间
        /// </summary>
        public string UnSubscribeTime { set; get; }
        /// <summary>
        /// 是否扫码
        /// </summary>
        public bool IsScan { set; get; }
        /// <summary>
        /// 最后一次扫码时间
        /// </summary>
        public string LastScanTime { set; get; }

    }

    [BsonIgnoreExtraElements]
    public class SaleDataInfo
    {
        /// <summary>
        /// 是否购买
        /// </summary>
        public bool IsBuyer { set; get; }
        /// <summary>
        /// 最后一次购买时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LastBuyerTime { set; get; }
    }

}
