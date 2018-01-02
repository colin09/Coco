using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wx.sqldb.data;

namespace com.wx.service.iservice
{
   public interface IStoreService
   {

       dynamic GetList(int page = 1, int pageSize = 12);

       dynamic GetDetail(int id);

       bool Modify(StoreEntity m, bool isCreate);

       bool Delete(int id);
   }
}
