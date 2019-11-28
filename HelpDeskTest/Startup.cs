using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpDeskTest.Startup))]
namespace HelpDeskTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
