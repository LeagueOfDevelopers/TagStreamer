using System.Configuration;
using Microsoft.Practices.Unity;
using System.Web.Http;
using TagStream.Infrastructure;
using TagStream.Models;
using Unity.WebApi;

namespace TagStream
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

			container.RegisterType<AdminService>(new ContainerControlledLifetimeManager());
			container.RegisterType<UserFeedItemService>(new ContainerControlledLifetimeManager());
			container.RegisterType<InstagramManager>(new PerThreadLifetimeManager(), 
				new InjectionConstructor(ConfigurationManager.AppSettings["InstagramTag"]));
			container.RegisterType<TwitterManager>(new PerThreadLifetimeManager(), new InjectionConstructor(
				ConfigurationManager.AppSettings["TwitterTag"]));
			container.RegisterType<IRecentItemProvider, RecentItemProvider>(new ContainerControlledLifetimeManager());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}