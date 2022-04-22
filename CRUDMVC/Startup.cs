using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUDMVC.Startup))]
namespace CRUDMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
