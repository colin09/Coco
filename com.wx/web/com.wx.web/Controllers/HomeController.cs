using com.wx.service.iservice;
using com.wx.sqldb.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.wx.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPositionConfigService _positionService;


        public HomeController(IPositionConfigService positionService)
        {
            _positionService = positionService;
        }


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Quotation()
        {
            return View();
        }
        public ActionResult Scene()
        {
            return View();
        }
        public ActionResult Customer()
        {
            return View();
        }

        public ActionResult ItemData(int code, PageType type, int sourceId)
        {
            var result = _positionService.GetPositionItsms(code, type, sourceId, 0);

            var list = result.Where(r => r.RelationType == RelationType.Image).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult Technique()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Detail(int code, int source)
        {
            ViewBag.Code = code;
            ViewBag.Source = source;

            return View();
        }


        public ActionResult List(int code, int source)
        {
            ViewBag.Code = code;
            ViewBag.Source = source;
            ViewBag.Title = "-";
            var result = _positionService.GetPositionItsms(code, PageType.Detail, source, 0);
            var list = result.Where(r => r.RelationType == RelationType.Image).ToList();
            ViewBag.List = list;
            if (list != null && list.Count > 0)
                ViewBag.Caption = list.FirstOrDefault().Name;

            return View();
        }

        public ActionResult QuotationDetail(int code)
        {
            ViewBag.Code = code;
            return View();
        }









        public ActionResult Index2()
        {
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;

            return View();
        }
    }
}