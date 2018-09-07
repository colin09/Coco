using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.common.mvc
{
    [UserAuthorize]
    public class WeixinPageController : BaseController
    {


        public WeixinPageController()
        {
            if (OpenId == "0" || UserId < 1)
                Redirect("~/Home/Error?message='用户尚未同意微信授权。'");
        }
    }
}
