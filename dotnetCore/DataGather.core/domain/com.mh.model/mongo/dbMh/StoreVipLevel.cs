using System;
using System.ComponentModel;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{
    [BsonIgnoreExtraElements]
    public class StoreVipLevel : MagicHorseBase
    {
        public int GroupId { set; get; }

        public int StoreId { set; get; }

        public int VipLevel { set; get; }

        public string VipLevelName { set; get; }

        public string VipCode { set; get; }

        public VipLevelStatus Status { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
    }




    public enum VipLevelStatus
    {
        [Description("删除")]
        Delete = -1,
        [Description("系统保留级别")]
        System = 0,
        [Description("不显示")]
        Hide = 1,
        [Description("显示")]
        Show = 2
    }

}