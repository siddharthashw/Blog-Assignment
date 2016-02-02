using Autofac;
using RoutingAssignment.Models;
using RoutingAssignment.Controllers;
using IdentitySample.Models;
using Autofac.Integration.Mvc;
using System.Web.Mvc;


namespace RoutingAssignment
{
    public static class Autofac1
    {
       public static void Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(BasicsController).Assembly);
            builder.RegisterType<ApplicationDbContext>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}