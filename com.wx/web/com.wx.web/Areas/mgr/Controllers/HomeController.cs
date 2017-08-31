using com.wx.common.mvc;
using System.Web.Mvc;
using com.wx.service.iservice;

namespace com.wx.web.Areas.mgr.Controllers
{


    public class HomeController : ManagerController
    {



        // GET: mgr/Home
        public ActionResult Index()
        {
            ViewBag.nickName = this.NickName;
            return View();
        }

    }
}