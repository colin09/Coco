using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model
{
    /// <summary>
    /// 所有实体的基类
    /// </summary>
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N");

            CreateTime = DateTime.Now;
            LastUpdateTime = DateTime.Now;
        }
        
        [Key]
        [MaxLength(64)]
        public string Id { get; set; }

        /// <summary>
        /// 数据的创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}
