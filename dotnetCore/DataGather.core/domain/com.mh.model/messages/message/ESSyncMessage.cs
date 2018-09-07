using System;

namespace com.mh.model.messages.message
{

    public class ESSyncMessage : BaseMessage
    {
        public override int SourceType
        {
            get
            {
                return (int)MessageSourceType.ESSync;
            }
        }

        public override int ActionType
        {
            get
            {
                return (int)MessageAction.ESSync;
            }
        }
        /// <summary>
        /// 同步类型
        /// </summary>
        public SyncType SyncType { get; set; }

        public int UserId { get; set; }

        public ESSyncMemberInfoRequest ESSyncMemberInfo { get; set; }
    }

    /// <summary>
    /// 同步顾客信息
    /// </summary>
    public class ESSyncMemberInfoRequest
    {
        public string ManagerId { get; set; }

        public string MemberId { get; set; }

        public int StoreId { get; set; }

        public int SectionId { get; set; }

        public int GroupId { get; set; }
    }

    public enum SyncType
    {
        #region 商品
        /// <summary>
        /// 根据商品编号同步商品
        /// </summary>
        SyncProductById = 0,
        /// <summary>
        /// 根据门店同步商品
        /// </summary>
        SyncProductByStoreId = 1,
        /// <summary>
        /// 根据用户ID同步商品
        /// </summary>
        SyncProductByUserId = 2,
        /// <summary>
        /// 同步所有商品
        /// </summary>
        SyncProductAll = 3,
        #endregion

        #region 门店
        /// <summary>
        /// 根据门店id同步门店
        /// </summary>
        SyncStoreById = 10,
        /// <summary>
        /// 同步所有门店
        /// </summary>
        SyncStoreAll = 11,
        #endregion

        #region 买手
        /// <summary>
        /// 根据userId同步买手
        /// </summary>
        SyncAssociateByUserId = 20,
        /// <summary>
        /// 根据门店编号同步买手
        /// </summary>
        SyncAssociateByStoreId = 21,
        /// <summary>
        /// 根据专柜同步买手
        /// </summary>
        SyncAssociateBySectionId = 22,
        #endregion

        #region 资源
        /// <summary>
        /// 根据资源Id同步资源信息到ES
        /// </summary>
        SyncResourceById = 30,

        /// <summary>
        /// 同步所有的资源信息
        /// </summary>
        SynResourceAll = 31,

        #endregion

        #region 顾客
        /// <summary>
        /// 根据资源Id同步资源信息到ES
        /// </summary>
        SyncCustomerById = 40,

        /// <summary>
        /// 同步所有的资源信息
        /// </summary>
        SyncCustomerAll = 41,
        #endregion

        #region 收藏/喜欢/关注
        /// <summary>
        /// 关注用户
        /// </summary>
        SyncFavoriteUser = 50,
        /// <summary>
        /// 收藏商品
        /// </summary>
        SyncFavoriteProduct = 51,

        /// <summary>
        /// 喜欢商品
        /// </summary>
        SyncLikeProduct = 52,

        /// <summary>
        /// 收藏买手后，同步买手数据
        /// </summary>
        SyncFavoriteForBuyer = 53,
        #endregion

        #region 品牌
        /// <summary>
        /// 根据id同步品牌
        /// </summary>
        SyncBrandById = 60,
        /// <summary>
        /// 根据专柜id同步品牌
        /// </summary>
        SyncBrandBySectionId = 61,
        /// <summary>
        /// 根据门店id同步品牌
        /// </summary>
        SYncBrandByStoreId = 62,
        /// <summary>
        /// 同步所有品牌
        /// </summary>
        SyncAllBrand = 63,
        /// <summary>
        /// 同步某个门店的所有品牌
        /// </summary>
        SyncAllStoreBrand = 64,

        #endregion


        #region 新版顾客信息
        /// <summary>
        /// 同步顾客信息
        /// </summary>
        SyncMemberInfo=70,
        #endregion
    }
}
