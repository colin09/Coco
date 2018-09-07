using System;
using com.wx.service.basis;
using com.wx.service.iservice;
using com.wx.sqldb.data;
using System.Linq;
using com.wx.mq;
using Newtonsoft.Json;

namespace com.wx.service.service
{
    public class StoreService : BaseService, IStoreService
    {



        public StoreService()
        {

        }

        public dynamic GetList(int page = 1, int pageSize = 12)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 12;

            var query = from store in DbSession.StoreRepository.Where(s => s.Status == DataStatue.Normal)
                        join res in DbSession.ResourceRepository.Where(r => r.Status == DataStatue.Normal && r.SourceType == SourceType.StoreQrCode)
                            on store.Id equals res.SourceId into resources
                        from res in resources.DefaultIfEmpty()
                        select new { store, res };
            //var query = DbSession.StoreRepository.Page(pageSize, page, out total, s => s.Status == DataStatue.Normal, s => s.CreateDate, false);

            if (!query.Any())
                return null;
            var total = query.Count();

            var list = query.OrderByDescending(q => q.store.CreateDate).Skip((page - 1) * pageSize).Take(pageSize).ToList().Select(q => new
            {
                id = q.store.Id,
                name = q.store.Name,
                addr = q.store.Address,
                contact = q.store.Contact,
                mobile = q.store.Mobile,
                email = q.store.EMail,
                qrCode = q.res?.Domain + q.res?.Name + q.res?.ExtName
            }).ToList();

            var result = new
            {
                item = list,
                total = total,
                pageCount = Math.Ceiling((double)total / pageSize)
            };
            return result;
        }


        public dynamic GetDetail(int id)
        {
            var query = from store in DbSession.StoreRepository.Where(s => s.Id == id && s.Status == DataStatue.Normal)
                        join resource in DbSession.ResourceRepository.Where(r => r.Status == DataStatue.Normal && r.SourceType == SourceType.StoreQrCode) on store.Id equals resource.SourceId into resources
                        from resource in resources.DefaultIfEmpty()
                        select new { s = store, r = resource };

            if (query.Any())
                return query.ToList().Select(q => new
                {
                    id = q.s.Id,
                    name = q.s.Name,
                    addr = q.s.Address,
                    contact = q.s.Contact,
                    mobile = q.s.Mobile,
                    email = q.s.EMail,
                    qrCode = q.r?.Name,
                }).FirstOrDefault();
            return null;
        }


        public bool Modify(StoreEntity m, bool isCreate)
        {
            try
            {
                if (isCreate)
                {
                    Log.Info("create store-before:{0}", JsonConvert.SerializeObject(m));
                    DbSession.StoreRepository.Create(m);
                    DbSession.SaveChange();
                    Log.Info("create store-after:{0}", JsonConvert.SerializeObject(m));
                    MessageNotify.NotifyStoreQrCode(new string[] { m.Id.ToString() });
                }
                else
                    DbSession.StoreRepository.Update(m);

                /*
                var ticket = Api_QrCode.CreateTicketFoyStore(WXAccessToken.param.AccessToken, m.Id);
                var bytes = Api_QrCode.GetQrCodeByTicket(ticket);

                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                var fileName = $"store\\qrcode\\{Guid.NewGuid().ToString()}";
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{fileName}.png";
                image.Save(path);

                DbSession.ResourceRepository.Create(new ResourceEntity()
                {
                    SourceId = m.Id,
                    SourceType = SourceType.StoreQrCode,

                    Name = fileName,
                    ExtName = ".png",
                });
                */
                return true;
            }
            catch (System.Exception ex)
            {
                Log.Info($"create[{isCreate}] store error ==>{ex}");
                return false;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                DbSession.StoreRepository.Delete(id);

                return true;
            }
            catch (System.Exception ex)
            {
                Log.Info($"delete store error ==>{ex.Message}");
                return false;
            }
        }
    }
}


