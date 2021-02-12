using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoPartsStockControlSystem.Startup))]
namespace AutoPartsStockControlSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
