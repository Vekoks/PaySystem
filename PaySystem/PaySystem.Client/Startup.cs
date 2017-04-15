using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaySystem.Client.Startup))]
namespace PaySystem.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
