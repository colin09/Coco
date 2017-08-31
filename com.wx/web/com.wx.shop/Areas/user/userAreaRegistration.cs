using System.Web.Mvc;

namespace com.wx.shop.Areas.user
{
    public class userAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "user";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "user_default",
                "user/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}