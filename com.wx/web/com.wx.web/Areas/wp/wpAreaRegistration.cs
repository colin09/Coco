using System.Web.Mvc;

namespace com.wx.web.Areas.wp
{
    public class wpAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "wp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "wp_default",
                "wp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, 
                namespaces: new string[] { "com.wx.web.Areas.wp.Controllers" }
            );
        }
    }
}