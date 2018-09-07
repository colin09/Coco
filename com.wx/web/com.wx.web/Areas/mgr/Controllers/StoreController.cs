using System.Web.Mvc;
using com.wx.common.helper;
using com.wx.common.mvc;
using com.wx.service.iservice;
using com.wx.sqldb.data;
using Newtonsoft.Json;

namespace com.wx.web.Areas.mgr.Controllers
{
    public class StoreController : ManagerController
    {
        private readonly IStoreService _storeService;


        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;


        }


        // GET: mgr/Store
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult List(int page, int pageSize)
        {
            var list = _storeService.GetList(page, pageSize);
            //log.Info($"store-list.count = {JsonConvert.SerializeObject(list)}");
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Modify(string name, string addr, string contact, string mobile, string email, int id)
        {
            if (name.IsEmpty())
                return Json(new { message = "名称不能为空！" }, JsonRequestBehavior.AllowGet);
            if (mobile.IsEmpty())
                return Json(new { message = "电话不能为空！" }, JsonRequestBehavior.AllowGet);
            if (contact.IsEmpty())
                return Json(new { message = "联系人不能为空！" }, JsonRequestBehavior.AllowGet);

            bool create = !(id > 0);
            var m = new StoreEntity()
            {
                Name = name,
                Address = addr,
                Contact = contact,
                Mobile = mobile,
                EMail = email
            };
            var r = _storeService.Modify(m, create);

            return Json(new { message = r }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var r = _storeService.Delete(id);

            return Json(new { message = r }, JsonRequestBehavior.AllowGet);
        }
    }
}