using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Testgrej.Startup))]
namespace Testgrej
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
