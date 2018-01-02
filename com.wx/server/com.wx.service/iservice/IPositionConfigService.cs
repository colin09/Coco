using com.wx.service.models;
using com.wx.sqldb.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.service.iservice
{
    public interface IPositionConfigService
    {
        List<PositionModel> GetAll();

        List<PositionModel> GetListByParentId(int parentId);

        bool Modify(PositionModel m, bool isNew);

        bool Delete(int id);

        List<PositionItemModel> GetItsmsByCode(int code);
        bool ItemModify(PositionItemModel m, bool isNew);
        bool DeleteItem(int id);
        List<PositionItemModel> GetPositionItsms(int code, PageType type, int relationId, RelationType relationType);
    }
}
