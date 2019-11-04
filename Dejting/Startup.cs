using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dejting.Startup))]
namespace Dejting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
