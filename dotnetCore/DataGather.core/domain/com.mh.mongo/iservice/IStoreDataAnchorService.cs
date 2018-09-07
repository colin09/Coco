
using System;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;

namespace com.mh.mongo.iservice
{
    public interface IStoreDataAnchorService
    {

        
        void AddStoreDataPressLog(StoreDataPressLog log);
        
        void AddStoreDataPressLogList(List<StoreDataPressLog> list);
    }
}