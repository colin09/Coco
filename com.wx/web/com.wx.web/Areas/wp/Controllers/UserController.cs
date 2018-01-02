using System.Web.Mvc;
using com.wx.common.mvc;
using com.wx.service.iservice;
using com.wx.service.models;
using com.wx.sqldb.data;

namespace com.wx.web.Areas.wp.Controllers
{
    public class UserController : WeixinPageController
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: wp/User
        public ActionResult Index()
        {
            ViewBag.nickName = "昵称";
            ViewBag.logo = "../../images/logo.gif";
            ViewBag.desc = "描述。";

            var result = _userService.GetUserInfo(UserId);
            if (result.IsSuccess)
            {
               var r = result as ServiceResult<UserEntity>;

                ViewBag.nickName = r.Data.NickName;
                ViewBag.logo = r.Data.Logo;
            }

            return View();
        }
    }
}