using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(com.wx.shop.Startup))]
namespace com.wx.shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
