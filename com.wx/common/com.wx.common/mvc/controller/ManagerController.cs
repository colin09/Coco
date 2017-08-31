using com.wx.common.helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.common.mvc
{
    [UserAuthorize]
    public class ManagerController : BaseController
    {

        



        public ManagerController()
        {
            if (UserLevel < 0 || UserLevel > 100)
                Redirect("~/Home/Index");
        }
        





    }
}
