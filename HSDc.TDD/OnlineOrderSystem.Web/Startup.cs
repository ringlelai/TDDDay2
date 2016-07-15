using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineOrderSystem.Web.Startup))]
namespace OnlineOrderSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
