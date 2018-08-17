using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Enums.FileModule
{
    public enum FileType
    {
        /// <summary>
        /// 采购入库单图片
        /// </summary>
        PurchaseInStockNotePic,

        /// <summary>
        /// 供应商图片
        /// </summary>
        ProviderPic,
        /// <summary>
        /// 物流凭证图片
        /// </summary>
        LogisticsPic,
        /// <summary>
        /// 经销商入库图片
        /// </summary>
        ShopPurchaseInStockNotePic,

        /// <summary>
        /// 破损出库图片
        /// </summary>
        BaseNotePic,

        /// <summary>
        /// 签到拜访照片
        /// </summary>
        SignPic,
    }
}
