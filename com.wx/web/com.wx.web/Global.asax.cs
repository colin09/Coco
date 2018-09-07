using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using com.wx.ioc.IOCUnity;

namespace com.wx.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //注册 Unity IOC 容器 方法一
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());
        }
    }
}
