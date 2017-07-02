using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineCourt2017.Startup))]
namespace OnlineCourt2017
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
