using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IBS.Startup))]
namespace IBS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
