using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Model.AmoebaReportModule
{
    /// <summary>
    /// 阿米巴目标
    /// </summary>
    [Table("AmoebaImprovement")]
    public class AmoebaImprovement : BaseEntity, IAggregationRoot
    {
        #region  基本属性
        [MaxLength(64)]
        public string CityId { set; get; }
        [MaxLength(32)]
        public string DataMonth { set; get; }


        [MaxLength(64)]
        public string DataAlias { set; get; }
        [MaxLength(512)]
        public string Improvement { set; get; }



        #endregion

    }
}
