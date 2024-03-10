using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(College.Startup))]
namespace College
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
