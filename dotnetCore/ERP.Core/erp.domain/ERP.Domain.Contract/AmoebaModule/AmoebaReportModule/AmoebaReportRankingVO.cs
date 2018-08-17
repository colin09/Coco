using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ERP.Domain.Contract.AmoebaModule.AmoebaReportModule
{
    [DataContract]
    public class AmoebaReportRankingVO
    {
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public string Name { set; get; }


        [DataMember]
        public string FirstName { set; get; }
        [DataMember]
        public string SecondName { set; get; }
        [DataMember]
        public string ThirdName { set; get; }
        [DataMember]
        public string Unit { set; get; }
        [DataMember]
        public bool ValuePercent { set; get; }


        [DataMember]
        public List<AmoebaReportRankingItems> Items { set; get; }
    }

    [DataContract]
    public class AmoebaReportRankingItems
    {
        public string Id { set; get; }
        [DataMember]
        public long Rank { set; get; }
        public string CityId { set; get; }
        [DataMember]
        public string CityName { set; get; }
        [DataMember]
        public decimal? Value { set; get; }
        [DataMember]
        public string Province { set; get; }
        public string City { set; get; }
        [DataMember]
        public bool IsCurrent { set; get; }
    }
}
