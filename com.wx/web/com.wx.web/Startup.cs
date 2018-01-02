using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(com.wx.web.Startup))]
namespace com.wx.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
