using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    /// <summary>
    /// 检查信息项： 序列化后存储
    /// </summary>
    public class PurchaseRequisitionCheckInfoItem : IValueObject
    {

        public PurchaseRequisitionCheckInfoItem() { }

        public PurchaseRequisitionCheckInfoItem(PurchaseRequisitionCheckInfoItem item)
        {
            this.SequenceNo = item.SequenceNo;
            this.Passed = item.Passed;
            this.Title = item.Title;
            this.Content = item.Content;
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SequenceNo { get; set; }
        /// <summary>
        /// 检查是否通过
        /// </summary>
        public bool Passed { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 具体内容（可能无内容）
        /// </summary>
        public string Content { get; set; }
    }
}
