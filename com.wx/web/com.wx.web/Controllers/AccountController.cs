using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using com.wx.web.Models;
using com.wx.service.iservice;
using com.wx.common.helper;
using com.wx.service.models;
using Newtonsoft.Json;

namespace com.wx.web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var s = "";
            s.ToJson();

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = _userService.Login(model.Mobile, model.Password);
            if (result.IsSuccess)
            {
                var r = result as ServiceResult<dynamic>;
                var cookieName = "COOKIE_MANAGER_INFO";
                var cookie = JsonConvert.SerializeObject(r.Data);

                HttpHelper.WriteCookie(cookieName, cookie, 60);
                return Redirect("~/mgr/Home/Index");
            }
            else
            {
                ViewBag.message = result.Message;
                return View(model);
            }

        }

    }
}