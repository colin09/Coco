using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ERP.Domain.Enums.AmoebaModule;



namespace ERP.Domain.Model.AmoebaReportModule
{
    [Table("AmoebaConfig")]
    public class AmoebaConfig : BaseEntity, IAggregationRoot
    {

        public int DataId { set; get; }
        public int ParentId { set; get; }
        [MaxLength(64)]
        public string Name { set; get; }
        [MaxLength(64)]
        public string Alias { set; get; }
        public bool EndNode { set; get; }
        public bool Percent { set; get; }
        [MaxLength(64)]
        public string FirstName { set; get; }
        [MaxLength(64)]
        public string SecondName { set; get; }
        [MaxLength(64)]
        public string ThirdName { set; get; }
        [MaxLength(64)]
        public string Unit { set; get; }
        
        public bool HasTarget { set; get; }
        public AmoebaTargetType TargetType { set; get; }
        
    }



    /* 
    public enum AmoebaTargetType
    {
        None = 1,
        Calculate,
        Edit,
    }*/

}
