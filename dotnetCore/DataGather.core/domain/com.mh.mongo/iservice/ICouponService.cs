using System;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;

namespace com.mh.mongo.iservice
{
    public interface ICouponService
    {

        List<CouponInfo> GetCouponList(int storeId, List<string> codes);

        void UseCouponInfo(List<string> codes);

    }
}