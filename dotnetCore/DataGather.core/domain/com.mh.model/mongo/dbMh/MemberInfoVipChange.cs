using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{


    [BsonIgnoreExtraElements]
    public class MemberInfoVipChange : MagicHorseBase
    {

        public int StoreId { get; set; }

        public int SectionId { get; set; }

        public int GroupId { get; set; }

        public string MemberId { get; set; }
        /// <summary>
        /// 上次卡级别
        /// </summary>
        public string LastCardLeave { get; set; }

        /// <summary>
        /// 最新卡级别
        /// </summary>
        public string CurrentCardLeave { get; set; }

        /// <summary>
        /// 变化时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 哪天开始变化的
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ChangeFrom { get; set; }

        /// <summary>
        /// 哪天结束变化的
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ChangeTo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }
    }
}