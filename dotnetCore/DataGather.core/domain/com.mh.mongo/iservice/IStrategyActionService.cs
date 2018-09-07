using System;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;

namespace com.mh.mongo.iservice
{
    public interface IStrategyActionService
    {


        void WriteConversionRate(Dictionary<string, int> dic);

        void WriteMemberBack(List<ActionEffect> list);

    }
}
