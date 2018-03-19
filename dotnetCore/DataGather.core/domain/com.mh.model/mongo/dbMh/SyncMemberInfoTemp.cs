using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;



namespace com.mh.model.mongo.dbMh
{
 [BsonIgnoreExtraElements]
    public class SyncMemberInfoTemp : MagicHorseBase
    {
        /// <summary>
        /// MemberId
        /// </summary>
        public string MemberId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DataStatus Status { get; set; }
    }

}