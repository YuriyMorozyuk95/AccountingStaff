using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccountingStaff.Web.Startup))]
namespace AccountingStaff.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
