using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Chinook.Application.Configuration;
using Chinook.Infrastructure.Configuration;

namespace Chinook.Web
{
	public static class Bootstrapper
	{
		public static IWindsorContainer InitializeContainer()
		{
			// Windsor configuration
			var container = new WindsorContainer();
			
			container.Install(FromAssembly.This(), 
							  FromAssembly.Containing<NHibernateInstaller>(), 
							  FromAssembly.Containing<HandlersInstaller>());

			return container;
		}

		public static void Release(IWindsorContainer container)
		{
			container.Dispose();
		}
	}
}
