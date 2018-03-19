using System;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;

namespace com.mh.mongo.iservice
{
    public interface IPluginExecuteService
    {

        
        List<StoreExecute> GetStoreExecute(List<int> storeIds);
    }
}

