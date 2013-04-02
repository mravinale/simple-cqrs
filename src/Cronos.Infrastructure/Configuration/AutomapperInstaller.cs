using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Cronos.Core.Configuration;

namespace Cronos.Infrastructure.Configuration
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
