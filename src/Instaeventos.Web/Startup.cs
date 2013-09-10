using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Instaeventos.Web.Startup))]
namespace Instaeventos.Web
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
