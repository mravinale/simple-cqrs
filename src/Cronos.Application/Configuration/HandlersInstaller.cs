using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Cronos.Application.Processors;
using Cronos.Infrastructure.Commands;
using Cronos.Infrastructure.Queries;

namespace Cronos.Application.Configuration
{
	public class HandlersInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			var metadataProviderContributorsAssemblies = new[] { typeof (QueryProcessor).Assembly };

				container.Register(AllTypes.From(metadataProviderContributorsAssemblies.SelectMany(a => a.GetExportedTypes()))
			                   	.Where(t => t.Name.EndsWith("Handler") && t.IsAbstract == false)
			                   	.Configure(x => x.LifestyleSingleton())
				);
			container.Register(Component.For<IProcessQuery>().ImplementedBy<QueryProcessor>().LifeStyle.Singleton);
			container.Register(Component.For<IProcessCommand>().ImplementedBy<CommandProcessor>().LifeStyle.Singleton);
		}
	}

}
