using System.Collections.Generic;
using System.Runtime.Serialization;
using ERP.Domain.Enums.AmoebaModule;

namespace ERP.Domain.Contract.AmoebaModule.AmoebaReportModule
{
    [DataContract]
    public class AmoebaReportComparisonVO
    {
        [DataMember]
        public int Id { set; get; }
        [DataMember]
        public string Title { set; get; }

        [DataMember]
        public List<AmoebaReportComparisonItems> Items { set; get; }


    }


    [DataContract]
    public class AmoebaReportComparisonItems
    {
        [DataMember]
        public int Id { set; get; }
        public int ParentId { set; get; }
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public decimal? Value { set; get; }
        [DataMember]
        public decimal? CompareValue { set; get; }
        [DataMember]
        public bool ValuePercent { set; get; }

        [DataMember]
        public decimal? Increase { set; get; }
        /// <summary>
        /// 无子项
        /// </summary>
        [DataMember]
        public bool EndNode { set; get; }
        /// <summary>
        /// 是否百分数
        /// </summary>
        [DataMember]
        public bool Percent { set; get; }



        [DataMember]
        public decimal? Target { set; get; }
        [DataMember]
        public decimal? TargetNext { set; get; }
        [DataMember]
        public AmoebaTargetType TargetType { set; get; }
        [DataMember]
        public string Improvement { set; get; }
    }
}
