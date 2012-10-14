using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Chinook.Core.Configuration;

namespace Chinook.Infrastructure.Configuration
{
	public class AutomapperInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For(typeof(ManyToOneResolver<>)));	
			AutomapperConfiguration.Configure(container.Resolve);
		}
	}
}
