using Microsoft.Owin;

[assembly: OwinStartupAttribute(typeof(TechJunk.Web.Startup))]
namespace TechJunk.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
