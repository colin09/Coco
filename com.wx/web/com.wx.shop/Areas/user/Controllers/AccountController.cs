using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.wx.shop.Areas.user.Controllers
{
    public class AccountController : Controller
    {
        // GET: user/Account
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Center()
        {
            return View();
        }
    }
}