using System.Linq;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Cronos.Infrastructure.Mvc;
using Cronos.Web.Controllers;
using SignalR;
using SignalR.Hosting.AspNet;
using SignalR.Infrastructure;

namespace Cronos.Web.Configuration
{
	public class MvcInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				AllTypes.FromAssemblyContaining<HomeController>()
					.BasedOn<IController>()
					.WithService.Self()
					.Configure(cfg => cfg.LifestyleTransient()),
					
				Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>(),

				Component.For<IConnectionManager>().Instance(AspNetHost.DependencyResolver.Resolve<IConnectionManager>())
			
				);

			DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
		}
	}
}
