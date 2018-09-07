using System;
using System.ComponentModel;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;

namespace com.mh.model.mongo.dbMh
{
    [BsonIgnoreExtraElements]
    public class StoreDataGather : MagicHorseBase
    {
        public int StoreId { set; get; }
        public int GroupId { set; get; }

        public string RuleKey { set; get; }

        public string EMails { set; get; }
        public double LastId { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LastSyncTime { set; get; }

        //public Dictionary<string, int> DataList { set; get; }
        public List<string> DataList { set; get; }

        public List<DataGatherConfig> RunStrategy { set; get; }
    }

    [BsonIgnoreExtraElements]
    public class DataGatherConfig
    {
        public string Type { set; get; }
        //public RuntimeType RuntimeType { set; get; }

        public Dictionary<string, string> Config { set; get; }
        //public int MappingId { set; get; }
        public Dictionary<string, string> Mapping { set; get; }

        public List<RunModule> SubModule { set; get; }

        /// <summary>
        /// 数据类型 0:历史数据  1：每日新增数据
        /// </summary>
        public DataTypeEnum DataType { get; set; }
        /// <summary>
        /// 插件Id  PluginManager.ModuleId
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 模版id Template.Id
        /// </summary>
        public string TemplateId { get; set; }

    }


    public class RunModule
    {
        public int ModuleId { set; get; }
        public string ModuleName { set; get; }
        public string ModuleType { set; get; }
    }



    public enum DataTypeEnum
    {
        /// <summary>
        /// 历史数据
        /// </summary>
        History = 0,

        /// <summary>
        /// 每日新增数据
        /// </summary>
        Increment = 1
    }
}