using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Cronos.Core.Configuration
{
	public static class AutomapperConfiguration
	{
		public static void Configure(Func<Type, object> serviceLocator = null)
		{
			if (serviceLocator != null)
				Mapper.Configuration.ConstructServicesUsing(serviceLocator);

			var configurators = Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => typeof(IObjectMapperConfigurator).IsAssignableFrom(t)
							&& !t.IsAbstract
							&& !t.IsInterface)
				.Select(Activator.CreateInstance).OfType<IObjectMapperConfigurator>();

			foreach (var configurator in configurators)
			{
				configurator.Apply();
			}
		}
	}
}
