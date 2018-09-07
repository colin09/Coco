using ERP.Domain.Enums.AuditTraceModule;
using ERP.Domain.Model.CommonModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AuditTraceModule
{

    [Table("AuditTrace")]
    public class AuditTrace : BaseEntity, IAggregationRoot
    {
        public AuditTrace()
            : base()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.UserRoleInfo = new UserRoleInfo();
        }
        #region 基本属性

        /// <summary>
        /// 排序号
        /// </summary>
        public int SequenceNo { get; set; }

        /// <summary>
        /// 日志状态
        /// </summary>
        public AuditTraceState TraceState { get; set; }

        public UserRoleInfo UserRoleInfo { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        [MaxLength(64)]
        public string UserId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        [MaxLength(256)]
        public string UserName { get; set; }

        /// <summary>
        /// 操作人手机号码
        /// </summary>
        [MaxLength(64)]
        public string MobileNo { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(255)]
        public string Remark { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 图片存储容器
        /// </summary>
        [NotMapped]
        public string[] ImgIdsContainer
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ImgIds))
                {
                    return new string[0];
                }

                return JsonConvert.DeserializeObject<string[]>(ImgIds);
            }
            set
            {
                if (value == null)
                    ImgIds = null;
                else
                    ImgIds = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// 图片存储Json
        /// </summary>
        [MaxLength(255)]
        public string ImgIds { get; set; }

        /// <summary>
        /// 附件存储容器
        /// </summary>
        [NotMapped]
        public string[] FileIdsContainer
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FileIds))
                {
                    return new string[0];
                }

                return JsonConvert.DeserializeObject<string[]>(FileIds);
            }
            set
            {
                if (value == null)
                    FileIds = null;
                else
                    FileIds = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// 附件存储Json
        /// </summary>
        [MaxLength(255)]
        public string FileIds { get; set; }

        #endregion
    }
}
