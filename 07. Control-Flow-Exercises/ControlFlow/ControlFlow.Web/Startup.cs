using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControlFlow.Web.Startup))]
namespace ControlFlow.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
