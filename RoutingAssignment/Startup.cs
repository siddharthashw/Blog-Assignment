using Autofac;
using Owin;
using RoutingAssignment.Controllers;
using System.Web.Mvc;

namespace IdentitySample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
