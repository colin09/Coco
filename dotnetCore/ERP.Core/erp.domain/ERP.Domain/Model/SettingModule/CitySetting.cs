using ERP.Domain.Enums.SettingModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.SettingModule
{
    public class CitySetting : BaseEntity, IAggregationRoot
    {
        /// <summary>
        /// 财务期初数据初始化状态
        /// </summary>
        public InitOpeningDataState InitOpeningDataState { get; set; }

        /// <summary>
        /// 上线状态
        /// </summary>
        public OnlineState OnlineState { get; set; }

        /// <summary>
        /// 是否强制使用高拍仪
        /// </summary>
        public bool FocusCapture { get; set; }

        /// <summary>
        /// 是否启用采购申请流程
        /// </summary>
        public bool EnablePurchaseRequisition { get; set; }

        /// <summary>
        /// 盘点是否强制录入生产日期
        /// </summary>
        public bool IsOpenProductionDate { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }


        public City City { get; set; }
    }
}
