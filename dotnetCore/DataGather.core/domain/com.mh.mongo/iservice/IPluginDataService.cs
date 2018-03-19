using System;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;
using com.mh.model.mongo.dbSource;

namespace com.mh.mongo.iservice
{
    public interface IPluginDataService
    {

        List<StoreDataGather> GetStoreDataRules();
        void ModifyStoreLastSyncTime(int storeId);
        void ModifyStoreDataRuleLastId(int storeId, double lastId);


        List<StoreVipLevel> GetStoreVipLevels(int storeId);


        #region  --  MemberInfo  --

        
        void AddMemberInfos(List<MemberInfo> list);
        void ModifyMemberAllInfos(List<MemberInfo> list);
        void AddMemberVipChangeList(List<MemberInfoVipChange> list);
        void AddMemberManagerList(List<MemberInfoManager> list, int storeId);
        List<MemberInfo> GetMemberList(int storeId, List<string> ids = null, List<string> memberIds = null, List<string> openIds = null, List<string> vipCodes = null, List<int> userIds = null, List<string> loginNames = null);

        void AddMemberInfoList(List<SyncMemberInfoTemp> syncMemberTemps);



        List<string> GetMemberManager(List<string> ids, int storeId);

        #endregion


        #region  --  PluginOrderInfo  --
        void AddPluginOrderInfoList<T>(List<T> list) where T : PluginOrderBase;
        List<string> GetPluginOrderList<T>(List<string> list) where T : PluginOrderBase;
        List<string> GetPluginOrderList<T>(List<string> list, int storeId) where T : PluginOrderBase;



        List<dw_memberinfo_source> ReadMidMemberInfo( DateTime startTime, List<string> sectionCodes, int page, int pageSize);
        List<string> ReadMidOrderIds(DateTime startTime, List<string> sectionCodes, int pageStart, int pageSize);
        List<PluginOrderTxt> ReadMidOrderList(int startIndex, List<string> ids);


        #endregion
    }
}