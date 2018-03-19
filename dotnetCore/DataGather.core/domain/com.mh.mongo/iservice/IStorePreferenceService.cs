using System;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;

namespace com.mh.mongo.iservice
{
    public interface IStorePreferenceService
    {

        string GetVipLevelName(int storeId, string vipLevel);

        
    }
}

