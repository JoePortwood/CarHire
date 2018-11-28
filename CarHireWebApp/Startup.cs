using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarHireWebApp.Startup))]
namespace CarHireWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
