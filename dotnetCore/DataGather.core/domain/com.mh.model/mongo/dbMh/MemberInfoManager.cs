using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{


    [BsonIgnoreExtraElements]
    public class MemberInfoManager : MagicHorseBase
    {

        public string MemberId { set; get; }

        public int BuyerUserId { set; get; }

        /// <summary>
        /// 顾客关系修改级别
        /// 0：默认 
        /// 1：买手认领
        /// 2：数据导入[门店配置]
        /// 3：店长分配
        /// </summary>
        public int ManagerLevel { set; get; }

        public MemberManagerState Status { set; get; }

        public string Logo { set; get; }

        public string Name { set; get; }

        public string NickName { set; get; }

        public string Description { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastContactTime { set; get; }

        public List<MemberTag> Tags { set; get; }

        public string Mobile { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { set; get; }

        public int CreateUser { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateDate { set; get; }
        public int UpdateUser { set; get; }


        public List<MemberManagerLog> UpdateLog { set; get; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 专柜ID
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// 集团Id
        /// </summary>
        public int GroupId { get; set; }


        /// <summary>
        /// VIP编码
        /// </summary>
        public string VipCode { get; set; }

        /// <summary>
        /// 最后扫码时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LastScanTime { get; set; }
    }



    public class MemberManagerLog
    {
        public string MemberId { set; get; }

        public int BuyerUserId { set; get; }
        public int ManagerLevel { set; get; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateDate { set; get; }
        public int UpdateUser { set; get; }
    }

    public class MemberTag
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }


    public enum MemberManagerState
    {
        Delete = -1,
        Default = 0,
        Normal = 1,
    }

}