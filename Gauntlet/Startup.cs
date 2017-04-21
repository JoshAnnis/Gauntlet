using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gauntlet.Startup))]
namespace Gauntlet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
