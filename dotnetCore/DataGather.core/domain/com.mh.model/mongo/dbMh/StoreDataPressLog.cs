using System;
using System.ComponentModel;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{




    /* StoreDataPressLog 数据处理日志 */
    // 处理逻辑：导入订单时，将导入的订单中的支付时间分组顺序写入此表 写入此表时，状态为0
    //			 会员同步时：会员同步完成后，将会员最后更新时间顺序写入此表 写入此表时，状态为0
    // 数据输出到 数据执行锚点表 StoreDataAnchor表 
    [BsonIgnoreExtraElements]
    public class StoreDataPressLog : MagicHorseBase
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Time { get; set; }

        /// <summary>
        /// 0:订单 1:会员
        /// </summary>
        public StoreDataPressType Type { get; set; }

        /// <summary>
        /// 0：未处理 1：已处理
        /// </summary>
        public StoreDataPressStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }
    }





    
    public enum StoreDataPressStatus
    {
        UnDeal = 0,
        Dealed = 1
    }

    public enum StoreDataPressType
    {
        Order = 0,
        Member = 1
    }
}