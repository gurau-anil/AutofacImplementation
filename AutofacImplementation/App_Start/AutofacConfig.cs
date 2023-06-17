using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutofacImplementation.Controllers;
using AutofacImplementation.Filters;
using AutofacImplementation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace AutofacImplementation.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterService()
        {
            var builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;

            // Register the controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<WorkContext>().As<IWorkContext>().SingleInstance();
            builder.RegisterType<TestService>().As<ITestService>();

            //builder.RegisterWebApiFilterProvider(config);

            //builder.Register(c => new TestApiAttribute(c.Resolve<IWorkContext>())).AsWebApiActionFilterFor<IHttpController>().InstancePerRequest();
            // Build the container
            var container = builder.Build();

            // Set the dependency resolver
            var resolver = new AutofacWebApiDependencyResolver(container);
            var mvcresolver = new AutofacDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            DependencyResolver.SetResolver(mvcresolver);
        }
    }
}