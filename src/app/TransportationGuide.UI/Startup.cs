using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransportationGuide.UI.Startup))]
namespace TransportationGuide.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
