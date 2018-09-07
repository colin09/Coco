using com.wx.service.basis;
using com.wx.service.iservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wx.service.models;
using com.wx.sqldb.data;

namespace com.wx.service.service
{
    public class PositionConfigService : BaseService, IPositionConfigService
    {
        public List<PositionModel> GetAll()
        {
            var query = DbSession.PositionConfigRepository.Where(c => c.Status == DataStatue.Normal);

            if (query.Any())
                return query.OrderBy(o => o.Sort).Select(c => new PositionModel
                {
                    Id = c.Id,
                    ParentId = c.ParentId,
                    Name = c.Name,
                    EnName = c.EnName,
                    Type = c.Type,
                    Deep = c.Deep,
                    Sort = c.Sort
                }).ToList();
            return null;
        }



        public List<PositionModel> GetListByParentId(int parentId)
        {
            var query = DbSession.PositionConfigRepository.Where(c => c.Status == DataStatue.Normal && c.ParentId == parentId);

            if (query.Any())
                return query.OrderBy(o => o.Sort).Select(c => new PositionModel
                {
                    Id = c.Id,
                    ParentId = c.ParentId,
                    Name = c.Name,
                    EnName = c.EnName,
                    Type = c.Type,
                    Deep = c.Deep,
                    Sort = c.Sort
                }).ToList();
            return null;
        }

        public bool Modify(PositionModel m, bool isNew)
        {
            var model = new PositionConfigEntity()
            {
                ParentId = m.ParentId,
                Name = m.Name,
                EnName = m.EnName,
                Desc = "",
                Deep = m.Deep,
                Sort = m.Sort,
                UpdateDate = DateTime.Now,
                Status = DataStatue.Normal
            };

            if (isNew)
            {
                model.CreateDate = DateTime.Now;
                DbSession.PositionConfigRepository.Create(model);
            }
            else
            {
                model.Id = m.Id;
                DbSession.PositionConfigRepository.Update(model);
            }
            return true;
        }
        public bool Delete(int id)
        {
            DbSession.PositionConfigRepository.Delete(id);
            return true;
        }







        #region

        public List<PositionItemModel> GetItsmsByCode(int code)
        {
            var query = DbSession.PositionItemRepository.Where(i => i.Code == code && i.Status == DataStatue.Normal);
            if (query.Any())
            {
                return query.OrderBy(o => o.Sort).Select(i => new PositionItemModel()
                {
                    Id = i.Id,
                    Code = i.Code,
                    Name = i.Name,
                    EnName = i.EnName,
                    Alias = i.Alias,
                    Image = i.Image,
                    Domain = "http://image.ziyoufeng.tw/",
                    Desc = i.Desc,
                    Type = i.Type,
                    Sort = i.Sort,
                    RelationType=i.RelationType,
                    RelationId=i.RelationId,
                }).ToList();
            }
            return null;
        }

        public bool ItemModify(PositionItemModel m, bool isNew)
        {
            var model = new PositionItemEntity()
            {
                Code = m.Code,
                Name = m.Name,
                EnName = m.EnName,
                Alias = m.Alias,
                Image = m.Image,
                Desc = m.Desc,
                Type = m.Type,
                Sort = m.Sort,
                UpdateDate = DateTime.Now,
                Status = DataStatue.Normal
            };
            if (isNew)
            {
                model.CreateDate = DateTime.Now;
                DbSession.PositionItemRepository.Create(model);
            }
            else
            {
                model.Id = m.Id;
                DbSession.PositionItemRepository.Update(model);
            }
            return true;
        }

        public bool DeleteItem(int id)
        {
            var result = DbSession.PositionItemRepository.Delete(id);
            return result > 0;
        }


        public List<PositionItemModel> GetPositionItsms(int code, PageType type, int relationId, RelationType relationType)
        {
            var query = DbSession.PositionItemRepository.Where(i => i.Code == code && i.Status == DataStatue.Normal);
            if (type > 0)
                query = query.Where(i => i.Type == type);
            if (relationId > 0)
                query = query.Where(i => i.RelationId == relationId);
            if (relationType > 0)
                query = query.Where(i => i.RelationType == relationType);

            if (query.Any())
            {
                return query.OrderBy(o => o.Sort).Select(i => new PositionItemModel()
                {
                    Id = i.Id,
                    Code = i.Code,
                    Name = i.Name,
                    EnName = i.EnName,
                    Alias = i.Alias,
                    Image = i.Image,
                    Domain = "http://image.ziyoufeng.tw/",
                    Desc = i.Desc,
                    Type = i.Type,
                    Sort = i.Sort,
                    RelationId = i.RelationId,
                    RelationType = i.RelationType
                }).ToList();
            }
            return null;
        }

        #endregion






    }
}
