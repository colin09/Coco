using System.Web.Mvc;

namespace com.wx.shop.Areas.server
{
    public class serverAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "server";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "server_default",
                "server/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}