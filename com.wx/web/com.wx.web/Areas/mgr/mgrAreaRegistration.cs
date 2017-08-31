using System.Web.Mvc;

namespace com.wx.web.Areas.mgr
{
    public class mgrAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "mgr";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "mgr_default",
                "mgr/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "com.wx.web.Areas.mgr.Controllers" }
            );
        }
    }
}