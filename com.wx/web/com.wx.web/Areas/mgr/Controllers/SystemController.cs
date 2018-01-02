using System.Web.Mvc;
using com.wx.api.weixin;
using com.wx.common.mvc;
using com.wx.service.iservice;
using com.wx.service.models;
using System.Linq;

namespace com.wx.web.Areas.mgr.Controllers
{
    public class SystemController : ManagerController
    {

        private readonly IPositionConfigService _positionService;


        public SystemController(IPositionConfigService positionService)
        {
            _positionService = positionService;


        }


        // GET: mgr/System
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            if (UserLevel != 1)
                return Json(new { msg = "权限不足" }, JsonRequestBehavior.AllowGet);

            var r = MenuMg.CrateMenu();
            return Json(new { result = r }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Read()
        {
            if (UserLevel != 1)
                return Json(new { msg = "权限不足" }, JsonRequestBehavior.AllowGet);

            var r = MenuMg.GetMenu();
            return Json(new { result = r }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PositionConfig()
        {
            var result = _positionService.GetAll();

            return View(result);
        }

        public ActionResult PosotionItems(int code)
        {
            var list = _positionService.GetItsmsByCode(code);

            //var group = list.GroupBy(l=>l.re)



            if (list != null)
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return Json("empty", JsonRequestBehavior.AllowGet);

        }



        public ActionResult ModifyItem(PositionItemModel m ,bool isNew)
        {
            var result = _positionService.ItemModify(m, isNew);

            return Json(new { message = result }, JsonRequestBehavior.AllowGet);
        }


        

    }
}